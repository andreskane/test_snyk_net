using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IStructureModelDefinitionRepository : IGenericRepository<int, StructureModelDefinition, StructureContext>
    {

        Task<int> DeleteAsync(StructureModelDefinition entity, CancellationToken cancellationToken = default);

        Task<IList<StructureModelDefinition>> GetAllByStructureModel(int id, CancellationToken cancellationToken = default);

        Task<StructureModelDefinition> GetLevelByLevelId(int id, int levelId, CancellationToken cancellationToken = default);

        Task<IList<StructureModelDefinition>> GetAllByStructureModelWithOutLevel(int id, CancellationToken cancellationToken = default);
    }
}
