using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureExistenceDateQuery : IRequest<DateTimeOffset?>
    {
        public int StructureId { get; set; }
    }

    public class GetStructureExistenceDateQueryHandler : IRequestHandler<GetStructureExistenceDateQuery, DateTimeOffset?>
    {
        private readonly IMediator _mediator;


        public GetStructureExistenceDateQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DateTimeOffset?> Handle(GetStructureExistenceDateQuery request, CancellationToken cancellationToken)
        {
            var nodes = await _mediator.Send(new GetAllNodeQuery { StructureId = request.StructureId });
            var oldestNode = nodes
                .OrderBy(o => o.NodeValidityFrom)
                .FirstOrDefault();

            if (oldestNode == null)
                return null;


            return await Task.Run(() => oldestNode.NodeValidityFrom);
        }
    }
}

