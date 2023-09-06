using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface ISyncLogRepository
    {
        Task Create(SyncLog entity, CancellationToken cancellationToken = default);
    }
}
