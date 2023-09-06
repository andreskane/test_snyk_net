using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class Type : BaseEntity<int>
    {
        public string Code { get; set; }
        public int TypeGroupId { get; set; }
        public virtual TypeGroup TypeGroup { get; set; }
        public virtual ICollection<StructureArista> StructureArista { get; set; }
        public virtual ICollection<ChangeTracking> ChangeTracking { get; set; }


        public Type()
        {
            StructureArista = new HashSet<StructureArista>();
            ChangeTracking = new HashSet<ChangeTracking>();
        }
    }
}
