using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IAttentionModeRepository : IGenericRepository<int, AttentionMode, StructureContext>
    {
        Task<AttentionMode> GetByName(string name);

        Task<IList<AttentionMode>> GetAllActive(bool active);

    }
}
