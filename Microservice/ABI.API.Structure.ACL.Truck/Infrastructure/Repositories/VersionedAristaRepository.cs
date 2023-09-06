using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class VersionedAristaRepository : GenericRepository<int, VersionedArista, TruckACLContext>, IVersionedAristaRepository
    {

        #region Contructor
        public VersionedAristaRepository(TruckACLContext context) : base(context) { }

        public async Task<IList<VersionedArista>> GetByListAristasDefinitionId(List<int> ids)
        {

            //todo:revisar y sacar del generic
            var res = await  UntrackedSet().AsNoTracking()
     .Include(i => i.Versioned)
     .ThenInclude(s => s.VersionedStatus)
     .Where(x => ids.Contains(x.Id)).ToListAsync();



            return res;
        }

        #endregion
    }
}
