using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllNodePendingScheduledChangesQuery : IRequest<IList<NodePendingDTO>>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllNodePendingScheduledChangesQueryHandler : IRequestHandler<GetAllNodePendingScheduledChangesQuery, IList<NodePendingDTO>>
    {
        private readonly StructureContext _context;

        public GetAllNodePendingScheduledChangesQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<NodePendingDTO>> Handle(GetAllNodePendingScheduledChangesQuery request, CancellationToken cancellationToken)
        {
            // Cambios en arista y nodo_definicion
            var list = await (
                from a in _context.StructureAristas.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on a.NodeIdTo equals nd.NodeId
                where
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    (
                        a.MotiveStateId == (int)MotiveStateNode.Confirmed && a.ValidityFrom > request.ValidityFrom ||
                        nd.MotiveStateId == (int)MotiveStateNode.Confirmed && nd.ValidityFrom > request.ValidityFrom
                    )
                select new NodePendingDTO
                {
                    StructureId = request.StructureId,
                    NodeId = nd.NodeId,
                    NodeName = nd.Name,
                    NodeCode = nd.Node.Code,
                    NodeLevelId = nd.Node.LevelId,
                    NodeDefinitionId = nd.Id,
                    NodeParentId = a.NodeIdFrom,
                    NodeMotiveStateId = nd.MotiveStateId,
                    NodeValidityFrom = nd.ValidityFrom,
                    AristaValidityFrom = a.ValidityFrom,
                    AristaValidityTo = a.ValidityTo,
                    AristaMotiveStateId = a.MotiveStateId,
                    TypeVersion = "F"
                }
            ).ToListAsync();



            var root = (
                from s in _context.Structures.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on s.RootNodeId equals nd.NodeId
                where
                    s.Id == request.StructureId && 
                    nd.MotiveStateId == (int)MotiveStateNode.Confirmed && nd.ValidityFrom > request.ValidityFrom
                select new NodePendingDTO
                {
                    StructureId = request.StructureId,
                    NodeId = nd.NodeId,
                    NodeName = nd.Name,
                    NodeCode = nd.Node.Code,
                    NodeLevelId = nd.Node.LevelId,
                    NodeDefinitionId = nd.Id,
                    NodeParentId = null,
                    NodeMotiveStateId = nd.MotiveStateId,
                    NodeValidityFrom = nd.ValidityFrom,
                    AristaValidityFrom = nd.ValidityFrom,
                    AristaValidityTo = nd.ValidityTo,
                    AristaMotiveStateId = nd.MotiveStateId,
                    TypeVersion = "F"
                }).ToList();

            if (root != null)
                list.AddRange(root);


            return list;
        }
    }
}
