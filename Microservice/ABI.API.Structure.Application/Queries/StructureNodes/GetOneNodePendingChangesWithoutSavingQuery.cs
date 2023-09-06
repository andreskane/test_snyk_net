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
    public class GetOneNodePendingChangesWithoutSavingQuery : IRequest<DTO.NodePendingDTO>
    {
        public int StructureId { get; set; }

        public int NodeId { get; set; }

        public DateTimeOffset Validity { get; set; }
    }

    public class GetOneNodePendingChangesWithoutSavingQueryHandler : IRequestHandler<GetOneNodePendingChangesWithoutSavingQuery, DTO.NodePendingDTO>
    {
        private readonly StructureContext _context;


        public GetOneNodePendingChangesWithoutSavingQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DTO.NodePendingDTO> Handle(GetOneNodePendingChangesWithoutSavingQuery request, CancellationToken cancellationToken)
        {


            var nodeRoot = (from n in _context.StructureNodes.AsNoTracking()
                            .Include(i => i.StructureNodoDefinitions)
                            join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                            join s in _context.Structures.AsNoTracking() on n.Id equals s.RootNodeId
                            let isNew = (n.StructureNodoDefinitions.Count == 1)
                           where
                            n.Id == request.NodeId &&
                            (
                                (nd.ValidityFrom > request.Validity && nd.MotiveStateId == (int)MotiveStateNode.Draft) ||
                                nd.MotiveStateId == (int)MotiveStateNode.Draft
                            )
                            select new
                            {
                                n.Id,
                                n.Code,
                                n.LevelId,
                                NodeDefinitionId = nd.Id,
                                nd.Name,
                                nd.MotiveStateId,
                                nd.ValidityFrom,
                                IsNew = isNew
                            }
                            )
                            .FirstOrDefault();


            if (nodeRoot != null)

            {
                return new DTO.NodePendingDTO
                {
                    StructureId = request.StructureId,
                    AristaId = 0,
                    NodeId = nodeRoot.Id,
                    NodeName = nodeRoot.Name,
                    NodeLevelId = nodeRoot.LevelId,
                    NodeDefinitionId = nodeRoot.NodeDefinitionId,
                    NodeParentId = null,
                    NodeMotiveStateId = nodeRoot.MotiveStateId,
                    NodeValidityFrom = nodeRoot.ValidityFrom,
                    AristaMotiveStateId = nodeRoot.MotiveStateId,
                    AristaValidityFrom = nodeRoot.ValidityFrom,
                    TypeVersion = (nodeRoot.IsNew ? "N" : "B")
                };
            }
                

            var arista = await (
                from n in _context.StructureNodes.AsNoTracking()
                join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                where
                    n.Id == request.NodeId &&
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    a.MotiveStateId == (int)MotiveStateNode.Draft
                select a
            )
            .FirstOrDefaultAsync();

            if (arista == null)
            {
                arista = await (
                    from n in _context.StructureNodes.AsNoTracking()
                    join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                    where
                        n.Id == request.NodeId &&
                        a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                        a.ValidityFrom <= request.Validity && a.ValidityTo >= request.Validity &&
                        a.MotiveStateId == (int)MotiveStateNode.Confirmed
                    select a
                )
                .FirstOrDefaultAsync();
            }

            if (arista == null)
                return null;

            var nodes = await (
                from n in _context.StructureNodes.AsNoTracking()
                    .Include(i => i.AristasTo)
                    .Include(i => i.StructureNodoDefinitions)
                join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                let isNew = (
                        n.StructureNodoDefinitions.Count == 1 && n.AristasTo.Count == 1
                        ||
                            n.StructureNodoDefinitions.Where(d => d.MotiveStateId == (int)MotiveStateNode.Draft && d.ValidityFrom == request.Validity).ToList().Count == 1
                            && n.StructureNodoDefinitions.Where(d => d.MotiveStateId == (int)MotiveStateNode.Dropped && d.ValidityFrom == request.Validity).ToList().Count > 0
                            && n.AristasTo.Where(d => d.MotiveStateId == (int)MotiveStateNode.Confirmed).ToList().Count == 1
                )
                where
                    n.Id == request.NodeId &&
                    (
                        (nd.ValidityFrom >= request.Validity && nd.MotiveStateId == (int)MotiveStateNode.Confirmed) ||
                        nd.MotiveStateId == (int)MotiveStateNode.Draft
                    )
                select new
                {
                    n.Id,
                    n.Code,
                    n.LevelId,
                    NodeDefinitionId = nd.Id,
                    nd.Name,
                    nd.MotiveStateId,
                    nd.ValidityFrom,
                    IsNew = isNew
                }
            ).ToListAsync();


            var node = nodes.FirstOrDefault(c => c.MotiveStateId == (int)MotiveStateNode.Draft);

            if (node == null)
            {
                node = await (
                    from n in _context.StructureNodes.AsNoTracking()
                        .Include(i => i.AristasTo)
                        .Include(i => i.StructureNodoDefinitions)
                    join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                    let isNew = (n.StructureNodoDefinitions.Count == 1 && n.AristasTo.Count == 1)
                    where
                        n.Id == request.NodeId &&
                        nd.ValidityFrom <= request.Validity && nd.ValidityTo >= request.Validity &&
                        nd.MotiveStateId == (int)MotiveStateNode.Confirmed
                    select new
                    {
                        n.Id,
                        n.Code,
                        n.LevelId,
                        NodeDefinitionId = nd.Id,
                        nd.Name,
                        nd.MotiveStateId,
                        nd.ValidityFrom,
                        IsNew = isNew
                    }
                )
                .FirstOrDefaultAsync();
            }

            //if (node == null)
            //    return null;

            if (arista.MotiveStateId == (int)MotiveStateNode.Draft || node.MotiveStateId == (int)MotiveStateNode.Draft)
            {
                return new DTO.NodePendingDTO
                {
                    StructureId = request.StructureId,
                    AristaId = arista.Id,
                    NodeId = node.Id,
                    NodeName = node.Name,
                    NodeLevelId = node.LevelId,
                    NodeDefinitionId = node.NodeDefinitionId,
                    NodeParentId = arista.NodeIdFrom,
                    NodeMotiveStateId = node.MotiveStateId,
                    NodeValidityFrom = node.ValidityFrom,
                    AristaMotiveStateId = arista.MotiveStateId,
                    AristaValidityFrom = arista.ValidityFrom,
                    TypeVersion = (node.IsNew ? "N" : "B")
                };
            }
            else
                return null;
        }
    }
}
