using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations.Interface
{
    public interface ICompareStructuresTruckAndPortal
    {
        Task<StructurePortalCompareDTO> CompareTruckToPortal(string name, TruckStructure truck, StructureNodeDTO portal, List<ResourceDTO> resources);
    }
}