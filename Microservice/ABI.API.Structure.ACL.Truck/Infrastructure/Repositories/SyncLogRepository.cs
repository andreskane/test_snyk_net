using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class SyncLogRepository : ISyncLogRepository
    {
        private readonly TruckACLContext _context;

        public SyncLogRepository(TruckACLContext context)
        {
            _context = context;
        }

        public async Task Create(SyncLog entity, CancellationToken cancellationToken = default)
        {
            _context.SyncLogs.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
