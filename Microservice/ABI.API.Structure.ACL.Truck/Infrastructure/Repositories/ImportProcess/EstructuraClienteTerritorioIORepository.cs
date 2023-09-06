using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Caching;
using ABI.Framework.MS.EFBulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class EstructuraClienteTerritorioIORepository: IEstructuraClienteTerritorioIORepository
    {
        private readonly TruckACLContext _context;
        private readonly ICacheStore _cacheStore;

        public EstructuraClienteTerritorioIORepository(TruckACLContext context,ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
            _context = context;
        }

        private IQueryable<EstructuraClienteTerritorioIO> UntrackedSet() =>
                _context.EstructuraClienteTerritorioIODBSet.AsNoTracking();

        public async Task<IList<EstructuraClienteTerritorioIO>> GetByProcessId(int processId, CancellationToken cancellationToken) =>
            await UntrackedSet()
                .Where(x => x.ImportProcessId == processId)
                .ToListAsync(cancellationToken);

        public async Task BulkDelete(IList<EstructuraClienteTerritorioIO> EstructuraClienteTerritorioIOs, CancellationToken cancellationToken) =>
            await _context.BulkDeleteAsync(EstructuraClienteTerritorioIOs, cancellationToken: cancellationToken);

        public async Task BulkDeleteRestProcess(int IdProcess, CancellationToken cancellationToken)
        {
            var entity = await _context.ImportProcessDBSet.FindAsync(IdProcess);

            //borro las de la anterior sosicedad y no pertenecesn a este proceso
            //actulizo el nombre del pais a el id del proceso
      
            var IOData = UntrackedSet().Where(x => x.ImportProcessId == entity.Id).ToList();
            IOData.ForEach(c => c.Pais_ID = entity.CompanyId);
            await _context.BulkUpdateAsync(IOData, cancellationToken: cancellationToken);
 
            var listIO = await _context.EstructuraClienteTerritorioIODBSet
                .Where(x => x.ImportProcessId != entity.Id && x.Pais_ID == entity.CompanyId)
                .ToListAsync(cancellationToken);

            await DeleteRange(listIO, cancellationToken);

            _cacheStore.Remove(new AllIOCacheKey(entity.CompanyId));
        }

        public async Task<IList<EstructuraClienteTerritorioIO>> GetLastDataByCountryNoTracking(string country, CancellationToken cancellationToken)
        {
            var StoreCache = _cacheStore.Get(new AllIOCacheKey(country));

            if (StoreCache != null)
                return StoreCache;

            List<EstructuraClienteTerritorioIO> listResultado;                          
 
            listResultado = await UntrackedSet().Where(x => x.Pais_ID == country).ToListAsync(cancellationToken);

             _cacheStore.Add(listResultado, new AllIOCacheKey(country), "default");
            return listResultado;
        }

        public async Task<IList<EstructuraClienteTerritorioIO>> GetLastDataByCountry(string country, CancellationToken cancellationToken)
        {
            var StoreCache = _cacheStore.Get(new AllIOCacheKey(country));

            if (StoreCache != null)
                return StoreCache;

            List<EstructuraClienteTerritorioIO> listResultado;

            listResultado = await _context.EstructuraClienteTerritorioIODBSet.Where(x => x.Pais_ID == country).ToListAsync(cancellationToken);

            _cacheStore.Add(listResultado, new AllIOCacheKey(country), "default");
            return listResultado;
        }

        public async Task DeleteRange(IEnumerable<EstructuraClienteTerritorioIO> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                return;

            _context.EstructuraClienteTerritorioIODBSet.RemoveRange(entities);

            //TODO: Agregar soporte multi-país
            _cacheStore.Remove(new AllIOCacheKey(""));
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


    public class AllIOCacheKey : ICacheKey<List<EstructuraClienteTerritorioIO>>
    {
        public AllIOCacheKey(string param)
        {
            CacheKey = string.Format("{0}{1}", "AllIO_", param);
        }

        public string CacheKey { get; private set; }
    }
}
 
