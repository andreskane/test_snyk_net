using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class ChangeTrackingStatusRepository : IChangeTrackingStatusRepository
    {
        private readonly StructureContext _context;
        private readonly ICacheStore _cacheStore;
        private readonly IConfiguration _configuration;


        private IQueryable<ChangeTrackingStatus> UntrackedSet() =>
            _context.ChangesTrackingStatus.AsNoTracking();
       

        public ChangeTrackingStatusRepository(StructureContext context, ICacheStore cacheStore,
    IConfiguration configuration)
        {

            _cacheStore = cacheStore;
            _context = context;
            _configuration = configuration;
        }


        public async Task Create(ChangeTrackingStatus entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            _context.ChangesTrackingStatus.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<ChangeTrackingStatus>> GetAll(CancellationToken cancellationToken = default)
        {
            //todo: ver de sacar el anidado que arma el objeto 
            var res=  await UntrackedSet().Include(x => x.Status).ToListAsync(cancellationToken);

             
            return res;
        }

        public async Task<ChangeTrackingStatus> GetByChangeId(int changeId, CancellationToken cancellationToken = default)
        {
            return await _context.ChangesTrackingStatus.Include(x => x.Status).SingleOrDefaultAsync(x => x.IdChangeTracking == changeId, cancellationToken);
        }

        public void Update(ChangeTrackingStatus entity)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task Delete(ChangeTrackingStatus entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllCTCacheKey(""));
            _context.ChangesTrackingStatus.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
