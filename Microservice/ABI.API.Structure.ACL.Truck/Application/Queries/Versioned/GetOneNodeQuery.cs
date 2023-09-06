﻿using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneNodeQuery : IRequest<DTO.PortalStructureNodeDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
    }

    public class GetOneNodeQueryHandler : IRequestHandler<GetOneNodeQuery, DTO.PortalStructureNodeDTO>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;


        public GetOneNodeQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DTO.PortalStructureNodeDTO> Handle(GetOneNodeQuery request, CancellationToken cancellationToken)
        {
            var nodeMaxVersion = await _mediator.Send(new GetOneNodeMaxVersionByIdQuery { StructureId = request.StructureId, NodeId = request.NodeId, ValidityFrom = request.ValidityFrom });

            if (nodeMaxVersion == null)
                return null;

            var node = (from n in _context.StructureNodes.AsNoTracking().Include(n => n.Level)
                        join nd in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).Include(n => n.Role).Include(n => n.SaleChannel)
                            on n.Id equals nd.NodeId
                        from a in _context.StructureAristas.AsNoTracking().Where(a => a.NodeIdTo == nd.NodeId).DefaultIfEmpty()
                        where n.Id == request.NodeId && nd.ValidityFrom == nodeMaxVersion.ValidityFrom  && nd.MotiveStateId == (int)MotiveStateNode.Confirmed
                        select new DTO.PortalStructureNodeDTO
                        {
                            StructureId = request.StructureId,
                            NodeId = n.Id,
                            NodeCode = n.Code,
                            NodeName = nd.Name.ToUpper(),
                            NodeActive = nd.Active,
                            NodeDefinitionId = nd.Id,
                            NodeLevelId = n.LevelId,
                            NodeLevelName = n.Level.Name,
                            NodeAttentionModeId = nd.AttentionModeId,
                            NodeAttentionModeName = nd.AttentionModeId.HasValue ? nd.AttentionMode.Name : null,
                            NodeRoleId = nd.RoleId,
                            NodeRoleName = nd.RoleId.HasValue ? nd.Role.Name : null,
                            NodeSaleChannelId = nd.SaleChannelId,
                            NodeSaleChannelName = nd.SaleChannelId.HasValue ? nd.SaleChannel.Name : null,
                            NodeEmployeeId = nd.VacantPerson.HasValue ? (nd.VacantPerson.Value ? null : nd.EmployeeId) : null,
                            NodeEmployeeName = string.Empty,
                            NodeValidityFrom = nd.ValidityFrom,
                            NodeValidityTo = nd.ValidityTo,
                            NodeParentId = a.NodeIdFrom
                        }).FirstOrDefault();

            return await Task.Run(() => node);
        }

    }
}
