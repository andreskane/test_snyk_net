using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface IVersionedAristaRepository : IGenericRepository<int, VersionedArista, TruckACLContext>
    {
        Task<IList<VersionedArista>> GetByListAristasDefinitionId(List<int> ids);
    }
}
