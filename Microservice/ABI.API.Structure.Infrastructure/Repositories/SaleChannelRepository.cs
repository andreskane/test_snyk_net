using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class SaleChannelRepository : GenericNormalizedRepository<int, SaleChannel, StructureContext>, ISaleChannelRepository
    {
        #region Contructor
        public SaleChannelRepository(StructureContext context) : base(context) { }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SaleChannel> GetByName(string name)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }



        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns></returns>
        public async Task<IList<SaleChannel>> GetAllActive(bool active)
            => await UntrackedSet().Where(n => n.Active == active).ToListAsync();

        #endregion
    }
}
