using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllNodesSentTruckQuery : IRequest<IList<NodePortalTruckDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllNodesSentTruckQueryHandler : IRequestHandler<GetAllNodesSentTruckQuery, IList<NodePortalTruckDTO>>
    {
        private readonly IStructureNodePortalRepository _structureNodePortalRepository;

        public GetAllNodesSentTruckQueryHandler(IStructureNodePortalRepository structureNodePortalRepository)
        {
            _structureNodePortalRepository = structureNodePortalRepository;
        }

        public async Task<IList<NodePortalTruckDTO>> Handle(GetAllNodesSentTruckQuery request, CancellationToken cancellationToken)
        {
            var nodes = await _structureNodePortalRepository.GetAllGradeChangesForTruck(request.StructureId, request.ValidityFrom);

            foreach (var item in nodes)
            {
                switch (item.LevelId)
                {
                    case 6: //Jefatura
                        var child = await _structureNodePortalRepository.GetAllChildNodeForTruck(request.StructureId, item.NodeId, request.ValidityFrom);

                        item.ChildNodes = child.ToList();
                        break;
                }
            }

            return nodes;
        }
    }
}
