using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class AttentionMode : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<AttentionModeRole> AttentionModeRoles { get; set; }

        public virtual ICollection<StructureNodeDefinition> StructureNodoDefinitions { get; set; }

        public AttentionMode()
        {
            AttentionModeRoles = new HashSet<AttentionModeRole>();
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
            Active = false;
        }

    }



}
