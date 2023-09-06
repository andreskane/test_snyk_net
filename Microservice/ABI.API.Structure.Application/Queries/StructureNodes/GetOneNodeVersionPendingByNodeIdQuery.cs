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
    public class GetOneNodeVersionPendingByNodeIdQuery : IRequest<DTO.StructureNodeDTO>
    {
        public int StructureId { get; set; }

        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetOneNodeVersionPendingByNodeIdQueryHandler : IRequestHandler<GetOneNodeVersionPendingByNodeIdQuery, DTO.StructureNodeDTO>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;


        public GetOneNodeVersionPendingByNodeIdQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DTO.StructureNodeDTO> Handle(GetOneNodeVersionPendingByNodeIdQuery request, CancellationToken cancellationToken)
        {
            //nodo tipo cambio sin guardar
            var nodeVersion = await _mediator.Send(new GetOneNodePendingChangesWithoutSavingQuery { StructureId = request.StructureId, NodeId = request.NodeId, Validity = request.ValidityFrom });

            //nodo tipo cambio programado
            if (nodeVersion == null) 
                nodeVersion = await _mediator.Send(new GetOneNodePendingScheduledChangesQuery { StructureId = request.StructureId, NodeId = request.NodeId, ValidityFrom = request.ValidityFrom });

            if (nodeVersion != null)
            {
                var node = await (
                    from n in _context.StructureNodes.AsNoTracking()
                        .Include(n => n.Level)
                    join nd in _context.StructureNodeDefinitions.AsNoTracking()
                        .Include(n => n.AttentionMode)
                        .Include(n => n.Role)
                        .Include(n => n.SaleChannel) on n.Id equals nd.NodeId
                    where
                        nd.Id == nodeVersion.NodeDefinitionId
                    select new DTO.StructureNodeDTO
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
                        NodeEmployeeId = nd.VacantPerson.HasValue ? (nd.VacantPerson.Value ? null : nd.EmployeeId) : (int?)null,
                        NodeEmployeeName = string.Empty,
                        NodeValidityFrom = (
                            nodeVersion.NodeValidityFrom > nodeVersion.AristaValidityFrom
                                ? nodeVersion.NodeValidityFrom
                                : nodeVersion.AristaValidityFrom
                        ),
                        NodeParentId = nodeVersion.NodeParentId,
                        VersionType = nodeVersion.TypeVersion
                    })
                    .FirstOrDefaultAsync();

                return node;
            }

            return new DTO.StructureNodeDTO
            {
                VersionType = string.Empty
            };
        }
    }
}
