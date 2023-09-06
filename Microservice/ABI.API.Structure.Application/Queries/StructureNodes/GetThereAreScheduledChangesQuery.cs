using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetThereAreScheduledChangesQuery : IRequest<bool>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetThereAreScheduledChangesQueryHandler : IRequestHandler<GetThereAreScheduledChangesQuery, bool>
    {
        private readonly IMediator _mediator;

        public GetThereAreScheduledChangesQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(GetThereAreScheduledChangesQuery request, CancellationToken cancellationToken)
        {
            var list = await _mediator.Send(new GetAllNodePendingScheduledChangesQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            return list.Count > 0;
        }
    }
}
