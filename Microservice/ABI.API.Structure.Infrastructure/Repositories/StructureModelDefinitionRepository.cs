using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Exceptions;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class StructureModelDefinitionRepository : GenericNormalizedRepository<int, StructureModelDefinition, StructureContext>, IStructureModelDefinitionRepository
    {
        #region Contructor

        public StructureModelDefinitionRepository(StructureContext context) : base(context) { }

        #endregion

        /// <summary>
        /// Deletes the asyn.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="RepositoryException"></exception>
        public async Task<int> DeleteAsync(StructureModelDefinition entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = await dbContext.FindAsync<StructureModelDefinition>(new object[] { entity.Id }, cancellationToken);
            Set().Remove(dbEntity);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public override async Task<IList<StructureModelDefinition>> GetAll() =>
            await UntrackedSet().Include(s => s.StructureModel).Include(l => l.Level)
            .Include(p => p.ParentLevel)
            .ToListAsync();


        public override async Task<StructureModelDefinition> GetById(int id) =>
            await UntrackedSet().Include(s => s.StructureModel).Include(l => l.Level)
            .Include(p => p.ParentLevel)
            .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<StructureModelDefinition> GetLevelByLevelId(int id, int levelId, CancellationToken cancellationToken = default) =>
       await UntrackedSet().Include(s => s.StructureModel).Include(l => l.Level)
       .Include(p => p.ParentLevel)
       .SingleOrDefaultAsync(x => x.StructureModelId == id && x.LevelId == levelId, cancellationToken);

        public async Task<IList<StructureModelDefinition>> GetAllByStructureModel(int id, CancellationToken cancellationToken = default) =>
            await UntrackedSet().Include(s => s.StructureModel).Include(l => l.Level)
            .Include(p => p.ParentLevel).Where(x => x.StructureModelId == id)
            .ToListAsync(cancellationToken);

        public async Task<IList<StructureModelDefinition>> GetAllByStructureModelWithOutLevel(int id, CancellationToken cancellationToken = default) =>
            await UntrackedSet().Include(s => s.StructureModel)
            .Include(p => p.ParentLevel).Where(x => x.StructureModelId == id)
            .ToListAsync(cancellationToken);

    }
}
