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
    public class GetOneNodePendingScheduledChangesQuery : IRequest<DTO.NodePendingDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetOneNodePendingScheduledChangesQueryHandler : IRequestHandler<GetOneNodePendingScheduledChangesQuery, DTO.NodePendingDTO>
    {
        private readonly StructureContext _context;


        public GetOneNodePendingScheduledChangesQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DTO.NodePendingDTO> Handle(GetOneNodePendingScheduledChangesQuery request, CancellationToken cancellationToken)
        {

            var nodeRoot = await (from n in _context.StructureNodes.AsNoTracking()
                  .Include(i => i.StructureNodoDefinitions)
                            join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                            join s in _context.Structures.AsNoTracking() on n.Id equals s.RootNodeId
                            where
                             n.Id == request.NodeId &&
                             (
                                  nd.ValidityFrom > request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom &&
                                  nd.MotiveStateId == (int)MotiveStateNode.Confirmed
                             )
                            select new
                            {
                                n.Id,
                                n.Code,
                                n.LevelId,
                                NodeDefinitionId = nd.Id,
                                nd.Name,
                                nd.MotiveStateId,
                                nd.ValidityFrom
                            }
                  )
                  .FirstOrDefaultAsync(cancellationToken: cancellationToken);


            if (nodeRoot != null && nodeRoot.ValidityFrom > request.ValidityFrom)

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
                    TypeVersion = "F"
                };
            }



            var arista = await (
                from n in _context.StructureNodes.AsNoTracking()
                join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                where
                    n.Id == request.NodeId &&
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    a.ValidityFrom > request.ValidityFrom &&
                    a.MotiveStateId == (int)MotiveStateNode.Confirmed
                select a
            )
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (arista == null)
            {
                arista = await (
                    from n in _context.StructureNodes.AsNoTracking()
                    join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                    where
                        n.Id == request.NodeId &&
                        a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                        a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom &&
                        a.MotiveStateId == (int)MotiveStateNode.Confirmed
                    select a
                )
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            }

            if (arista == null)
                return null;

            var node = await (
                       from n in _context.StructureNodes.AsNoTracking()
                       join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                       where
                           n.Id == request.NodeId &&
                           nd.ValidityFrom > request.ValidityFrom &&
                           nd.MotiveStateId == (int)MotiveStateNode.Confirmed
                       select new
                       {
                           n.Id,
                           n.Code,
                           n.LevelId,
                           NodeDefinitionId = nd.Id,
                           nd.Name,
                           nd.MotiveStateId,
                           nd.ValidityFrom
                       }
               ).OrderBy(o=>o.ValidityFrom)
               .FirstOrDefaultAsync(cancellationToken: cancellationToken);



            if (node == null)
            {

                 node = await (
                    from n in _context.StructureNodes.AsNoTracking()
                    join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                    where
                        n.Id == request.NodeId &&
                        nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom &&
                        nd.MotiveStateId == (int)MotiveStateNode.Confirmed
                    select new
                    {
                        n.Id,
                        n.Code,
                        n.LevelId,
                        NodeDefinitionId = nd.Id,
                        nd.Name,
                        nd.MotiveStateId,
                        nd.ValidityFrom
                    }
                    ) .FirstOrDefaultAsync(cancellationToken: cancellationToken);


            }

            if (node == null)
                return null;

            if (arista.ValidityFrom > request.ValidityFrom || node.ValidityFrom > request.ValidityFrom)
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
                    TypeVersion = "F"
                };
            }
            else
                return null;
        }
    }
}
