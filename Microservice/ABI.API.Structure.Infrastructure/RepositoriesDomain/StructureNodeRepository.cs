using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.RepositoriesDomain
{
    public class StructureNodeRepository : IStructureNodeRepository
    {
        private readonly StructureContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        private IQueryable<StructureNode> UntrackedSet() =>    _context.StructureNodes.AsNoTracking();
        private IQueryable<StructureArista> UntrackedSetArista() => _context.StructureAristas.AsNoTracking();
        private IQueryable<StructureNodeDefinition> UntrackedSetNodeDefinition() => _context.StructureNodeDefinitions.AsNoTracking();
        public StructureNodeRepository(StructureContext context) => _context = context;
 
        #region Nodo

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public StructureNode Add(StructureNode entity)
        {
            return _context.StructureNodes.Add(entity).Entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(StructureNode entity)
        {
            _context.StructureNodes.Remove(entity);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StructureNode>> GetAllAsync() => await _context.StructureNodes.ToListAsync();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public IQueryable<StructureNode> GetAllWithLevelAsyncIQueryable() => _context.StructureNodes.AsNoTracking().Include(n => n.Level).AsQueryable();

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureNode> GetAsync(int id)
        {
            var nodedefinition = _context.StructureNodes.Include(d => d.StructureNodoDefinitions).FirstOrDefault(n => n.Id == id);

            return await Task.Run(() => nodedefinition);
        }


        /// <summary>
        /// Gets the nodes asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="levelId">The level identifier.</param>
        /// <returns></returns>
        public async Task<List<StructureNode>> GetNodesAsync(int structureId, int levelId)
        {
            var nodes = (from n in _context.StructureNodes
                         join a in _context.StructureAristas on n.Id equals a.NodeIdFrom 
                         where a.StructureIdFrom == structureId && n.LevelId == levelId
                         select n).ToList();


            return await Task.Run(() => nodes);
        }

        /// <summary>
        /// Gets the nodes asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        public async Task<List<StructureNode>> GetNodesTerritoryAsync(int structureId)
        {
            var nodes = (from n in _context.StructureNodes
                         join a in _context.StructureAristas on n.Id equals a.NodeIdTo
                         where a.StructureIdFrom == structureId && n.LevelId == 8
                         select n).ToList();


            return await Task.Run(() => nodes);
        }

        /// <summary>
        /// Determines whether [contains child nodes] [the specified structure identifier].
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [contains child nodes] [the specified structure identifier]; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> ContainsChildNodes(int structureId, int id)
        {
            var childNodes = await _context.StructureAristas.Where(s => s.NodeIdFrom == id && s.StructureIdTo == structureId).ToListAsync();

            if (childNodes.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the nodo child all by node identifier asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<List<StructureNode>> GetNodoChildAllByNodeIdAsync(int structureId, int id)
        {
            var childNodes = await _context.StructureAristas.Where(s => s.NodeIdFrom == id && s.StructureIdTo == structureId).ToListAsync();

            var list = new List<StructureNode>();

            foreach (var item in childNodes)
            {
                list.Add(await GetAsync(item.NodeIdTo));
            }


            return list;
        }

        public async Task<List<StructureNode>> GetNodoChildAllConfirmByNodeIdAsync(int structureId, int id, DateTimeOffset validityFrom)
        {
            var aristas = UntrackedSetArista()
                                        .Include(n => n.NodeFrom)
                                        .Include(n => n.NodeTo)
                                        .Where(s => s.NodeIdFrom == id
                                        && s.StructureIdTo == structureId
                                        && s.MotiveStateId == (int)MotiveStateNode.Confirmed
                                        && s.ValidityFrom <= validityFrom
                            ).AsQueryable();

            var childNodes = await aristas
                                .Include(n => n.NodeTo.StructureNodoDefinitions)
                                .Where(a =>
                                a.NodeTo.StructureNodoDefinitions.Any(
                                    x => x.Active
                                    && x.MotiveStateId == (int)MotiveStateNode.Confirmed
                                    && x.ValidityFrom <= validityFrom
                                    && x.ValidityTo >= validityFrom)
                                ).ToListAsync();

            var list = new List<StructureNode>();

            foreach (var item in childNodes)
            {
                list.Add(await GetAsync(item.NodeIdTo));
            }
            return list;
        }

        /// <summary>
        /// Gets the nodo one by node code asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="code">The code.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public async Task<StructureNode> GetNodoOneByNodeCodeAsync(int structureId, string code, int level)
        {
            var node = (from n in _context.StructureNodes.AsNoTracking().Include(a => a.StructureNodoDefinitions).AsNoTracking()
                        join na in _context.StructureAristas.AsNoTracking() on n.Id equals na.NodeIdFrom
                        where na.StructureIdFrom == structureId && n.LevelId == level && n.Code == code
                        select n).FirstOrDefault();

            return await Task.Run(() => node);
        }

        /// <summary>
        /// Gets the nodo one by code level asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="code">The code.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public async Task<StructureNode> GetNodoOneByCodeLevelAsync(int structureId, string code, int level)
        {
            var node = (from n in _context.StructureNodes.Include(a => a.StructureNodoDefinitions)
                        join na in _context.StructureAristas on n.Id equals na.NodeIdTo
                        where na.StructureIdFrom == structureId && n.LevelId == level && n.Code == code
                        select n).FirstOrDefault();

            return await Task.Run(() => node);
        }

        public async Task<StructureNode> GetOneNodoByCodeLevelAsync(int structureId, string code, int level)
        {
            var node = (from n in UntrackedSet()
                            .Include(a => a.StructureNodoDefinitions)
                            .Include(a=> a.AristasFrom)
                        join na in _context.StructureAristas.AsNoTracking() on n.Id equals na.NodeIdTo
                        where na.StructureIdFrom == structureId && n.LevelId == level && n.Code == code
                        select n).FirstOrDefault();

            return await Task.Run(() => node);
        }
        #endregion

        #region Nodo Definition

        /// <summary>
        /// Gets the nodo definition asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ValidityFrom">The validity from.</param>
        /// <param name="ValidityTo">The validity to.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionAsync(int nodeId, DateTimeOffset? ValidityFrom, DateTimeOffset ValidityTo)
        {

            var pending = await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.MotiveStateId == (int)MotiveStateNode.Draft
                                                          && d.ValidityTo == ValidityTo);

            if (pending != null)
                return pending;

            var node = await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.MotiveStateId == (int)MotiveStateNode.Confirmed
                                                          && d.ValidityFrom == ValidityFrom && d.ValidityTo == ValidityTo);
            if (node != null)
                return node;

            var nodeactual = await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.MotiveStateId == (int)MotiveStateNode.Confirmed
                                               && d.ValidityFrom <= ValidityFrom && d.ValidityTo > ValidityFrom);
            if (nodeactual != null)
                return nodeactual;

            var nodefuture = await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.MotiveStateId == (int)MotiveStateNode.Confirmed
                                           && d.ValidityFrom > ValidityFrom && d.ValidityTo == ValidityTo);
            if (nodefuture != null)
                return nodefuture;

            return null;
        }

        /// <summary>
        /// Gets the nodo definition last current asynchronous.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="ValidityTo">The validity to.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionLastCurrentAsync(int nodeId, DateTimeOffset ValidityTo)
        {
            return await _context.StructureNodeDefinitions.Where(d => d.NodeId == nodeId && d.ValidityTo == ValidityTo).OrderByDescending(d => d.ValidityFrom).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the nodo definition pending asynchronous.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionPendingAsync(int nodeId)
        {
            var today = new DateTimeOffset(DateTimeOffset.UtcNow.Date, TimeSpan.FromHours(-3)); //HACER: Ojo multipais

            return await _context.StructureNodeDefinitions.Where(d => d.NodeId == nodeId && (d.MotiveStateId == (int)MotiveStateNode.Draft || d.ValidityFrom > today))
            .OrderByDescending(c => c.ValidityFrom).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the nodo definition asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionByIdAsync(int id, bool tracking = true)
        {
            var nodesDefinitions = _context.StructureNodeDefinitions.AsQueryable();
            if (!tracking)
                nodesDefinitions = _context.StructureNodeDefinitions.AsNoTracking().AsQueryable();
            
            return await nodesDefinitions.Include(n => n.Node).FirstOrDefaultAsync(d => d.Id == id);
        }



        /// <summary>
        /// Get Lista de nodos segun la lista de ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<StructureNodeDefinition>> GetNodosDefinitionByIdsNodoAsync(List<int> ids, DateTimeOffset ValidityFrom, DateTimeOffset ValidityTo)
        {
            
            var res =await _context.StructureNodeDefinitions.AsNoTracking()
                .Include(n=>n.Node)
                .ThenInclude(l=>l.Level)
                .Where(x=> ids.Contains(x.NodeId)).Where(x => x.ValidityFrom <= ValidityTo && x.ValidityFrom >= ValidityFrom).ToArrayAsync();
            return res;


        }
        /// <summary>
        /// Get Lista de nodos segun la lista de ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<StructureNodeDefinition>> GetNodosDefinitionByIdsNodoAsync(List<int> ids    )
        {

            var res = await _context.StructureNodeDefinitions.AsNoTracking()
                .Include(n => n.Node)
                .ThenInclude(l => l.Level)
                .Where(x => ids.Contains(x.NodeId)).ToArrayAsync();
            return res;


        }

        /// <summary>
        /// Gets the nodo definition validity by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdAsync(int nodeId)
        {
            var validityFrom = DateTimeOffset.UtcNow.Date;
            return await _context.StructureNodeDefinitions.Include(n => n.Node).FirstOrDefaultAsync(d => d.NodeId == nodeId && d.ValidityFrom <= validityFrom);
        }


        /// <summary>
        /// Gets the nodo definition validity by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdAsync(int nodeId, DateTimeOffset validity)
        {
            var maxDate = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
            return  await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.ValidityFrom <= validity
                                                                                    && d.ValidityTo == maxDate
                                                                                    && d.MotiveStateId == (int)MotiveStateNode.Confirmed);
        }

        public async Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdNoTrackingAsync(int nodeId, DateTimeOffset validity)
        {
            var maxDate = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís

            return await UntrackedSetNodeDefinition().Include(n => n.Node)
                            .FirstOrDefaultAsync(d => d.NodeId == nodeId 
                                                && d.ValidityFrom <= validity
                                                && d.ValidityTo == maxDate
                                                && d.MotiveStateId == (int)MotiveStateNode.Confirmed);
        }

        /// <summary>
        /// Gets the nodo definition previous by node identifier asynchronous.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionPreviousByNodeIdAsync(int nodeId, DateTimeOffset validityFrom)
        {

           return await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.ValidityFrom < validityFrom
                                                                                        && d.ValidityTo >= validityFrom
                                                                                        && d.MotiveStateId == (int)MotiveStateNode.Confirmed);
        }

        /// <summary>
        /// Gets the nodo definition validity by identifier dropped asynchronous
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="validity"></param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdDroppedAsync(int nodeId, DateTimeOffset validity)
        {
            var maxDate = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
            return await _context.StructureNodeDefinitions.FirstOrDefaultAsync(d => d.NodeId == nodeId && d.ValidityFrom <= validity
                                                                                    && d.ValidityTo == maxDate
                                                                                    && d.MotiveStateId == (int)MotiveStateNode.Dropped);

        }

        /// <summary>
        /// Gets all nodo definition by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IList<StructureNodeDefinition>> GetAllNodoDefinitionByIdAsync(int id)
        {
            var list = _context.StructureNodeDefinitions.Where(d => d.Id == id).ToList();

            return await Task.Run(() => list);
        }

        /// <summary>
        /// Get all StructureNodeDefinition by NodeId
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<IList<StructureNodeDefinition>> GetAllNodoDefinitionByNodeIdAsync(int nodeId)
        {
            var list = _context.StructureNodeDefinitions.Where(d => d.NodeId == nodeId).ToList();

            return await Task.Run(() => list);
        }


        /// <summary>
        /// Gets all nodo definicion.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StructureNodeDefinition>> GetAllNodeDefinition() => await _context.StructureNodeDefinitions.AsNoTracking().ToListAsync();

        /// <summary>
        /// Gets all nodo definicion with includes.
        /// </summary>
        /// <returns></returns>
        public IQueryable<StructureNodeDefinition> GetAllNodeDefinitionWithIncludesIQueryable() => _context.StructureNodeDefinitions.AsNoTracking()
            .Include(n => n.AttentionMode).AsNoTracking()
            .Include(n => n.Role).AsNoTracking()
            .Include(n => n.SaleChannel).AsNoTracking().AsQueryable();

        /// <summary>
        /// Adds the nodo definition.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public StructureNodeDefinition AddNodoDefinition(StructureNodeDefinition entity)
        {
            return _context.StructureNodeDefinitions.Add(entity).Entity;
        }

        /// <summary>
        /// Updates the nodo definition.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateNodoDefinition(StructureNodeDefinition entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the nodo definitions.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteNodoDefinitions(StructureNodeDefinition entity)
        {
            _context.StructureNodeDefinitions.Remove(entity);
        }

        /// <summary>
        /// Returns a list of territories definitions from every zone an employee is asigned to.
        /// </summary>
        /// <param name="structureId"></param>
        /// <param name="employeeId"></param>
        /// <param name="validity"></param>
        /// <returns></returns>
        public async Task<List<StructureNodeDefinition>> GetTerritoriesByZonesEmployeeId(int structureId, int employeeId, DateTimeOffset validity)
        {
            var territories = await _context.StructureAristas.AsNoTracking()
                .Include(i => i.NodeFrom)
                    .ThenInclude(i => i.StructureNodoDefinitions)
                .Include(i => i.NodeTo)
                    .ThenInclude(i => i.StructureNodoDefinitions)
                .Where(arista =>
                    arista.StructureIdFrom == structureId && arista.StructureIdTo == structureId &&
                    arista.ValidityFrom <= validity && arista.ValidityTo >= validity &&
                    arista.NodeFrom.LevelId == 7 &&
                    arista.NodeFrom.StructureNodoDefinitions.Any(nd =>
                        nd.EmployeeId == employeeId &&
                        nd.ValidityFrom <= validity && nd.ValidityTo >= validity
                    )
                )
                .SelectMany(
                    territories => territories.NodeTo.StructureNodoDefinitions
                        .Where(w => w.ValidityFrom <= validity && w.ValidityTo >= validity)
                )
                .ToListAsync();

            return territories;
        }

        public async Task<List<StructureNodeDefinition>> GetTerritoriesByEmployeeId(int structureId, int employeeId, DateTimeOffset validity)
        {
            var territories = await _context.StructureAristas.AsNoTracking()
                .Include(i => i.NodeTo)
                    .ThenInclude(i => i.StructureNodoDefinitions)
                .Where(arista =>
                    arista.StructureIdFrom == structureId && arista.StructureIdTo == structureId &&
                    arista.ValidityFrom <= validity && arista.ValidityTo >= validity &&
                    arista.NodeTo.LevelId == 8 &&
                    arista.NodeTo.StructureNodoDefinitions.Any(nd =>
                        nd.EmployeeId == employeeId &&
                        nd.ValidityFrom <= validity && nd.ValidityTo >= validity
                    )
                )
                .SelectMany(territories =>
                    territories.NodeTo.StructureNodoDefinitions
                        .Where(w => w.EmployeeId == employeeId)
                )
                .ToListAsync();

            return territories;
        }

        #endregion

        #region Arista

        /// <summary>
        /// Adds the arista.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public StructureArista AddArista(StructureArista entity)
        {

            return _context.StructureAristas.Add(entity).Entity;

        }

        /// <summary>
        /// Updates the arista.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateArista(StructureArista entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets the arista previous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeid">The nodeid.</param>
        /// <param name="validityTo">The validity to.</param>
        /// <returns></returns>
        public async Task<StructureArista> GetAristaPrevious(int structureId, int nodeid, DateTimeOffset validity)
        {
            return await _context.StructureAristas.Where(a =>
                a.StructureIdFrom == structureId &&
                a.NodeIdTo == nodeid &&
                a.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                a.ValidityFrom <= validity &&
                a.ValidityTo >= validity
            )
            .FirstOrDefaultAsync();
        }

        public async Task<StructureNode> GetNodeParentByNodeId(int structureId, int nodeToId, DateTimeOffset validity)
        {
            var arista = await GetAristaPrevious(structureId, nodeToId, validity);
            if (arista != null)
                return await GetAsync(arista.NodeIdFrom);
            return null;
        }

        /// <summary>
        /// Gets the arista.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeIdFrom">The node identifier from.</param>
        /// <param name="nodeIdTo">The node identifier to.</param>
        /// <param name="validityFrom">The validity from.</param>
        /// <param name="validityTo">The validity to.</param>
        /// <returns></returns>
        public async Task<StructureArista> GetArista(int structureId, int nodeIdFrom, int nodeIdTo, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            return await _context.StructureAristas.Where(a => a.StructureIdFrom == structureId &&
                               a.NodeIdFrom == nodeIdFrom && a.NodeIdTo == nodeIdTo
                               && a.ValidityFrom == validityFrom && a.ValidityTo == validityTo).FirstOrDefaultAsync();

        }

        /// <summary>
        /// Gets the arista pendient.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeIdTo">The node identifier to.</param>
        /// <returns></returns>
        public async Task<StructureArista> GetAristaPendient(int structureId, int nodeIdTo, DateTimeOffset validityFrom)
        {
            return await _context.StructureAristas.Where(a => a.StructureIdFrom == structureId &&
                               a.NodeIdTo == nodeIdTo && a.ValidityFrom >= validityFrom
                               && a.MotiveStateId == (int)MotiveStateNode.Draft).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtener todas las aristas de la estructura
        /// </summary>
        /// <param name="structureId"></param>
        /// <returns></returns>
        public async Task<IList<StructureArista>> GetAllAristaByStructure(int structureId)
        {
            return await _context.StructureAristas.AsNoTracking().Where(a => a.StructureIdFrom == structureId).ToListAsync();
        }

        /// <summary>
        /// Obtener todas las aristas de la estructura
        /// </summary>
        /// <param name="structureId"></param>
        /// <returns></returns>
        public IQueryable<StructureArista> GetAllAristaByStructureIQueryable(int structureId)
        {
            return _context.StructureAristas.AsNoTracking().Where(a => a.StructureIdFrom == structureId).AsQueryable();
        }

        public async Task<StructureArista> GetAristaByNodeTo(int structureId, int nodeIdTo, DateTimeOffset validity, bool tracking = true)
        {
            var allAristas = _context.StructureAristas.AsQueryable();
            if (!tracking)
                allAristas = _context.StructureAristas.AsNoTracking().AsQueryable();
            var aristas = await allAristas.Where(a =>
                a.StructureIdFrom == structureId && a.StructureIdTo == structureId &&
                a.NodeIdTo == nodeIdTo &&
                a.ValidityFrom <= validity && a.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)) && //HACER: Ojo multipais
                (
                    a.MotiveStateId == (int)MotiveStateNode.Draft ||
                    a.MotiveStateId == (int)MotiveStateNode.Confirmed
                )
            )
            .ToListAsync();

            if (aristas.Count > 1)
                return aristas.FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);
            else
                return aristas.FirstOrDefault();
        }

        public IQueryable<StructureArista> GetAristaByNodeFromIQueryable(int structureId, int nodeIdFrom, DateTimeOffset validity)
        {
            return _context.StructureAristas.AsNoTracking()
                        .Include(n => n.NodeFrom)
                        .Include(n => n.NodeTo)
                        .Where(a =>
                        a.StructureIdFrom == structureId && 
                        a.StructureIdTo == structureId &&
                        a.NodeIdFrom == nodeIdFrom &&
                        a.ValidityFrom <= validity
                    )
                    .AsQueryable();
        }

        public async Task<IList<StructureArista>> GetAllAristaByNodeTo(int nodeIdTo)
        {
            return await _context.StructureAristas.AsNoTracking()
                .Where(w => w.NodeIdTo == nodeIdTo)
                .ToListAsync();
        }

        /// <summary>
        /// Gets all arista.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StructureArista>> GetAllArista()
        {
            return await _context.StructureAristas.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Deletes the arista.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteArista(StructureArista entity)
        {
            _context.StructureAristas.Remove(entity);
        }

        /// <summary>
        /// Existses the in structures.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public async Task<bool> ExistsInStructures(int nodeId)
        {
            var list = await _context.StructureAristas.Where(a => a.NodeIdFrom == nodeId).ToListAsync();

            if (list.Count > 1)
                return true;

            return false;

        }

        public async Task<StructureArista> GetAristaById(int aristaId)
        {
            return await UntrackedSetArista()
                .Where(a => a.Id == aristaId)
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Clientes
        public async Task<bool> ExistsActiveTerritoryClientByNode(int nodeId, DateTimeOffset validityNode)
        {
            var list = await _context.StructureClientNodes.Where(a => a.NodeId == nodeId 
                                    && a.ValidityFrom <= validityNode && a.ValidityTo == DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)) //HACER: Ojo multipais
                                    && a.ClientState == "1").ToListAsync();

            if (list.Count > 0)
                return true;

            return false;
        }
        #endregion Clientes

        public void Update(StructureNode entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
