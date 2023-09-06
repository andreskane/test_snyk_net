using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class VersionedNode : BaseEntity<int>
    {
        public int VersionedId { get; set; }
        public int NodeId { get; set; }
        public int NodeDefinitionId { get; set; }
        public Versioned Versioned { get; set; }
    }
}
