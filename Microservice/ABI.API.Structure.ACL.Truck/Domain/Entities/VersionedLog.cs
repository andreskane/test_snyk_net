using ABI.Framework.MS.Entity;
using System;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class VersionedLog : BaseEntity<int>
    {
        public int VersionedId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int LogStatusId { get; set; }
        public string Detaill { get; set; }
        public Versioned Versioned { get; set; }
        public VersionedLogStatus LogStatus { get; set; }       
    }
}
