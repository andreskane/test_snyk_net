using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllAristasSentTruckByVersionedIdQuery : IRequest<IList<NodePortalTruckDTO>>
    {
        public int VersionedId { get; set; }
    }

    public class GetAllAristasSentTruckByVersionedIdQueryHandler : IRequestHandler<GetAllAristasSentTruckByVersionedIdQuery, IList<NodePortalTruckDTO>>
    {
        private readonly IMediator _mediator;
        private readonly TruckACLContext _contextACL;
        private readonly StructureContext _context;
        private readonly IStructureNodePortalRepository _structureNodePortalRepository;

        public GetAllAristasSentTruckByVersionedIdQueryHandler(TruckACLContext contextACL,
                                                             StructureContext context,
                                                            IStructureNodePortalRepository structureNodePortalRepository,
                                                            IMediator mediator )
        {
            _context = context;
            _contextACL = contextACL;
            _structureNodePortalRepository = structureNodePortalRepository;
            _mediator = mediator;
        }

        public async Task<IList<NodePortalTruckDTO>> Handle(GetAllAristasSentTruckByVersionedIdQuery request, CancellationToken cancellationToken)
        {

            var list = new List<NodePortalTruckDTO>();
            
            var versioned = await _mediator.Send(new GetOneVersioneByIdQuery { VersionedId = request.VersionedId });

            var versionedAristasId = (from c in _contextACL.VersionedsArista
                                    where c.VersionedId == request.VersionedId
                                    select c
                                    ).Select(s => s.AristaId).ToList();

            var listAristas = (from a in _context.StructureAristas.Include(nd => nd.NodeTo).Include(nh => nh.NodeFrom).AsNoTracking()
                               where versionedAristasId.Contains(a.Id)
                               select new 
                               {
                                   AristaId = a.Id,
                                   a.NodeIdFrom,
                                   a.NodeIdTo
                               }
                   ).ToList();

            foreach (var item in listAristas)
            {
                var nodeMaxParent = await _mediator.Send(new GetOneNodeMaxVersionByIdQuery { StructureId = versioned.StructureId, NodeId = item.NodeIdFrom, ValidityFrom = versioned.Validity });
                var nodeParent = await _mediator.Send(new GetOneNodeQuery { StructureId = versioned.StructureId, NodeId = item.NodeIdFrom, ValidityFrom = nodeMaxParent.ValidityFrom });

                var nodeMax = await _mediator.Send(new GetOneNodeMaxVersionByIdQuery { StructureId = versioned.StructureId, NodeId = item.NodeIdTo, ValidityFrom = versioned.Validity});
                var node = await _mediator.Send(new GetOneNodeQuery { StructureId = versioned.StructureId, NodeId = item.NodeIdTo, ValidityFrom = nodeMax.ValidityFrom });

                var nodePortal = new NodePortalTruckDTO
                {
                    NodeId = node.NodeId,
                    Name = node.NodeName,
                    ActiveNode = node.NodeActive.Value,
                    Code = node.NodeCode,
                    LevelId = node.NodeLevelId,
                    AttentionModeId = node.NodeAttentionModeId,
                    RoleId = node.NodeRoleId,
                    SaleChannelId = node.NodeSaleChannelId,
                    EmployeeId = node.NodeEmployeeId,
                    ValidityFrom = node.NodeValidityFrom,
                    ValidityTo = node.NodeValidityTo,
                    IsRootNode = false,
                    NodeIdParent = nodeParent.NodeId,
                    ParentNodeCode = nodeParent.NodeCode,
                    ChildNodeCode = null,
                    NodeDefinitionId = node.NodeDefinitionId
                };
                list.Add(nodePortal);


                if (nodeParent.NodeLevelId == 6 && nodeParent.NodeParentId.HasValue) //Es Jefatura
                {
                    var nodeMaxParentJef = await _mediator.Send(new GetOneNodeMaxVersionByIdQuery { StructureId = versioned.StructureId, NodeId = nodeParent.NodeParentId.Value, ValidityFrom = versioned.Validity });
                    var nodeParentJet = await _mediator.Send(new GetOneNodeQuery { StructureId = versioned.StructureId, NodeId = nodeParent.NodeParentId.Value, ValidityFrom = nodeMaxParent.ValidityFrom });

                    var nodePortalJef = new NodePortalTruckDTO
                    {
                        NodeId = nodeParent.NodeId,
                        Name = nodeParent.NodeName,
                        ActiveNode = nodeParent.NodeActive.Value,
                        Code = nodeParent.NodeCode,
                        LevelId = nodeParent.NodeLevelId,
                        AttentionModeId = nodeParent.NodeAttentionModeId,
                        RoleId = nodeParent.NodeRoleId,
                        SaleChannelId = nodeParent.NodeSaleChannelId,
                        EmployeeId = nodeParent.NodeEmployeeId,
                        ValidityFrom = nodeParent.NodeValidityFrom,
                        ValidityTo = nodeParent.NodeValidityTo,
                        IsRootNode = false,
                        NodeIdParent = nodeParentJet.NodeId,
                        ParentNodeCode = nodeParentJet.NodeCode,
                        ChildNodeCode = null,
                        NodeDefinitionId = nodeParent.NodeDefinitionId
                   
                    };

                    nodePortalJef.ChildNodes.Add(nodePortal);

                    list.Add(nodePortalJef);
                }

            }

            return list;
        }
    }
}
