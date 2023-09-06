using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Caching;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class VersionedRepository : GenericRepository<int, Versioned, TruckACLContext>, IVersionedRepository
    {
        private readonly ICacheStore _cacheStore;
        #region Contructor
      //  public VersionedRepository(TruckACLContext context) : base(context) { }
        public VersionedRepository(TruckACLContext context, ICacheStore cacheStore) : base(context)
        {

            _cacheStore = cacheStore;
           // context = context;

        }
        #endregion


        /// <summary>
        /// Gets the by status asynchronous.
        /// </summary>
        /// <param name="statu">The statu.</param>
        /// <returns></returns>
        public async Task<IList<Versioned>> GetByStatuAsync(VersionedState statu)
        {
            return  await UntrackedSet().Where(v => v.StatusId == (int)statu).OrderByDescending(o=>o.Validity).ToListAsync();
        }
        public override Task<int> Create(Versioned entity)
        {
            _cacheStore.Remove(new AllVersionCacheKey(""));
            return base.Create(entity);
        }
        public override Task Delete(int id)
        {
            _cacheStore.Remove(new AllVersionCacheKey(""));
            return base.Delete(id);

        }
        public override Task Update(Versioned entity)
        {
            _cacheStore.Remove(new AllVersionCacheKey(""));
            return base.Update(entity);
        }
        /// <summary>
        /// Gets the by nro ver validity.
        /// </summary>
        /// <param name="versionTruck">The version truck.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        public async Task<IList<Versioned>> GetByNroVerValidity(string versionTruck, DateTimeOffset validity)
        {
            return await UntrackedSet().Where(v => v.Version == versionTruck && v.Validity == validity).ToListAsync();
        }

        public async Task<IList<Versioned>> GetVersionByIdsValidity(List<int> NodesIds, List<int> AristasIds, DateTimeOffset Validate)
        {
            var res = await UntrackedSet().AsNoTracking()
                .Include(n=>n.VersionedsNode).Where(x=>x.VersionedsNode.Any(b=>NodesIds.Contains(b.NodeDefinitionId)) )
                //.Include(n => n.VersionedsArista).Where(x => x.VersionedsArista.Any(b => AristasIds.Contains(b.AristaId)))
                .Include(s => s.VersionedStatus)
                .Where(w=>w.Validity<= Validate)
                .ToListAsync();
     
            return res;
        }

        public async Task<IList<Versioned>> GetVersionByIdsStructureValidity(List<Int32> StructureIds, DateTimeOffset ValidateFrom, DateTimeOffset ValidateTo)
        {
            if (StructureIds == null || StructureIds.Count == 0)
            {
                return await UntrackedSet().AsNoTracking()
                     .Include(s => s.VersionedStatus)
                     .Where(x => x.Validity <= ValidateTo & x.Validity >= ValidateFrom)
                     .ToListAsync();
            }
            else
            {
                return await UntrackedSet().AsNoTracking()
                    .Include(s => s.VersionedStatus)
                    .Where(x => x.Validity <= ValidateTo & x.Validity >= ValidateFrom)
                    .Where(y => StructureIds == null || StructureIds.Contains(y.StructureId))
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Gets all versions by validity.
        /// </summary>
        /// <param name="validateFrom">The validate from.</param>
        /// <returns></returns>
        public async Task<IList<Versioned>> GetAllVersionsByValidity(DateTimeOffset validateFrom)
        {
            return await UntrackedSet().AsNoTracking()
                    .Where(v => v.Validity == validateFrom).OrderByDescending(o=>o.Date).ToListAsync();

        }


        /// <summary>
        /// Gets the one version by ver truck.
        /// </summary>
        /// <param name="verTruck">The ver truck.</param>
        /// <returns></returns>
        public async Task<Versioned> GetOneVersionByVerTruck(string verTruck)
        {
            return await UntrackedSet().AsNoTracking().FirstOrDefaultAsync(v => v.Version == verTruck);
        }

        /// <summary>
        /// Gets all versions pending by validity.
        /// </summary>
        /// <param name="validateFrom">The validate from.</param>
        /// <returns></returns>
        public async Task<IList<Versioned>> GetAllVersionsPendingByValidity(DateTimeOffset validateFrom)
        {
            return await UntrackedSet().AsNoTracking()
                    .Where(v => v.Validity == validateFrom && v.StatusId == (int)VersionedState.PendienteDeEnvio).OrderByDescending(o => o.Date).ToListAsync();

        }
    }

    public class AllVersionCacheKey : ICacheKey<IList<Versioned>>
    {
        public AllVersionCacheKey(string param)
        {
            CacheKey = string.Format("{0}{1}", "AllVersion_", param);
        }

        public string CacheKey { get; private set; }
    }

}
