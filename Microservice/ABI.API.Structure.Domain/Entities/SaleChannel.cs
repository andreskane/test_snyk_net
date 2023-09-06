using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class SaleChannel : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<StructureNodeDefinition> StructureNodoDefinitions { get; set; }

        public SaleChannel()
        {
            Active = false;
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
        }

    }



}
