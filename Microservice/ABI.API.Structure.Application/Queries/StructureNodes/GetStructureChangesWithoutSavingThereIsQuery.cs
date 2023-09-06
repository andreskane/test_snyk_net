using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureChangesWithoutSavingThereIsQuery : IRequest<bool>
    {
        public int StructureId { get; set; }
    }

    public class GetStructureChangesWithoutSavingThereIsQueryHandler : IRequestHandler<GetStructureChangesWithoutSavingThereIsQuery, bool>
    {
        private readonly IMediator _mediator;


        public GetStructureChangesWithoutSavingThereIsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(GetStructureChangesWithoutSavingThereIsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = DateTimeOffset.UtcNow.Date });

            if (result.Count == 0)
                return false;

            return true;
        }
    }
}

