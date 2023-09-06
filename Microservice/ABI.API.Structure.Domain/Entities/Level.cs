using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class Level : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<StructureModelDefinition> StructureModelsDefinitions { get; set; }
        public virtual ICollection<StructureModelDefinition> ParentStructureModelsDefinitions { get; set; }
        public virtual ICollection<StructureNode> StructureNodos { get; set; }
        public Level()
        {
            Active = false;

            StructureModelsDefinitions = new HashSet<StructureModelDefinition>();
            ParentStructureModelsDefinitions = new HashSet<StructureModelDefinition>();
            StructureNodos = new HashSet<StructureNode>();

        }

    }
}
