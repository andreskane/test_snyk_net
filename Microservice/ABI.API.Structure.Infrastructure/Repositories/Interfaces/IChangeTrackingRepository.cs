using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IChangeTrackingRepository
    {
        Task Create(ChangeTracking entity, CancellationToken cancellationToken = default);
        Task<IList<ChangeTracking>> GetAll(bool onlyConfirmed, CancellationToken cancellationToken = default);
         Task<IList<int>> GetAllObjectsIdByType(ChangeTrackingObjectType type, CancellationToken cancellationToken = default);
        Task<IList<string>> GetAllUsers(CancellationToken cancellationToken = default);
        Task<IList<ChangeTracking>> GetByDatesRange(DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default);
        Task<ChangeTracking> GetById(int id, bool tracking = true, CancellationToken cancellationToken = default);
        Task Update(ChangeTracking entity, CancellationToken cancellationToken = default);
        Task<IList<ChangeTracking>> GetByStructureId(int structureId, CancellationToken cancellationToken = default);
        Task<IList<ChangeTracking>> GetByOriginAndDestinationIdAndValidity(int originId, int destinationId, DateTimeOffset validity, bool tracking = false, CancellationToken cancellationToken = default);
        Task Delete(ChangeTracking entity, CancellationToken cancellationToken = default);
    }
}