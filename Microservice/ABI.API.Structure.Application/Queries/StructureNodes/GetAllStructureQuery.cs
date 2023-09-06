using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.DTO.Extension;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllStructureQuery : IRequest<StructureDomainDTO>
    {

        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? Active { get; set; }
    }

    public class GetStructurePortalQueryHandler : IRequestHandler<GetAllStructureQuery, StructureDomainDTO>
    {
        private readonly IMediator _mediator;

        public GetStructurePortalQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<StructureDomainDTO> Handle(GetAllStructureQuery request, CancellationToken cancellationToken)
        {
            var structureData = await _mediator.Send(new GetStructureDomainQuery { StructureId = request.StructureId });

            var result = await _mediator.Send(new GetAllStructureNodesQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom, Active = request.Active });

            var resultPending = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            var changesWithoutSaving = await _mediator.Send(new GetThereAreChangesWithoutSavingQuery { StructureId = request.StructureId });

            return structureData.ToStructureDomainDTO(request.ValidityFrom, result, resultPending.ToList(), request.Active, changesWithoutSaving);

        }
    }
}
