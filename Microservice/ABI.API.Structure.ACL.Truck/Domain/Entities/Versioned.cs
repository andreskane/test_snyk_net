using ABI.Framework.MS.Entity;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class Versioned : BaseEntity<int>
    {
        public int StructureId { get; set; }
        public DateTimeOffset Validity { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Version { get; set; }
        public DateTimeOffset? GenerateVersionDate { get; set; }    
        public int StatusId { get; set; }
        public string User { get; set; }
        public VersionedStatus VersionedStatus { get; set; }
        public virtual ICollection<VersionedNode> VersionedsNode { get; set; }
        public virtual ICollection<VersionedArista> VersionedsArista { get; set; }
        public virtual ICollection<VersionedLog> VersionedsLog { get; set; }
    }
}
