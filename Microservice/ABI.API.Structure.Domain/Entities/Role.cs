using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public bool Active { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<AttentionModeRole> AttentionModeRoles { get; set; }
        public virtual ICollection<StructureNodeDefinition> StructureNodoDefinitions { get; set; }

        public Role()
        {
            Active = true;
            AttentionModeRoles = new HashSet<AttentionModeRole>();
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
        }

    }
}
