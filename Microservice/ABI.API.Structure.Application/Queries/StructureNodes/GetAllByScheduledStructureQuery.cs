using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.DTO.Extension;
using ABI.API.Structure.Application.Extensions;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllByScheduledStructureQuery : IRequest<DTO.StructureDomainDTO>
    {
        public int Id { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? Active { get; set; }
    }

    public class GetAllByScheduledStructureHandler : IRequestHandler<GetAllByScheduledStructureQuery, DTO.StructureDomainDTO>
    {
        private readonly IMediator _mediator;


        public GetAllByScheduledStructureHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<DTO.StructureDomainDTO> Handle(GetAllByScheduledStructureQuery request, CancellationToken cancellationToken)
        {
            var pendingDate = await _mediator.Send(new GetPendingChangesDateQuery { StructureId = request.Id, ValidityFrom = request.ValidityFrom }, cancellationToken);

            var structureData = await _mediator.Send(new GetStructureDomainQuery { StructureId = request.Id });

            if (structureData == null)
                throw new NotFoundException();

            var validityFrom = pendingDate.HasValue && pendingDate.Value > request.ValidityFrom ? pendingDate.Value : request.ValidityFrom;

            if (request.ValidityFrom == DateTimeOffset.MinValue)
            {
                throw new NotFoundException();
            }

            var pendingNodes = await _mediator.Send(new GetAllNodePendingChangesWithoutSavingQuery { StructureId = request.Id, ValidityFrom = validityFrom });

            var pendingDates = pendingNodes
                .Where(w => w.NodeMotiveStateId == (int)MotiveStateNode.Draft)
                .Select(s => s.NodeValidityFrom)
                .Union(
                    pendingNodes
                        .Where(w => w.AristaMotiveStateId == (int)MotiveStateNode.Draft)
                        .Select(s => s.AristaValidityFrom)
                )
                .GroupBy(g => g)
                .Select(s => s.Key)
                .OrderBy(o => o);

            if (pendingDates.Any())
            {
                if (pendingDates.Any(a => a.ToUniversalTime().Date.CompareTo(DateTimeOffset.UtcNow.ToOffset(-3).Date) <= 0))
                    await _mediator.Send(new DeleteNodesChangesWithoutSavingCommand(request.Id, DateTimeOffset.UtcNow.ToOffset(-3).Date)); //TODO: Ajustar para multipaís
                else
                    validityFrom = pendingDates.First();
            }

            var actualNodes = await _mediator.Send(new GetAllStructureNodesQuery { StructureId = request.Id, ValidityFrom = validityFrom, Active = request.Active });

            if (actualNodes.Count == 0)
            {
                var scheduledChanges = await _mediator.Send(new GetAllNodePendingScheduledChangesQuery { StructureId = request.Id, ValidityFrom = validityFrom });

                var scheduleDates = scheduledChanges
                    .Where(w => w.NodeMotiveStateId == (int)MotiveStateNode.Confirmed)
                    .GroupBy(g => g.NodeValidityFrom)
                    .Select(s => s.Key)
                    .OrderBy(o => o);

                if (scheduleDates.Any())
                {
                    validityFrom = scheduleDates.First();
                    actualNodes = await _mediator.Send(new GetAllStructureNodesQuery { StructureId = request.Id, ValidityFrom = validityFrom, Active = request.Active });
                }
            }

            var futureNodes = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.Id, ValidityFrom = validityFrom });
            var changesWithoutSaving = await _mediator.Send(new GetThereAreChangesWithoutSavingQuery { StructureId = request.Id, ValidityFrom = validityFrom });

            var structureNode = structureData.ToStructureDomainDTO(validityFrom, actualNodes, futureNodes.ToList(), request.Active, changesWithoutSaving);

            structureNode.CalendarDate = validityFrom;

            return structureNode;
        }
    }
}
