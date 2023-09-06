using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Caching;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class ChangeTrackingRepository : IChangeTrackingRepository
    {
        private readonly StructureContext _context;
        private readonly ICacheStore _cacheStore;
     
        private IQueryable<ChangeTracking> UntrackedSet() =>
            _context.ChangesTracking.AsNoTracking();


        public ChangeTrackingRepository(StructureContext context,ICacheStore cacheStore)
        {

            _cacheStore = cacheStore;
            _context = context;
         
        }

       

        public async Task<IList<ChangeTracking>> GetAll(bool onlyConfirmed, CancellationToken cancellationToken = default)
        {
            var StoreCache = _cacheStore.Get(new AllCTCacheKey(""));

            if (StoreCache != null)
            {
                return StoreCache;
            }

            var res = await UntrackedSet()
                 .Include(s => s.ChangeTrackingStatusListItems).ThenInclude(x => x.Status)
                 .Include(e => e.Structure).ThenInclude(m => m.StructureModel)
                .ToListAsync(cancellationToken);
            
            if (onlyConfirmed)
                res = res.Where(x => x.ChangeStatus != null && x.ChangeStatus.Status != null && x.ChangeStatus.Status.Id == (int)ChangeTrackingStatusCode.Confirmed).ToList();

            _cacheStore.Add(res, new AllCTCacheKey(""), "default");

            return res;

        }

        public async Task<IList<int>> GetAllObjectsIdByType(ChangeTrackingObjectType type, CancellationToken cancellationToken = default) =>
            await UntrackedSet()
                .Where(w => w.IdObjectType == (int)type)
                .GroupBy(g => g.IdObjectType)
                .Select(s => s.Key)
                .ToListAsync(cancellationToken);

        public async Task<IList<string>> GetAllUsers(CancellationToken cancellationToken = default) =>
            await UntrackedSet()
                .GroupBy(g => g.UserJson)
                .Select(s => s.Key)
                .ToListAsync(cancellationToken);

        public async Task<IList<ChangeTracking>> GetByDatesRange(DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default) =>
            await UntrackedSet()
                .Where(w => w.CreateDate >= from && w.CreateDate <= to)
                .Include(x => x.ObjectType)
                .ToListAsync(cancellationToken);

        public async Task<ChangeTracking> GetById(int id, bool tracking = true, CancellationToken cancellationToken = default)
        {
            if(tracking)
                return await _context.ChangesTracking
                    .Include(x => x.ObjectType)
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            else
                return await UntrackedSet()
                    .Include(x => x.ObjectType)
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
            
        public async Task<IList<ChangeTracking>> GetByStructureId(int structureId, CancellationToken cancellationToken = default) =>
            await _context.ChangesTracking
                .Where(x => x.IdStructure == structureId)
                .Include(s => s.ChangeTrackingStatusListItems).ThenInclude(x => x.Status)
                .Include(x => x.ObjectType)
                .ToListAsync(cancellationToken);

        public async Task Create(ChangeTracking entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            _context.ChangesTracking.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ChangeTracking entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            await Task.Run(() => _context.ChangesTracking.Update(entity));
        }

        public async Task<IList<ChangeTracking>> GetByOriginAndDestinationIdAndValidity(int originId, int destinationId, DateTimeOffset validity, bool tracking =false, CancellationToken cancellationToken = default)
        {
            if (tracking)
                return await _context.ChangesTracking
                    .Where(x => x.IdDestino == destinationId && x.IdOrigen == originId && x.ValidityFrom == validity)
                .Include(s => s.ChangeTrackingStatusListItems).ThenInclude(x => x.Status)
                .Include(x => x.ObjectType)
                .ToListAsync(cancellationToken);
            else
                return await UntrackedSet()
                    .Where(x => x.IdDestino == destinationId && x.IdOrigen == originId && x.ValidityFrom == validity)
                .Include(s => s.ChangeTrackingStatusListItems).ThenInclude(x => x.Status)
                .Include(x => x.ObjectType)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task Delete(ChangeTracking entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            _context.ChangesTracking.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


    public class AllCTCacheKey : ICacheKey<IList<ChangeTracking>>
    {
        public AllCTCacheKey(string param)
        {
            CacheKey = string.Format("{0}{1}", "AllCT_", param);
        }

        public string CacheKey { get; private set; }
    }

}
