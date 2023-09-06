using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public interface IStructureRepository : IRepositoryDomain<StructureDomain>
    {
        Task<StructureDomain> GetStructureNodeRootAsync(int structureId);
        Task<IList<StructureDomain>> GetAllStructureNodeRootAsync(string country);
        Task<StructureDomain> GetStructureDataCompleteAsync(int structureId);
        Task<StructureDomain> GetStructureDataCompleteByNameAsync(string name);
        StructureDomain AddMigration(StructureDomain entity);
        Task<IList<GenericKeyValue>> GetAllStructuresChangesTracking(CancellationToken cancellationToken = default);
        Task<StructureDomain> GetStructureByNameAsync(string name);
        Task<StructureDomain> GetStructureByCodeAsync(string code);
    }
}
