using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IAttentionModeRoleRepository : IGenericRepository<int, AttentionModeRole, StructureContext>
    {
        Task<AttentionModeRole> GetRoleById(int roleId);

        Task<IList<AttentionModeRole>> GetAllByRoleId(int roleId);
    }
}
