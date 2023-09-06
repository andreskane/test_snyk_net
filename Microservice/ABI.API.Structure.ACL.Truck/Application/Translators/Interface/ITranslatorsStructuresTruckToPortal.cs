using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations.Interface
{
    public interface ITranslatorsStructuresTruckToPortal
    {
        Task<StructurePortalDTO> TruckToPortal(TruckStructure truck, string name, List<ResourceDTO> resources);
    }
}