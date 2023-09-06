using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Generics;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class LevelTruckPortalRepository : GenericRepository<int, LevelTruckPortal, TruckACLContext>, ILevelTruckPortalRepository
    {
        #region Contructor
        public LevelTruckPortalRepository(TruckACLContext context) : base(context) { }



        #endregion
    }
}
