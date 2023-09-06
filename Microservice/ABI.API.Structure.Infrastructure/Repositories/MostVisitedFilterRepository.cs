using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Caching;

using Microsoft.EntityFrameworkCore;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class MostVisitedFilterRepository : IMostVisitedFilterRepository
    {
        private readonly StructureContext _context;
        private readonly ICacheStore _cacheStore;

        private IQueryable<MostVisitedFilter> UntrackedSet() =>
            _context.MostVisitedFilters.AsNoTracking();

        public MostVisitedFilterRepository(StructureContext context, ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
            _context = context;
        }
        public async Task Create(MostVisitedFilter entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllMVCacheKey(""));
            _context.MostVisitedFilters.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<MostVisitedFilter>> GetByUserAndStructureOrder(Guid userId, int structureId, int maxRegisters=0, CancellationToken cancellationToken = default)
        {
            var res = await UntrackedSet()
                    .Where(x=> x.StructureId == structureId && x.UserId == userId)
                    .OrderByDescending(x=> x.Quantity)
                    .ToListAsync(cancellationToken);

            if (maxRegisters > 0)
                res = res.Take(maxRegisters).ToList();

            return res;
        }

        public async Task<MostVisitedFilter> GetByUserStructureAndValue(string description, int filterType, Guid userId, int structureId, CancellationToken cancellationToken = default)
        {
            var res = await _context.MostVisitedFilters
                    .SingleOrDefaultAsync(x => x.StructureId == structureId
                                && x.UserId == userId
                                && x.Description == description
                                && x.FilterType == filterType, cancellationToken);

            return res;
        }

        public async Task Update(MostVisitedFilter entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllMVCacheKey(""));
            _context.MostVisitedFilters.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    public class AllMVCacheKey : ICacheKey<IList<MostVisitedFilter>>
    {
        public AllMVCacheKey(string param)
        {
            CacheKey = string.Format("{0}{1}", "AllMV_", param);
        }

        public string CacheKey { get; private set; }
    }
}
