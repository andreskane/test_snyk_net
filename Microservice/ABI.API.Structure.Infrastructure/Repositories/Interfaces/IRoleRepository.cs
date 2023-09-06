using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IRoleRepository : IGenericRepository<int, Role, StructureContext>
    {
        Task<Role> GetByName(string name);
        Task<IList<string>> GetAllTag();
        Task<IList<Role>> GetAllActive(bool active);

    }
}
