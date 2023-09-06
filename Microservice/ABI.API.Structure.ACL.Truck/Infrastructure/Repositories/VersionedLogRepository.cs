using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class VersionedLogRepository : GenericRepository<int, VersionedLog, TruckACLContext>, IVersionedLogRepository
    {

        #region Contructor
        public VersionedLogRepository(TruckACLContext context) : base(context) { }

        #endregion

        /// <summary>
        /// Gets the status by versioned identifier.
        /// </summary>
        /// <param name="versionedId">The versioned identifier.</param>
        /// <returns></returns>
        public async Task<VersionedLog> GetLastStateByVersionedId(int versionedId)
        {
            return await UntrackedSet().Where(n => n.VersionedId == versionedId).Include(l=>l.LogStatus).OrderByDescending(t => t.Date).FirstOrDefaultAsync();
        }
    }
}
