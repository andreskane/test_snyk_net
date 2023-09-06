using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate
{
    public interface IStructureClientRepository
    {
        
         Task<IList<StructureClientNode>> GetAllByStructureId(int structureId, DateTimeOffset validityFrom, CancellationToken cancellationToken);
        Task<StructureClientNode> GetById(int id, CancellationToken cancellationToken = default);
        Task Create(StructureClientNode entity, CancellationToken cancellationToken = default);
        Task Update(StructureClientNode entity, CancellationToken cancellationToken = default);
        Task Delete(StructureClientNode entity, CancellationToken cancellationToken = default);
        Task<IList<StructureClientNode>> GetAllCurrentByStructureIdWithOutTracking(int structureId, CancellationToken cancellationToken = default);

        Task<IList<StructureClientNode>> GetAllCurrentByStructureId(int structureId, CancellationToken cancellationToken = default);
        Task CreateRange(IEnumerable<StructureClientNode> entities, CancellationToken cancellationToken = default);
        Task UpdateRange(IEnumerable<StructureClientNode> entities, CancellationToken cancellationToken = default);
        Task<IList<StructureClientNode>> GetActivesClientsByNodeId(int nodeId, DateTimeOffset validityFrom, CancellationToken cancellationToken = default);
        Task<IList<StructureClientNode>> GetClientsByNodesIds(List<int> nodesIds, DateTimeOffset validityFrom, CancellationToken cancellationToken = default);
        Task BulkInsertAsync(IList<StructureClientNode> entities, int structureId, CancellationToken cancellationToken = default);
        Task BulkUpdateAsync(IList<StructureClientNode> entities, int structureId, CancellationToken cancellationToken = default);
    }
}
