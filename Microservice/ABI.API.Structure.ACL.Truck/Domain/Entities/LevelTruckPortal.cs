using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class LevelTruckPortal : BaseEntity<int>
    {

        public int LevelTruck { get; set; }

        public string LevelTruckName { get; set; }

        public int LevelPortalId { get; set; }

        public string LevelPortalName { get; set; }

        public string TypeEmployeeTruck { get; set; }

        public int? RolPortalId { get; set; }

    }
}
