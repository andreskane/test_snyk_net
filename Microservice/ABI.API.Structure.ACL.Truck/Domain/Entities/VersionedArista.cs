using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class VersionedArista : BaseEntity<int>
    {
        public int VersionedId { get; set; }
        public int AristaId { get; set; }
        public Versioned Versioned { get; set; }
    }
}
