
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface IMapeoTableTruckPortal
    {
        Task<List<LevelTruckPortal>> GetAllLevelTruckPortal();
        Task<List<BusinessTruckPortal>> GetAllBusinessTruckPortal();
        Task<BusinessTruckPortal> GetOneBusinessTruckPortal(string code);
        Task<IList<TypeVendorTruckPortal>> GetAllTypeVendorTruckPortal();
        Task<BusinessTruckPortal> GetOneBusinessTruckPortalByName(string name);
        Task<LevelTruckPortal> GetOneLevelTruckPortalByLevelId(int levelId);
    }
}
