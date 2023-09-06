using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using ABI.Framework.MS.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.RepositoriesDomain
{
    public class StructureRepository : IStructureRepository
    {
        private readonly StructureContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public StructureRepository(StructureContext context) => _context = context;

        #region Operation

        public StructureDomain Add(StructureDomain entity)
        {
            var normalizedEntity = entity.NormalizeString();
            return _context.Structures.Add(normalizedEntity).Entity;
        }

        public StructureDomain AddMigration(StructureDomain entity)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            var structure = _context.Structures.Add(entity).Entity;

            return structure;
        }

        public void Delete(StructureDomain entity)
        {
            _context.Structures.Remove(entity);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>

        public async Task<IList<StructureDomain>> GetAllAsync() => await _context.Structures.ToListAsync();

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetAsync(int id) => await _context.Structures.FindAsync(id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(StructureDomain entity)
        {
            var normalizedEntity = entity.NormalizeString();
            _context.Entry(normalizedEntity).State = EntityState.Modified;
        }
        #endregion

        #region Get

        /// <summary>
        /// Gets the structure node root asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetStructureNodeRootAsync(int structureId)
        {

            var structure = await (from est in _context.Structures.AsNoTracking().Include(m => m.StructureModel).AsNoTracking()
                                   .Include(n => n.Node).ThenInclude(d => d.StructureNodoDefinitions).AsNoTracking()
                                   where est.Id == structureId
                                   select est
                                   ).FirstOrDefaultAsync();

            return await Task.Run(() => structure);
        }

        /// <summary>
        /// Gets the structure data complete asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetStructureDataCompleteAsync(int structureId)
        {

            var structure = await (from est in _context.Structures.AsNoTracking().Include(n => n.Node).AsNoTracking()
                                   .Include(m => m.StructureModel).ThenInclude(s => s.StructureModelsDefinitions).ThenInclude(l => l.Level).AsNoTracking()
                                   where est.Id == structureId
                                   select est
                                   ).FirstOrDefaultAsync();

            return await Task.Run(() => structure);
        }

        /// <summary>
        /// Gets the structure data complete byname asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetStructureDataCompleteByNameAsync(string name)
        {

            var structure = await (from est in _context.Structures.AsNoTracking().Include(n => n.Node).AsNoTracking()
                                   .Include(m => m.StructureModel).ThenInclude(c=> c.Country)
                                   .Include(m => m.StructureModel).ThenInclude(s => s.StructureModelsDefinitions).ThenInclude(l => l.Level).AsNoTracking()
                                   where est.Name == name
                                   select est
                                   ).FirstOrDefaultAsync();

            return await Task.Run(() => structure);
        }

        /// <summary>
        /// Gets the structure by name asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetStructureByNameAsync(string name)
        {

            var structure = await (from est in _context.Structures.AsNoTracking()
                                   where  est.Name == name
                                   select est
                                   ).FirstOrDefaultAsync();

            return await Task.Run(() => structure);
        }

        /// <summary>
        /// Gets the structure by code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<StructureDomain> GetStructureByCodeAsync(string code)
        {

            var structure = await (from est in _context.Structures.AsNoTracking()
                                   where est.Code == code
                                   select est
                                   ).FirstOrDefaultAsync();

            return await Task.Run(() => structure);
        }

        /// <summary>
        /// Gets all structure node root asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StructureDomain>> GetAllStructureNodeRootAsync(string country)
        {

            var structures = (from est in _context.Structures.AsNoTracking()
                                .Include(m => m.StructureModel).ThenInclude(c=> c.Country).AsNoTracking()      
                                .Include(n => n.Node).ThenInclude(d => d.StructureNodoDefinitions).AsNoTracking()   
                                select est
                                   );

            if(!string.IsNullOrEmpty(country))
            {
                structures = structures.AsQueryable().Where(s => s.StructureModel.Country.Code == country);
            }

            return await Task.Run(() => structures.ToListAsync());
        }

        public async Task<IList<GenericKeyValue>> GetAllStructuresChangesTracking(CancellationToken cancellationToken = default)
        {
            var results = await _context.ChangesTracking
                .AsNoTracking()
                .Select(s => s.IdStructure)
                .Distinct()
                .Join(_context.Structures.AsNoTracking(),
                    c => c,
                    s => s.Id,
                    (c, s) => s
                )
                .Select(s => new GenericKeyValue
                {
                    Id = s.Id.ToString(),
                    Name = s.Name
                })
                .ToListAsync(cancellationToken);

            return results;
        }
        #endregion
    }

}
