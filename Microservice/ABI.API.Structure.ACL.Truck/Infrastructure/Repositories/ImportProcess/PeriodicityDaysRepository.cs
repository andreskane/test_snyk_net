using ABI.API.Structure.ACL.Truck.Domain.Enums;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class PeriodicityDaysRepository : IPeriodicityDaysRepository
    {
        private readonly TruckACLContext _context;

        public PeriodicityDaysRepository(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<int> GetDaysCount(Periodicity periodicity) =>
            await _context
                .PeriodicityDaysDBSet
                .AsNoTracking()
                .Where(x => x.Id == periodicity)
                .Select(x => x.DaysCount)
                .SingleAsync();
        public async Task<IDictionary<Periodicity, int>> GetPeriodicityToDaysDictionary(CancellationToken cancellationToken) =>
    new Dictionary<Periodicity, int>(
        await _context
        .PeriodicityDaysDBSet
        .AsNoTracking()
        .Select(x => new KeyValuePair<Periodicity, int>(x.Id, x.DaysCount))
        .ToListAsync(cancellationToken));
    }
}
