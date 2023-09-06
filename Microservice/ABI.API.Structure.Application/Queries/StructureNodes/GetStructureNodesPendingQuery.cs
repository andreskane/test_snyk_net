﻿using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureNodesPendingQuery : IRequest<IList<DTO.StructureNodeDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetStructureNodesPendingQueryHandler : IRequestHandler<GetStructureNodesPendingQuery, IList<DTO.StructureNodeDTO>>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;


        public GetStructureNodesPendingQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<DTO.StructureNodeDTO>> Handle(GetStructureNodesPendingQuery request, CancellationToken cancellationToken)
        {
            var nodesDraft = await _mediator.Send(new GetAllNodePendingChangesWithoutSavingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });
            var nodesFuture = await _mediator.Send(new GetAllNodePendingScheduledChangesQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            var listNodePendiente = nodesDraft.Union(nodesFuture);

            var structureQuery = (
                from jer in listNodePendiente
                join ndn in _context.StructureNodeDefinitions.AsNoTracking()
                    .Include(n => n.AttentionMode)
                    .Include(n => n.Role)
                    .Include(n => n.SaleChannel) on jer.NodeDefinitionId equals ndn.Id
                join est in _context.Structures.AsNoTracking()
                    .Include(e => e.StructureModel) on jer.StructureId equals est.Id
                where
                    est.Id == request.StructureId
                select new DTO.StructureNodeDTO
                {
                    StructureId = est.Id,
                    StructureValidityFrom = est.ValidityFrom,
                    RootNodeId = est.RootNodeId ?? 0,

                    StructureModelID = est.StructureModel.Id,
                    StructureModelName = est.StructureModel.Name,
                    StructureModelShortName = est.StructureModel.ShortName,
                    StructureModelDescription = est.StructureModel.Description,
                    StructureModelActive = est.StructureModel.Active.HasValue && est.StructureModel.Active.Value,

                    NodeId = jer.NodeId,
                    NodeName = ndn.Name.ToUpper(),
                    NodeCode = jer.NodeCode,
                    NodeActive = ndn.Active,
                    NodeLevelId = jer.NodeLevelId,
                    NodeParentId = jer.NodeParentId,

                    NodeAttentionModeId = ndn.AttentionModeId,
                    NodeAttentionModeName = ndn.AttentionModeId.HasValue ? ndn.AttentionMode.Name : null,

                    NodeRoleId = ndn.RoleId,
                    NodeRoleName = ndn.RoleId.HasValue ? ndn.Role.Name : null,

                    NodeEmployeeId = ndn.VacantPerson.HasValue ? (ndn.VacantPerson.Value ? null : ndn.EmployeeId) : (int?)null,

                    NodeSaleChannelId = ndn.SaleChannelId,
                    NodeSaleChannelName = ndn.SaleChannelId.HasValue ? ndn.SaleChannel.Name : null,

                    NodeValidityFrom = ndn.ValidityFrom,
                    NodeValidityTo = ndn.ValidityTo,

                    NodeDefinitionId = ndn.Id,
                    NodeMotiveStateId = ndn.MotiveStateId,

                    AristaMotiveStateId = jer.AristaMotiveStateId,
                    AristaValidityFrom = jer.AristaValidityFrom,
                    AristaValidityTo = jer.AristaValidityTo,

                    VersionType = jer.TypeVersion
                })
                .OrderBy(s => s.NodeLevelId)
                .ThenBy(a => a.NodeId)
                .ToList();

            return structureQuery;
        }
    }
}
