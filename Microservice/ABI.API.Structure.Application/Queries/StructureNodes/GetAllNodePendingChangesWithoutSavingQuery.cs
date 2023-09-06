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
    public class GetAllNodePendingChangesWithoutSavingQuery : IRequest<IList<NodePendingDTO>>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllNodePendingChangesWithoutSavingQueryHandler : IRequestHandler<GetAllNodePendingChangesWithoutSavingQuery, IList<NodePendingDTO>>
    {

        private readonly StructureContext _context;


        public GetAllNodePendingChangesWithoutSavingQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<NodePendingDTO>> Handle(GetAllNodePendingChangesWithoutSavingQuery request, CancellationToken cancellationToken)
        {
            // Cambios en arista y nodo_definicion
            var q1 = (
                from a in _context.StructureAristas.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on a.NodeIdTo equals nd.NodeId
                from a1 in _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == nd.NodeId && w.MotiveStateId == (int)MotiveStateNode.Confirmed)
                    .DefaultIfEmpty()
                from nd1 in _context.StructureNodeDefinitions.AsNoTracking()
                    .Where(w => w.NodeId == nd.NodeId && w.MotiveStateId == (int)MotiveStateNode.Confirmed)
                    .DefaultIfEmpty()
                where
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    a.MotiveStateId == (int)MotiveStateNode.Draft &&
                    nd.MotiveStateId == (int)MotiveStateNode.Draft
                select new { nd, a, isNew = (a1 == null && nd1 == null) }
            );

            // Cambios solo en arista
            var q2 = (
                from a in _context.StructureAristas.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on a.NodeIdTo equals nd.NodeId
                where
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    a.MotiveStateId == (int)MotiveStateNode.Draft &&
                    nd.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom &&
                    !q1.Any(x => x.nd.NodeId == a.NodeIdTo)
                select new { nd, a, isNew = false }
            );

            var q3 = (
                from a in _context.StructureAristas.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on a.NodeIdTo equals nd.NodeId
                let isNew =
                    (
                        _context.StructureNodeDefinitions.AsNoTracking()
                        .Where(w => w.NodeId == nd.NodeId && (w.MotiveStateId == (int)MotiveStateNode.Draft || w.MotiveStateId == (int)MotiveStateNode.Confirmed))
                        .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                        &&
                        _context.StructureAristas.AsNoTracking()
                        .Where(w => w.NodeIdTo == nd.NodeId)
                        .Count(c => c.MotiveStateId == (int)MotiveStateNode.Confirmed) == 1        
                        &&
                         _context.StructureAristas
                            .Any(aa => aa.NodeIdTo == nd.NodeId)
                    )
                where
                    
                    a.StructureIdTo == request.StructureId  && a.StructureIdFrom == request.StructureId &&
                    a.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom &&
                    nd.MotiveStateId == (int)MotiveStateNode.Draft &&
                    !q1.Any(x => x.nd.NodeId == a.NodeIdTo)
                select new { nd, a, isNew }
            );


            var list = await q1.Union(q2).Union(q3)
                .Select(s => new NodePendingDTO
                {
                    StructureId = request.StructureId,
                    NodeId = s.nd.NodeId,
                    NodeName = s.nd.Name,
                    NodeCode = s.nd.Node.Code,
                    NodeLevelId = s.nd.Node.LevelId,
                    NodeDefinitionId = s.nd.Id,
                    NodeParentId = s.a.NodeIdFrom,
                    NodeMotiveStateId = s.nd.MotiveStateId,
                    NodeValidityFrom = s.nd.ValidityFrom,
                    AristaValidityFrom = s.a.ValidityFrom,
                    AristaValidityTo = s.a.ValidityTo,
                    AristaMotiveStateId = s.a.MotiveStateId,
                    TypeVersion = (s.isNew ? "N" : "B")
                })
                .ToListAsync();


            var root = (
                from s in _context.Structures.AsNoTracking()
                join nd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(i => i.Node)
                    on s.RootNodeId equals nd.NodeId
                where
                    s.Id == request.StructureId &&
                    nd.MotiveStateId == (int)MotiveStateNode.Draft
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
                    TypeVersion = "B"
                }).FirstOrDefault();

            if (root != null)
                list.Add(root);


            return list;
        }
    }
}
