using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllNodeMaxVersionQuery : IRequest<List<NodeMaxVersionDTO>>
    {
        public DateTimeOffset ValidityFrom { get; set; }
        public IList<NodeAristaDTO> Nodes { get; set; }
    }

    public class GetAllNodeMaxVersionQueryHandler : IRequestHandler<GetAllNodeMaxVersionQuery, List<NodeMaxVersionDTO>>
    {
 
        //public GetAllNodeMaxVersionQueryHandler(StructureContext context)
        //{
             
        //}

        public   Task<List<NodeMaxVersionDTO>> Handle(GetAllNodeMaxVersionQuery request, CancellationToken cancellationToken)
        {
            var nodes = (
                from ndn in request.Nodes
                where
                    ndn.NodeValidityFrom <= request.ValidityFrom &&
                    ndn.AristaMotiveStateId == (int)MotiveStateNode.Confirmed
                group ndn by ndn.NodeId into g
                select new NodeMaxVersionDTO
                {
                    NodeId = g.Key,
                    ValidityFrom = g.Max(e => e.NodeValidityFrom)
                }
            )
            .ToList();

            var nodesNew = (
                from ndn in request.Nodes
                where
                    ndn.NodeValidityFrom == request.ValidityFrom &&
                    ndn.NodeMotiveStateId == (int)MotiveStateNode.Draft &&
                    ndn.AristaMotiveStateId == (int)MotiveStateNode.Draft
                group ndn by ndn.NodeId into g
                select new NodeMaxVersionDTO
                {
                    NodeId = g.Key,
                    ValidityFrom = g.Max(e => e.NodeValidityFrom)
                }
            )
            .ToList();

            var listNodes = nodes.Union(nodesNew).ToList();

            return Task.FromResult(listNodes);
        }
    }
}
