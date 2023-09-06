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
    public class GetAllStructureNodesQuery : IRequest<List<DTO.StructureNodeDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? Active { get; set; }
    }

    public class GetAllStructureNodesQueryHandler : IRequestHandler<GetAllStructureNodesQuery, List<DTO.StructureNodeDTO>>
    {
        private readonly IMediator _mediator;
        private readonly StructureContext _context;

        public GetAllStructureNodesQueryHandler(IMediator mediator, StructureContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<List<DTO.StructureNodeDTO>> Handle(GetAllStructureNodesQuery request, CancellationToken cancellationToken)
        {
            var listNodes = await _mediator.Send(new GetAllNodeByStructureIdQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });
            var roles = await _context.Roles.AsNoTracking().ToListAsync();
            var attentionModes = await _context.AttentionMode.AsNoTracking().ToListAsync();
            var saleChannels = await _context.SalesChannels.AsNoTracking().ToListAsync();

            var query = (
                from n in listNodes
                let r = roles.FirstOrDefault(f => f.Id == n.NodeRoleId)
                let a = attentionModes.FirstOrDefault(f => f.Id == n.NodeAttentionModeId)
                let s = saleChannels.FirstOrDefault(f => f.Id == n.NodeSaleChannelId)
                where
                    (
                        n.IsNew &&
                        (!n.AristaValidityTo.HasValue || n.AristaValidityTo.Value.ToUniversalTime().Date > DateTimeOffset.UtcNow.Date) &&
                        n.NodeValidityFrom.ToUniversalTime().Date > DateTimeOffset.UtcNow.Date
                    )
                    ||
                    (
                        n.AristaMotiveStateId == (int)MotiveStateNode.Confirmed &&
                        (
                            (
                                n.NodeMotiveStateId == (int)MotiveStateNode.Confirmed &&
                                n.NodeValidityFrom <= request.ValidityFrom && n.NodeValidityTo >= request.ValidityFrom
                            )
                            ||
                            (
                                n.NodeMotiveStateId == (int)MotiveStateNode.Draft 
                            )
                        )
                        || n.AristaMotiveStateId == (int)MotiveStateNode.Draft
                    )
                select new DTO.StructureNodeDTO
                {
                    NodeId = n.NodeId,
                    NodeName = n.NodeName,
                    NodeCode = n.NodeCode,
                    NodeActive = n.NodeActive,
                    NodeLevelId = n.NodeLevelId,

                    NodeAttentionModeId = n.NodeAttentionModeId,
                    NodeAttentionModeName = n.NodeAttentionModeId.HasValue ? a.Name : null,

                    NodeRoleId = n.NodeRoleId,
                    NodeRoleName = n.NodeRoleId.HasValue ? r.Name : null,

                    NodeEmployeeId = n.NodeVacantPerson.HasValue ? (n.NodeVacantPerson.Value ? null : n.NodeEmployeeId) : (int?)null,

                    NodeSaleChannelId = n.NodeSaleChannelId,
                    NodeSaleChannelName = n.NodeSaleChannelId.HasValue ? s.Name : null,

                    NodeValidityFrom = n.NodeValidityFrom,
                    NodeValidityTo = n.NodeValidityTo,
                    NodeMotiveStateId = n.NodeMotiveStateId,

                    AristaMotiveStateId = n.AristaMotiveStateId,
                    AristaChildMotiveStateId = n.AristaChildMotiveStateId,
                    AristaValidityFrom = n.AristaValidityFrom,
                    AristaValidityTo = n.AristaValidityTo,
                    IsNew = n.IsNew,

                    NodeParentId = n.NodeIdParent,
                    ContainsNodeId = n.NodeIdTo
                }
            );

            if (request.Active.HasValue)
                query = query.Where(w =>
                    w.NodeActive == request.Active.Value
                );

            var results = query
                    .Distinct()
                    .OrderBy(o => o.NodeLevelId)
                    .ThenBy(o => o.NodeId)
                    .ToList();

            return results;
        }
    }
}