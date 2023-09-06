using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories
{
    public class AttentionModeRoleRepository : GenericNormalizedRepository<int, AttentionModeRole, StructureContext>, IAttentionModeRoleRepository
    {
        #region Contructor
        public AttentionModeRoleRepository(StructureContext context) : base(context) { }

        public override async Task<IList<AttentionModeRole>> GetAll() =>
            await UntrackedSet().Include(x => x.AttentionMode).Include(r => r.Role).ToListAsync();

        public async Task<IList<AttentionModeRole>> GetAllByRoleId(int roleId) =>
       await UntrackedSet().Include(x => x.AttentionMode).Include(r => r.Role)
            .Where(r => r.RoleId == roleId).ToListAsync();

        public async Task<AttentionModeRole> GetRoleById(int roleId) =>
            await UntrackedSet().Include(x => x.AttentionMode)
                        .Include(r => r.Role).SingleOrDefaultAsync(x => x.RoleId == roleId);
        #endregion
    }
}
