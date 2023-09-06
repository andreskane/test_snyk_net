using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structure
{
    public class GetNodeTreeByNodeIdQuery : IRequest<IList<int>>
    {
        public int StructureId { get; set; }

        public int NodeRootId { get; set; }

        public int NodeId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }

        public List<int> NodesId { get; set; }

    }

    public class GetNodeTreeByNodeIdqueryHandler : IRequestHandler<GetNodeTreeByNodeIdQuery, IList<int>>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;


        public GetNodeTreeByNodeIdqueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<int>> Handle(GetNodeTreeByNodeIdQuery request, CancellationToken cancellationToken)
        {
            if (request.NodesId == null)
                request.NodesId = new List<int>();

            var nodeIdFrom = (from a in _context.StructureAristas.AsNoTracking()
                           where a.StructureIdFrom == request.StructureId
                           && a.NodeIdTo == request.NodeId
                           && a.ValidityFrom <= request.ValidityFrom
                           select a.NodeIdFrom).FirstOrDefault();

            request.NodesId.Add(nodeIdFrom);

            if(nodeIdFrom != request.NodeRootId)
                await _mediator.Send(new GetNodeTreeByNodeIdQuery { StructureId = request.StructureId, NodeRootId = request.NodeRootId, NodeId = nodeIdFrom, ValidityFrom = request.ValidityFrom, NodesId = request.NodesId });

            return request.NodesId;
        }
    }
}
