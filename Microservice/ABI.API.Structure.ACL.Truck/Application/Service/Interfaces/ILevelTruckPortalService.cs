using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.Infrastructure;
using ABI.Framework.MS.Service.Generics;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ILevelTruckPortalService : IGenericService<int, LevelTruckPortal, StructureContext>
    {
        Task<int> Add(LevelTruckPortal entity);
        Task Edit(LevelTruckPortal entity);
    }
}
