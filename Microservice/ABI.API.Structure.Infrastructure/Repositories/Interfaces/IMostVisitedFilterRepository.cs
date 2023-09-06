using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IMostVisitedFilterRepository
    {
        Task Create(MostVisitedFilter entity, CancellationToken cancellationToken = default);
        Task Update(MostVisitedFilter entity, CancellationToken cancellationToken = default);
        Task<IList<MostVisitedFilter>> GetByUserAndStructureOrder(Guid userId, int structureId, int maxRegisters=0, CancellationToken cancellationToken = default);
        Task<MostVisitedFilter> GetByUserStructureAndValue(string description, int filterType, Guid userId, int structureId, CancellationToken cancellationToken = default);
    }
}
