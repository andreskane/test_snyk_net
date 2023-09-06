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
    public class VersionedNodeRepository : GenericRepository<int, VersionedNode, TruckACLContext>, IVersionedNodeRepository
    {

        #region Contructor
        public VersionedNodeRepository(TruckACLContext context) : base(context) { }

       

        #endregion

        /// <summary>
        /// Gets the one by node definition identifier.
        /// </summary>
        /// <param name="nodeDefinitionId">The node definition identifier.</param>
        /// <returns></returns>
        public async Task<VersionedNode> GetOneByNodeDefinitionId(int nodeDefinitionId)
        {
            return await UntrackedSet().Where(n => n.NodeDefinitionId == nodeDefinitionId).OrderByDescending(o => o.VersionedId).FirstOrDefaultAsync();
        }

        public async Task<IList<VersionedNode>> GetByListNodeDefinitionId(List<int> ids)
        {

            var res= await UntrackedSet().AsNoTracking()
                .Include(i=>i.Versioned)
                .ThenInclude(s=>s.VersionedStatus)
                .Where(x => ids.Contains(x.NodeId)).ToListAsync();



            return res;

           
        }


    }
}
