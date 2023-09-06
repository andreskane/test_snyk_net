using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetStructureNodeRootQuery : IRequest<DTO.PortalStructureNodeDTO>
    {

        public int StructureId { get; set; }
    }

    public class GetStructureNodeRootQueryHandler : IRequestHandler<GetStructureNodeRootQuery, DTO.PortalStructureNodeDTO>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;

        public GetStructureNodeRootQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DTO.PortalStructureNodeDTO> Handle(GetStructureNodeRootQuery request, CancellationToken cancellationToken)
        {
            var structure = await (from est in _context.Structures.AsNoTracking().Include(m => m.StructureModel)
                                   where est.Id == request.StructureId
                                   select est).FirstOrDefaultAsync();

            var node = new DTO.PortalStructureNodeDTO
            {
                StructureId = structure.Id,
                StructureValidityFrom = structure.ValidityFrom,
                RootNodeId = structure.RootNodeId ?? 0,

                StructureModelID = structure.StructureModel.Id,
                StructureModelName = structure.StructureModel.Name,
                StructureModelShortName = structure.StructureModel.ShortName,
                StructureModelDescription = structure.StructureModel.Description,
                StructureModelActive = structure.StructureModel.Active.HasValue && structure.StructureModel.Active.Value
            };

            if (structure.RootNodeId.HasValue)
            {
                var nodeRoot = await (from nd in _context.StructureNodes.AsNoTracking()
                                      join ndn in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).Include(n => n.Role).Include(n => n.SaleChannel) on nd.Id equals ndn.NodeId
                                      where nd.Id == structure.RootNodeId.Value
                                      select new
                                      {
                                          NodeId = nd.Id,
                                          NodeName = ndn.Name,
                                          NodeCode = nd.Code,
                                          NodeActive = ndn.Active,
                                          NodeLevelId = nd.LevelId,

                                          NodeAttentionModeId = ndn.AttentionModeId,
                                          NodeAttentionModeName = ndn.AttentionModeId.HasValue ? ndn.AttentionMode.Name : null,

                                          NodeRolId = ndn.RoleId,
                                          NodeRolName = ndn.RoleId.HasValue ? ndn.Role.Name : null,

                                          NodeEmployeeId = ndn.EmployeeId,

                                          NodeSaleChannelId = ndn.SaleChannelId,
                                          NodeSaleChannelName = ndn.SaleChannelId.HasValue ? ndn.SaleChannel.Name : null,

                                          NodeValidityFrom = ndn.ValidityFrom,
                                          NodeValidityTo = ndn.ValidityTo

                                      }
                                        ).FirstOrDefaultAsync();


                if (nodeRoot != null)
                {
                    node.NodeId = nodeRoot.NodeId;
                    node.NodeName = nodeRoot.NodeName;
                    node.NodeCode = nodeRoot.NodeCode;
                    node.NodeActive = nodeRoot.NodeActive;
                    node.NodeLevelId = nodeRoot.NodeLevelId;

                    node.NodeAttentionModeId = nodeRoot.NodeAttentionModeId;
                    node.NodeAttentionModeName = nodeRoot.NodeAttentionModeName;

                    node.NodeRoleId = nodeRoot.NodeRolId;
                    node.NodeRoleName = nodeRoot.NodeRolName;

                    node.NodeEmployeeId = nodeRoot.NodeEmployeeId;

                    node.NodeSaleChannelId = nodeRoot.NodeSaleChannelId;
                    node.NodeSaleChannelName = nodeRoot.NodeSaleChannelName;

                    node.NodeValidityFrom = nodeRoot.NodeValidityFrom;
                    node.NodeValidityTo = nodeRoot.NodeValidityTo;
                }

                var nodesDraft = await _mediator.Send(new GetAllNodePendingChangesWithoutSavingQuery { StructureId = request.StructureId });
                var nodesFuture = await _mediator.Send(new GetAllNodePendingScheduledChangesQuery { StructureId = request.StructureId });
                var listNodePendiente = nodesDraft.Union(nodesFuture).ToList();

                var nodePendiente = listNodePendiente.FirstOrDefault(n => n.NodeId == nodeRoot.NodeId);

                if (nodePendiente != null)
                    node.VersionType = nodePendiente.TypeVersion;

            }

            return await Task.Run(() => node);
        }
    }
}
