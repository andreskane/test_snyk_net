using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface ILevelRepository : IGenericRepository<int, Level, StructureContext>
    {
        Task<Level> GetByName(string name, CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(Level entity, CancellationToken cancellationToken = default);

        Task<IList<Level>> GetAllActive(bool active, CancellationToken cancellationToken = default);
    }
}
