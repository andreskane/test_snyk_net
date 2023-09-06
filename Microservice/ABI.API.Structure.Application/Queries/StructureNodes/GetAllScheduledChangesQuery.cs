using ABI.API.Structure.Application.Extensions;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllScheduledChangesQuery : IRequest<IList<DateTimeOffset>>
    {
        public int Id { get; set; }
    }

    public class GetAllScheduledChangesHandler : IRequestHandler<GetAllScheduledChangesQuery, IList<DateTimeOffset>>
    {
        private readonly IMediator _mediator;

        public GetAllScheduledChangesHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IList<DateTimeOffset>> Handle(GetAllScheduledChangesQuery request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetStructureDomainQuery { StructureId = request.Id });

            if (structure == null)
                throw new NotFoundException();

            var nodesFuture = await _mediator.Send(new GetAllNodePendingScheduledChangesQuery { StructureId = request.Id });
            var nodesPendig = await _mediator.Send(new GetAllNodePendingChangesWithoutSavingQuery { StructureId = request.Id });
            var existenceDate = await _mediator.Send(new GetStructureExistenceDateQuery { StructureId = request.Id });

            var scheduleDates = new List<DateTimeOffset>();

            scheduleDates.AddRange(
                nodesFuture
                 .Where(w => w.NodeMotiveStateId == (int)MotiveStateNode.Confirmed)
                .GroupBy(g => g.NodeValidityFrom)
                .Select(s => s.Key)
            );

            scheduleDates.AddRange(
                nodesFuture
                 .Where(w => w.AristaMotiveStateId == (int)MotiveStateNode.Confirmed)
                .GroupBy(g => g.AristaValidityFrom)
                .Select(s => s.Key)
            );

            scheduleDates.AddRange(
              nodesPendig
               .Where(w => w.NodeMotiveStateId == (int)MotiveStateNode.Draft)
              .GroupBy(g => g.NodeValidityFrom)
              .Select(s => s.Key)
             );

            scheduleDates.AddRange(
              nodesPendig
               .Where(w => w.AristaMotiveStateId == (int)MotiveStateNode.Draft)
              .GroupBy(g => g.AristaValidityFrom)
              .Select(s => s.Key)
             );

            if (existenceDate.HasValue)
            {
                if (!scheduleDates.Contains(existenceDate.Value))
                    scheduleDates.Add(existenceDate.Value);
            }
            else
                scheduleDates.Add(DateTimeOffset.UtcNow.Today());

            return scheduleDates
                .Distinct()
                .OrderBy(o => o)
                .ToList();
        }
    }
}
