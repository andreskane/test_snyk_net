using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllNodesSentTruckByVersionedIdQuery : IRequest<IList<NodePortalTruckDTO>>
    {
        public int VersionedId { get; set; }
    }

    public class GetAllNodesSentTruckByVersionedIdQueryHandler : IRequestHandler<GetAllNodesSentTruckByVersionedIdQuery, IList<NodePortalTruckDTO>>
    {
        private readonly IMediator _mediator;
        private readonly TruckACLContext _contextACL;
        private readonly StructureContext _context;
        private readonly IStructureNodePortalRepository _structureNodePortalRepository;

        public GetAllNodesSentTruckByVersionedIdQueryHandler(TruckACLContext contextACL,
                                                             StructureContext context,
                                                            IStructureNodePortalRepository structureNodePortalRepository,
                                                            IMediator mediator )
        {
            _context = context;
            _contextACL = contextACL;
            _structureNodePortalRepository = structureNodePortalRepository;
            _mediator = mediator;
        }

        public async Task<IList<NodePortalTruckDTO>> Handle(GetAllNodesSentTruckByVersionedIdQuery request, CancellationToken cancellationToken)
        {

            var versioned = await _mediator.Send(new GetOneVersioneByIdQuery { VersionedId = request.VersionedId });

            var versionedNodesId = (from c in _contextACL.VersionedsNode
                                    where c.VersionedId == request.VersionedId
                                    select c
                                    ).Select(s => s.NodeDefinitionId).ToList();

            IQueryable < NodePortalTruckDTO > query1 = (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                        join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                        from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                        join np in _context.StructureNodes.AsNoTracking() on a.NodeIdFrom equals np.Id
                        where versionedNodesId.Contains(nd.Id) && a.ValidityFrom <= versioned.Validity && a.ValidityTo >= versioned.Validity && a.MotiveStateId == (int)MotiveStateNode.Confirmed
                                                        select new NodePortalTruckDTO
                         {
                             NodeId = nd.NodeId,
                             Name = nd.Name,
                             ActiveNode = nd.Active,
                             Code = n.Code,
                             LevelId = n.LevelId,
                             AttentionModeId = nd.AttentionModeId,
                             RoleId = nd.RoleId,
                             SaleChannelId = nd.SaleChannelId,
                             EmployeeId = nd.EmployeeId,
                             ValidityFrom = nd.ValidityFrom,
                             ValidityTo = nd.ValidityTo,
                             IsRootNode = false,
                             NodeIdParent = a.NodeIdFrom,
                             ParentNodeCode = np.Code,
                             ChildNodeCode = null,
                             NodeDefinitionId = nd.Id
                         }).Distinct();

            var query1List1 = query1.ToList();

            var nodesQuery1 = query1List1.Select(n => n.NodeId).Distinct().ToList();

            IQueryable<NodePortalTruckDTO> query2 = (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                     join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                     from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdFrom).DefaultIfEmpty()
                                                     where versionedNodesId.Contains(nd.Id) && a.ValidityFrom <= versioned.Validity && a.ValidityTo >= versioned.Validity && a.MotiveStateId == (int)MotiveStateNode.Confirmed
                                                     select new NodePortalTruckDTO
                                                     {
                                                         NodeId = nd.NodeId,
                                                         Name = nd.Name,
                                                         ActiveNode = nd.Active,
                                                         Code = n.Code,
                                                         LevelId = n.LevelId,
                                                         AttentionModeId = nd.AttentionModeId,
                                                         RoleId = nd.RoleId,
                                                         SaleChannelId = nd.SaleChannelId,
                                                         EmployeeId = nd.EmployeeId,
                                                         ValidityFrom = nd.ValidityFrom,
                                                         ValidityTo = nd.ValidityTo,
                                                         IsRootNode = true,
                                                         NodeIdParent = 0,
                                                         ParentNodeCode = string.Empty,
                                                         ChildNodeCode = null,
                                                         NodeDefinitionId = nd.Id
                                                     }).Distinct();


            var query1List2 = query2.Where(x => !nodesQuery1.Contains(x.NodeId)).ToList();
            var listNodes = query1List1.Union(query1List2).ToList();

            foreach (var item in listNodes)
            {
                switch (item.LevelId)
                {
                    case 6: //Jefatura
                        var child = await _structureNodePortalRepository.GetAllChildNodeForTruck(versioned.StructureId, item.NodeId, versioned.Validity);

                        item.ChildNodes = child.ToList();
                        break;
                }
            }

            return listNodes;
        }
    }
}
