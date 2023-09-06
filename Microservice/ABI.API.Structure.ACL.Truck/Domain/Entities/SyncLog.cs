using ABI.Framework.MS.Entity;
using System;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class SyncLog : IEntity<long>
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }
    }
}
