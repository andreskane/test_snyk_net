using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class VersionedStatus : BaseEntity<int>
    {
        public virtual ICollection<Versioned> Versioneds { get; set; }
    }
}
