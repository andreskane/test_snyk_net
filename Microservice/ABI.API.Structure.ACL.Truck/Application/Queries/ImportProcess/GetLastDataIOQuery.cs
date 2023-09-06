using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries
{


    public class GetLastDataIOQuery : IRequest<IList<DataIODto>>
    {

        public string Country { get; set; }
    }

    public class GetLastDataIOQueryHandler : IRequestHandler<GetLastDataIOQuery, IList<DataIODto>>
    {
        private readonly IMapper _mapper;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IPortalService _portalService;
        private readonly IStructureClientRepository _structureClientRepository;

        public GetLastDataIOQueryHandler(
            IMapper mapper, IMapeoTableTruckPortal mapeoTableTruckPortal,
            IPortalService portalService, IStructureClientRepository structureClientRepository
        )
        {
            _mapper = mapper;
            _mapeoTableTruckPortal = mapeoTableTruckPortal;
            _portalService = portalService;
            _structureClientRepository = structureClientRepository;
        }

        public async Task<IList<DataIODto>> Handle(GetLastDataIOQuery request, CancellationToken cancellationToken)
        {
            var mapeoTruck = await _mapeoTableTruckPortal.GetOneBusinessTruckPortal(request.Country.Substring(0, 2));
            if (mapeoTruck != null)
            {
                var structure = await _portalService.GetStrucureByName(mapeoTruck.Name);
                return _mapper.Map<IList<StructureClientNode>, IList<DataIODto>>(await _structureClientRepository.GetAllCurrentByStructureIdWithOutTracking(structure.Id, cancellationToken));
            }
            else {
                return new List<DataIODto>();
            }
            
        }
    }
}
