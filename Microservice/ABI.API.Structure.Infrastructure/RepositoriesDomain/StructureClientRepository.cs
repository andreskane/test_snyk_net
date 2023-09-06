using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.Framework.MS.Caching;
using ABI.Framework.MS.EFBulkExtensions;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.RepositoriesDomain
{
    public class StructureClientRepository : IStructureClientRepository
    {
        private readonly StructureContext _context;
        private readonly ICacheStore _cacheStore;

        public StructureClientRepository(StructureContext context,
                        ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
            _context = context;


        }

        public async Task Create(StructureClientNode entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            _context.StructureClientNodes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(StructureClientNode entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            _context.StructureClientNodes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<StructureClientNode>> GetAllByStructureId(int structureId, DateTimeOffset validityFrom, CancellationToken cancellationToken)
        {
            //var StoreCache = _cacheStore.Get(new AllStructureClientNodeCacheKey( string.Format("{0}-{1}", structureId.ToString(), validityFrom)));
            var StoreCache = _cacheStore.Get(new AllStructureClientNodeCacheKey(structureId.ToString()));

            if (StoreCache != null)
            {
                return StoreCache;
            }

            List<StructureClientNode> listClients;

            listClients = await (from c in _context.StructureClientNodes.AsNoTracking()
                           join n in _context.StructureNodes.AsNoTracking() on c.NodeId equals n.Id 
                           join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                           where a.StructureIdFrom == structureId &&  n.LevelId == 8 
                           &&  c.ValidityFrom <= validityFrom && c.ValidityTo > validityFrom
                           select c
                          ).ToListAsync(cancellationToken);

            _cacheStore.Add(listClients, new AllStructureClientNodeCacheKey(string.Format("{0}-{1}", structureId.ToString(), validityFrom)), "default");

            return listClients;
        }

        public async Task<IList<StructureClientNode>> GetAllCurrentByStructureId(int structureId, CancellationToken cancellationToken)
        {
            List<StructureClientNode> listClients;

            listClients = await _context.StructureClientNodes
                            .Include(z => z.Node)
                            .Where(x => x.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3))
                                        && x.Node != null && x.Node.AristasTo.Count > 0 && x.Node.AristasTo.FirstOrDefault().StructureIdFrom == structureId
                                        && x.Node.LevelId == 8)
                            .ToListAsync(cancellationToken);

            return listClients;
        }

        public async Task<IList<StructureClientNode>> GetAllCurrentByStructureIdWithOutTracking(int structureId, CancellationToken cancellationToken)
        {

            //todo:descomento el cache en este caso, despues hay que crear la clase para eliminar todas las key de cache con wildcard
            var StoreCache = _cacheStore.Get(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", structureId.ToString())));


            if (StoreCache != null)
            {
                return StoreCache;
            }

            List<StructureClientNode> listClients;

            listClients = await _context.StructureClientNodes.AsNoTracking()
                            .Include(z => z.Node)
                            .Where(x => x.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3))
                                        && x.Node != null && x.Node.AristasTo.Count > 0 && x.Node.AristasTo.FirstOrDefault().StructureIdFrom == structureId
                                        && x.Node.LevelId == 8)
                            .ToListAsync(cancellationToken);

             _cacheStore.Add(listClients, new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", structureId.ToString())), "default");

            return listClients;



        }


        public async Task<StructureClientNode> GetById(int id, CancellationToken cancellationToken = default) => await _context.StructureClientNodes
                .Include(x => x.Node)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task Update(StructureClientNode entity, CancellationToken cancellationToken = default)
        {
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(entity.Id.ToString()));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", entity.Id.ToString())));
            
            _context.StructureClientNodes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateRange(IEnumerable<StructureClientNode> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                return;

            await _context.StructureClientNodes.AddRangeAsync(entities, cancellationToken);

            //TODO: Agregar soporte multi-país
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            foreach (var item in entities)
            {
                foreach (var subitem in item.Node.Structures)
                {
                    _cacheStore.Remove(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", subitem.Id.ToString())));
                    _cacheStore.Remove(new AllStructureClientNodeCacheKey(subitem.Id.ToString()));
                }

            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BulkInsertAsync(IList<StructureClientNode> entities, int structureId, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                return;

            await _context.BulkInsertAsync(entities, cancellationToken: cancellationToken);

            //TODO: Agregar soporte multi-país
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", structureId.ToString())));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(structureId.ToString()));
        }

        public async Task UpdateRange(IEnumerable<StructureClientNode> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                return;

            _context.StructureClientNodes.UpdateRange(entities);

            //TODO: Agregar soporte multi-país
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));

            foreach (var item in entities)
            {
                foreach (var subitem in item.Node.Structures)
                {

                    _cacheStore.Remove(new AllStructureClientNodeCacheKey(subitem.Id.ToString()));
                    _cacheStore.Remove(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", subitem.Id.ToString())));
                   
                }

            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BulkUpdateAsync(IList<StructureClientNode> entities, int structureId, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                return;

            await _context.BulkUpdateAsync(entities, cancellationToken: cancellationToken);

            //TODO: Agregar soporte multi-país
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(""));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(string.Format("rel_client_{0}", structureId.ToString())));
            _cacheStore.Remove(new AllStructureClientNodeCacheKey(structureId.ToString()));
        }

        public async Task<IList<StructureClientNode>> GetActivesClientsByNodeId(int nodeId, DateTimeOffset validityFrom, CancellationToken cancellationToken = default)
        {
            List<StructureClientNode> listClients;
            listClients = await _context.StructureClientNodes.AsNoTracking()
                            .Where(x => x.NodeId == nodeId && x.ValidityFrom <= validityFrom
                            && x.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3))
                            && x.ClientState == "1")
                            .ToListAsync(cancellationToken);
            return listClients;
        }

        public async Task<IList<StructureClientNode>> GetClientsByNodesIds(List<int> nodesIds, DateTimeOffset validityFrom, CancellationToken cancellationToken = default)
        {
            List<StructureClientNode> listClients;
            listClients = await _context.StructureClientNodes.AsNoTracking()
                            .Where(x => nodesIds.Contains(x.NodeId) 
                            && x.ValidityFrom <= validityFrom
                            && x.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3))
                            && x.ClientState == "1")
                            .ToListAsync(cancellationToken);
            return listClients;
        }
    }

    public class AllStructureClientNodeCacheKey : ICacheKey<List<StructureClientNode>>
    {
        public AllStructureClientNodeCacheKey(string param)
        {
            CacheKey = string.Format("{0}{1}", "AllStructureClientNode_", param);
        }

        public string CacheKey { get; private set; }
    }
}
