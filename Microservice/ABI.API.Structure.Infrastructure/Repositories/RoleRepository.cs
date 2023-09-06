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
    public class RoleRepository : GenericNormalizedRepository<int, Role, StructureContext>, IRoleRepository
    {
        #region Contructor

        public RoleRepository(StructureContext context) : base(context) { }

        #endregion

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<Role> GetByName(string name)
        {
            var text = name.ToUpper().NormalizeTextLatam();

            var entitys = await GetAll();

            return (from p in entitys
                    where p.Name.NormalizeTextLatam().ToUpper() == text
                    select p).FirstOrDefault();
        }

        public override async Task<Role> GetById(int id) =>
              await UntrackedSet().Include(x => x.AttentionModeRoles).ThenInclude(b => b.AttentionMode)
            .SingleOrDefaultAsync(x => x.Id == id);

        public override async Task<IList<Role>> GetAll() =>
        await UntrackedSet().Include(x => x.AttentionModeRoles).ThenInclude(b => b.AttentionMode)
            .ToListAsync();

        public async Task<IList<string>> GetAllTag()
        {
            var listTags = new List<string>();

            var list = await UntrackedSet().Where(t => t.Tags != null).Select(a => a.Tags).ToListAsync();

            foreach (var item in list)
            {
                var tag = item.Split(',');
                listTags.AddRange(from itemt in tag
                                  select itemt);
            }

            return listTags.Distinct().OrderBy(q => q).ToList();
        }


        public async Task<IList<Role>> GetAllActive(bool active) => await UntrackedSet().Include(x => x.AttentionModeRoles).ThenInclude(b => b.AttentionMode)
          .Where(n => n.Active == active).ToListAsync();


        public override async Task Update(Role entity)
        {
            var dbEntity = await dbContext.Set<Role>()
                .Include(a => a.AttentionModeRoles).FirstOrDefaultAsync(i => i.Id == entity.Id);

            dbContext.Entry(dbEntity).CurrentValues.SetValues(entity);

            var attentionModeRole = dbEntity.AttentionModeRoles.FirstOrDefault();
            var attentionModeRoleNew = entity.AttentionModeRoles.FirstOrDefault();
            if(attentionModeRole == null)
            {
                dbContext.AttentionModeRole.Add(attentionModeRoleNew);
            }else
            {
                attentionModeRole.AttentionModeId = attentionModeRoleNew.AttentionModeId;
                dbContext.AttentionModeRole.Update(attentionModeRole);
            }

            await base.Update(dbEntity);
        }
    }
}
