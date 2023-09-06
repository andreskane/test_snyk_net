using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Repository.Exceptions;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class LevelRepository : GenericNormalizedRepository<int, Level, StructureContext>, ILevelRepository
    {

        #region Contructor

        public LevelRepository(StructureContext context) : base(context) { }

        #endregion

        /// <summary>
        /// Deletes the asyn.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="RepositoryException"></exception>
        public async Task<int> DeleteAsync(Level entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = await dbContext.FindAsync<Level>(new object[] { entity.Id }, cancellationToken);
            Set().Remove(dbEntity);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>

        public async Task<Level> GetByName(string name, CancellationToken cancellationToken = default)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }
        public async override Task<Level> GetById(int id) => await UntrackedSet().Include(x => x.StructureModelsDefinitions).Include(b => b.ParentStructureModelsDefinitions)
         .SingleOrDefaultAsync(x => x.Id == id);


        public async override Task<IList<Level>> GetAll() => await UntrackedSet().Include(x => x.StructureModelsDefinitions).Include(b => b.ParentStructureModelsDefinitions)
            .ToListAsync();


        public async Task<IList<Level>> GetAllActive(bool active, CancellationToken cancellationToken = default) => await UntrackedSet().Include(x => x.StructureModelsDefinitions).Include(b => b.ParentStructureModelsDefinitions)
            .Where(n => n.Active == active).ToListAsync(cancellationToken);
    }
}
