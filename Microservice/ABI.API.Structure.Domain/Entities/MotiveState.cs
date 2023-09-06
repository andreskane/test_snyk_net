using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class MotiveState : BaseEntity<int>
    {
        public int StateId { get; set; }
        public int MotiveId { get; set; }
        public State State { get; set; }
        public Motive Motive { get; set; }

        public virtual ICollection<StructureNodeDefinition> StructureNodoDefinitions { get; set; }
        public virtual ICollection<StructureArista> StructureAristas { get; set; }

        public MotiveState()
        {
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
            StructureAristas = new HashSet<StructureArista>();
        }
    }
}
