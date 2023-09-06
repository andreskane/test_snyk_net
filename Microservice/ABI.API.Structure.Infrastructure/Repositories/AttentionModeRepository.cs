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
    public class AttentionModeRepository : GenericNormalizedRepository<int, AttentionMode, StructureContext>, IAttentionModeRepository
    {
        #region Contructor
        public AttentionModeRepository(StructureContext context) : base(context) { }

        public async Task<AttentionMode> GetByName(string name)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }

        public override async Task<IList<AttentionMode>> GetAll() =>
            await UntrackedSet().Include(x => x.AttentionModeRoles).ToListAsync();

        public async Task<IList<AttentionMode>> GetAllActive(bool active) =>
           await UntrackedSet().Include(x => x.AttentionModeRoles).Where(n => n.Active == active).ToListAsync();

        public override async Task<AttentionMode> GetById(int id) =>
            await UntrackedSet().Include(x => x.AttentionModeRoles).SingleOrDefaultAsync(x => x.Id == id);

        #endregion
    }
}
