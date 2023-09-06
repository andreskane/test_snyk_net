using ABI.API.Structure.ACL.Truck.Application.Queries.LogImpactTruck;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures
{
    public class GetAllOrderQuery : IRequest<IList<StructureDTO>>
    {
        public string Country { get; set; }
    }

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<StructureDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureRepository _repository;
        private readonly IMediator _mediator;

        public GetAllOrderQueryHandler(IStructureRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IList<StructureDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var logImpactStatus = await _mediator.Send(new GetAllStructureStatesQuery());

            var country = !string.IsNullOrEmpty(request.Country) ? request.Country.ToUpper() : request.Country;
            var results = await _repository.GetAllStructureNodeRootAsync(country);
            var map = _mapper.Map<IList<StructureDomain>, IList<StructureDTO>>(results);

            foreach (var m in map)
            {
                m.ThereAreChangesWithoutSaving = await _mediator.Send(new GetThereAreChangesWithoutSavingQuery { StructureId = m.Id, ValidityFrom = DateTimeOffset.UtcNow.Date }, cancellationToken);
                m.ThereAreScheduledChanges = await _mediator.Send(new GetThereAreScheduledChangesQuery { StructureId = m.Id, ValidityFrom = DateTimeOffset.UtcNow.Date }, cancellationToken);
                m.Processing = logImpactStatus.Any(a => a.StructureId == m.Id && a.StateId == 2);
            }

            return map;
        }
    }
}
