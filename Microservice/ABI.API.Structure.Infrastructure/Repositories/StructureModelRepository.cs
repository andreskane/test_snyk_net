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
    public class StructureModelRepository : GenericNormalizedRepository<int, StructureModel, StructureContext>, IStructureModelRepository
    {
        #region Contructor

        public StructureModelRepository(StructureContext context) : base(context) { }

        #endregion

        /// <summary>
        /// Deletes the asyn.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="RepositoryException"></exception>
        public async Task<int> DeleteAsync(StructureModel entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = await dbContext.FindAsync<StructureModel>(new object[] { entity.Id }, cancellationToken);
            Set().Remove(dbEntity);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<StructureModel> GetByName(string name, CancellationToken cancellationToken = default)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }


        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async override Task<StructureModel> GetById(int id)
            => await UntrackedSet().Include(x => x.StructureModelsDefinitions).SingleOrDefaultAsync(x => x.Id == id);


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async override Task<IList<StructureModel>> GetAll()
            => await UntrackedSet().Include(x => x.StructureModelsDefinitions).Include(x => x.Country).Include(x => x.Structures).ToListAsync();

        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns></returns>
        public async Task<IList<StructureModel>> GetAllActive(bool active, CancellationToken cancellationToken = default)
         => await UntrackedSet().Include(x => x.StructureModelsDefinitions).Include(x => x.Country).Where(n => n.Active == active).ToListAsync(cancellationToken);

        /// <summary>
        /// Get by name and country
        /// </summary>
        /// <param name="name"></param>
        /// <param name="countryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StructureModel> GetByNameAndCountry(string name, int countryId, CancellationToken cancellationToken = default)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text && p.CountryId == countryId
                    select p).FirstOrDefault();
        }

        /// <summary>
        /// Get by code and country
        /// </summary>
        /// <param name="code"></param>
        /// <param name="countryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StructureModel> GetByCodeAndCountry(string code, int countryId, CancellationToken cancellationToken = default)
        {
            var text = code.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Code.NormalizeTextLatam().ToUpper() == text && p.CountryId == countryId
                    select p).FirstOrDefault();
        }
    }
}


