using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IStructureModelRepository : IGenericRepository<int, StructureModel, StructureContext>
    {
        Task<StructureModel> GetByName(string name, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(StructureModel entity, CancellationToken cancellationToken = default);
        Task<IList<StructureModel>> GetAllActive(bool active, CancellationToken cancellationToken = default);
        Task<StructureModel> GetByNameAndCountry(string name, int countryId, CancellationToken cancellationToken = default);
        Task<StructureModel> GetByCodeAndCountry(string code, int countryId, CancellationToken cancellationToken = default);
    }
}
