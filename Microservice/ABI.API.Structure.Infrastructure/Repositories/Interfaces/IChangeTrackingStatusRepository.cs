using ABI.API.Structure.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IChangeTrackingStatusRepository
    {
        Task Create(ChangeTrackingStatus entity, CancellationToken cancellationToken = default);
        Task Delete(ChangeTrackingStatus entity, CancellationToken cancellationToken = default);
        Task<IList<ChangeTrackingStatus>> GetAll(CancellationToken cancellationToken = default);
        Task<ChangeTrackingStatus> GetByChangeId(int changeId, CancellationToken cancellationToken = default);
        void Update(ChangeTrackingStatus entity);
    }
}
