using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Translators.Interface
{
    public interface ITranslatorsStructuresPortalToTruck
    {
        Task<StructureTruck> PortalToTruckAsync(OpecpiniOut ini, int structureId, List<NodePortalTruckDTO> nodes, List<ResourceDTO> resources);
    }
}
