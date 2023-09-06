using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures
{
    public class GetByIdQuery : IRequest<StructureDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, StructureDTO>
    {
        private readonly IMapper _mapper;
        private readonly IStructureRepository _repository;
        private readonly IMediator _mediator;

        public GetByIdQueryHandler(IStructureRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<StructureDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetStructureNodeRootAsync(request.Id);
            var map = _mapper.Map<StructureDomain, StructureDTO>(result);

            map.ThereAreChangesWithoutSaving = await _mediator.Send(new GetThereAreChangesWithoutSavingQuery { StructureId = map.Id }, cancellationToken);
            map.ThereAreScheduledChanges = await _mediator.Send(new GetThereAreScheduledChangesQuery { StructureId = map.Id }, cancellationToken);

            return map;
        }
    }
}
