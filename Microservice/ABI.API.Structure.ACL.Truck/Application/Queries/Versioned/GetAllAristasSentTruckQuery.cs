using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllAristasSentTruckQuery : IRequest<IList<PortalAristalDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllAristasSentTruckQueryHandler : IRequestHandler<GetAllAristasSentTruckQuery, IList<PortalAristalDTO>>
    {
        private readonly IStructureNodePortalRepository _structureNodePortalRepository;

        public GetAllAristasSentTruckQueryHandler(IStructureNodePortalRepository structureNodePortalRepository)
        {
            _structureNodePortalRepository = structureNodePortalRepository;
        }

        public async Task<IList<PortalAristalDTO>> Handle(GetAllAristasSentTruckQuery request, CancellationToken cancellationToken)
        {

            return await _structureNodePortalRepository.GetAllAristasGradeChangesForTruck(request.StructureId, request.ValidityFrom);
        }
    }
}
