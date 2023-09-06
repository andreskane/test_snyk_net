using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class StructureModel : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool CanBeExportedToTruck { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public virtual ICollection<StructureModelDefinition> StructureModelsDefinitions { get; set; }

        public virtual ICollection<StructureDomain> Structures { get; set; }

        public virtual Country Country { get; set; }

        public StructureModel()
        {
            CanBeExportedToTruck = true;
            StructureModelsDefinitions = new HashSet<StructureModelDefinition>();
            Structures = new HashSet<StructureDomain>();
            Country = null;
        }
    }
}
