using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllNodeByStructureIdQuery : IRequest<IList<NodeAristaDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool Active { get; set; }
    }

    public class GetAllNodeByStructureIdQueryHandler : IRequestHandler<GetAllNodeByStructureIdQuery, IList<NodeAristaDTO>>
    {
        private readonly StructureContext _context;

        public GetAllNodeByStructureIdQueryHandler(IConfiguration configuration, StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<NodeAristaDTO>> Handle(GetAllNodeByStructureIdQuery request, CancellationToken cancellationToken)
        {
            var root = await (
                from e in _context.Structures.AsNoTracking()
                where
                    e.Id == request.StructureId
                select e.RootNodeId
            )
            .FirstOrDefaultAsync();

            var roots = await (
                from nd in _context.StructureNodeDefinitions.AsNoTracking()
                join a in _context.StructureAristas.AsNoTracking() on nd.NodeId equals a.NodeIdFrom
                from ah in _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == a.NodeIdFrom)
                    .DefaultIfEmpty()
                let isNew =
                (
                    _context.StructureAristas
                        .Any(aa => aa.NodeIdTo == nd.NodeId)
                    &&
                    _context.StructureAristas
                    .Where(w => w.NodeIdTo == nd.NodeId)
                    .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                ) ||
                (
                    _context.StructureAristas
                        .Any(aa => aa.NodeIdTo == a.NodeIdTo)
                    &&
                    _context.StructureAristas
                    .Where(w => w.NodeIdTo == a.NodeIdTo)
                    .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                )
                where
                 nd.NodeId == root &&
                   a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                   (
                       (
                           a.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                           a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom
                       )
                       ||
                       (
                           a.MotiveStateId == (int)MotiveStateNode.Draft
                       )
                   )
                   &&
                   (
                       isNew ||
                       (
                           nd.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                           nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom
                       )
                   )

                select new NodeAristaDTO
                {
                    StructureId = a.StructureIdFrom,
                    NodeId = nd.NodeId,
                    NodeCode = nd.Node.Code,
                    NodeLevelId = nd.Node.LevelId,
                    NodeName = nd.Name,
                    NodeActive = nd.Active,
                    NodeRoleId = nd.RoleId,
                    NodeAttentionModeId = nd.AttentionModeId,
                    NodeSaleChannelId = nd.SaleChannelId,
                    NodeVacantPerson = nd.VacantPerson,
                    NodeEmployeeId = nd.EmployeeId,
                    NodeValidityFrom = nd.ValidityFrom,
                    NodeValidityTo = nd.ValidityTo,
                    NodeMotiveStateId = nd.MotiveStateId,
                    NodeIdParent = null,
                    NodeIdTo = a.NodeIdTo,
                    AristaMotiveStateId = a.MotiveStateId,
                    AristaChildMotiveStateId = null,
                    AristaValidityFrom = ah.ValidityFrom,
                    AristaValidityTo = ah.ValidityTo,
                    IsNew = isNew
                }
            )
            .Distinct()
            .ToListAsync();

            var query = await (
                from nd in _context.StructureNodeDefinitions.AsNoTracking().Where(c => c.MotiveStateId == (int)MotiveStateNode.Draft || c.MotiveStateId == (int)MotiveStateNode.Confirmed)
                join a in _context.StructureAristas.AsNoTracking() on nd.NodeId equals a.NodeIdTo
                from ah in _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdFrom == a.NodeIdTo)
                    .DefaultIfEmpty()
                let isNew =
                (
                    _context.StructureAristas.AsNoTracking()
                        .Any(aa => aa.NodeIdTo == nd.NodeId)
                    &&
                    _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == nd.NodeId)
                    .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                ) ||
                (
                    _context.StructureAristas.AsNoTracking()
                        .Any(aa => aa.NodeIdTo == ah.NodeIdTo)
                    &&
                    _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == ah.NodeIdTo)
                    .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                )
                ||
                (
                    _context.StructureAristas
                        .Any(aa => aa.NodeIdTo == nd.NodeId)
                    &&
                    _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == nd.NodeId)
                    .Count(c=>c.MotiveStateId == (int)MotiveStateNode.Confirmed) == 1
                    &&
                    _context.StructureNodeDefinitions.AsNoTracking()
                    .Where(w => w.NodeId == nd.NodeId && (w.MotiveStateId == (int)MotiveStateNode.Draft || w.MotiveStateId == (int)MotiveStateNode.Confirmed))
                    .All(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                )
                where
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    (
                        (
                            a.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                            a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom
                        )
                        ||
                        (
                            a.MotiveStateId == (int)MotiveStateNode.Draft
                        )
                    )
                 
                    &&
                    (
                        isNew ||
                        (
                            ((a.MotiveStateId == (int)MotiveStateNode.Confirmed || nd.MotiveStateId == (int)MotiveStateNode.Draft) &&
                            nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom)
                        )
                    )
                select new NodeAristaDTO
                {
                    StructureId = a.StructureIdFrom,
                    NodeId = nd.NodeId,
                    NodeCode = nd.Node.Code,
                    NodeLevelId = nd.Node.LevelId,
                    NodeName = nd.Name,
                    NodeActive = nd.Active,
                    NodeRoleId = nd.RoleId,
                    NodeAttentionModeId = nd.AttentionModeId,
                    NodeSaleChannelId = nd.SaleChannelId,
                    NodeVacantPerson = nd.VacantPerson,
                    NodeEmployeeId = nd.EmployeeId,
                    NodeValidityFrom = nd.ValidityFrom,
                    NodeValidityTo = nd.ValidityTo,
                    NodeMotiveStateId = nd.MotiveStateId,
                    NodeIdParent = a.NodeIdFrom,
                    NodeIdTo = ah.NodeIdTo,
                    AristaMotiveStateId = a.MotiveStateId,
                    AristaChildMotiveStateId = ah.MotiveStateId,
                    AristaValidityFrom = ah.ValidityFrom,
                    AristaValidityTo = ah.ValidityTo,
                    IsNew = isNew
                }
            )
            .ToListAsync();

            var results = roots.Union(query)
                .ToList();

            return results;
        }
    }
}
