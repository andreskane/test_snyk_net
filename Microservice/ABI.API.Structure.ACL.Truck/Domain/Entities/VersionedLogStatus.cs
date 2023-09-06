using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class VersionedLogStatus : BaseEntity<int>
    {

        public string Code { get; set; }

        public virtual ICollection<VersionedLog> VersionedLogs { get; set; }
    }
}
