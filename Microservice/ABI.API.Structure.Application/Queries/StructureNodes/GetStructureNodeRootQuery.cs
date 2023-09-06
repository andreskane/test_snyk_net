using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureNodeRootQuery : IRequest<DTO.StructureNodeDTO>
    {

        public int StructureId { get; set; }
    }

    public class GetStructureNodeRootQueryHandler : IRequestHandler<GetStructureNodeRootQuery, DTO.StructureNodeDTO>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;

        public GetStructureNodeRootQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DTO.StructureNodeDTO> Handle(GetStructureNodeRootQuery request, CancellationToken cancellationToken)
        {
            var structure = await (
                from est in _context.Structures.AsNoTracking().Include(m => m.StructureModel)
                where est.Id == request.StructureId
                select est
            )
            .FirstOrDefaultAsync();

            var node = new DTO.StructureNodeDTO
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
                var nodeRoot = await (
                    from nd in _context.StructureNodes.AsNoTracking()
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

                        NodeEmployeeId = ndn.VacantPerson.HasValue ? (ndn.VacantPerson.Value ? null : ndn.EmployeeId) : (int?)null,

                        NodeSaleChannelId = ndn.SaleChannelId,
                        NodeSaleChannelName = ndn.SaleChannelId.HasValue ? ndn.SaleChannel.Name : null,

                        NodeValidityFrom = ndn.ValidityFrom,
                        NodeValidityTo = ndn.ValidityTo,

                        VersionType = (ndn.MotiveStateId == (int)MotiveStateNode.Draft ? "N" : string.Empty)
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

                    node.VersionType = nodeRoot.VersionType;
                }
            }

            return node;
        }
    }
}
