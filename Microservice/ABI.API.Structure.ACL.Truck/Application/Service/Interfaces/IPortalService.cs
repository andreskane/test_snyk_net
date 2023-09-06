using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface IPortalService
    {
        Task<StructureNodeDTO> GetAllCompareByStructureId(string structureName, bool? active);
        Task<StructureDomain> GetStrucureByName(string structureName);
    }
}
