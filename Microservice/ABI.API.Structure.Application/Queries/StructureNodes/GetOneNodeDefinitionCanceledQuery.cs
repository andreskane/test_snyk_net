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
    public class GetOneNodeDefinitionCanceledQuery : IRequest<DTO.StructureNodeDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetOneNodeCanceledQueryHandler : IRequestHandler<GetOneNodeDefinitionCanceledQuery, DTO.StructureNodeDTO>
    {
        private readonly StructureContext _context;

        public GetOneNodeCanceledQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DTO.StructureNodeDTO> Handle(GetOneNodeDefinitionCanceledQuery request, CancellationToken cancellationToken)
        {
            var node = (from n in _context.StructureNodes.AsNoTracking().Include(n => n.Level)
                        join nd in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).Include(n => n.Role).Include(n => n.SaleChannel)
                            on n.Id equals nd.NodeId
                        where (n.Id == request.NodeId && nd.MotiveStateId == (int)MotiveStateNode.Dropped && nd.ValidityFrom == request.ValidityFrom)
                        select new DTO.StructureNodeDTO
                        {
                            StructureId = request.StructureId,
                            NodeId = n.Id,
                            NodeCode = n.Code,
                            NodeName = nd.Name,
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
                            NodeEmployeeId = nd.VacantPerson.HasValue ? (nd.VacantPerson.Value ? null : nd.EmployeeId) : (int?)null,
                            NodeEmployeeName = string.Empty,
                            NodeValidityFrom = nd.ValidityFrom,
                            NodeValidityTo = nd.ValidityTo,
                            NodeMotiveStateId = nd.MotiveStateId
                        }).FirstOrDefault();

            return await Task.Run(() => node);
        }
    }
}
