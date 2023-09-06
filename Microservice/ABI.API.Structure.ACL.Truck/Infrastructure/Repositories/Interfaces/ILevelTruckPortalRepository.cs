using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.Framework.MS.Repository.Generics;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface ILevelTruckPortalRepository : IGenericRepository<int, LevelTruckPortal, TruckACLContext>
    {


    }
}
