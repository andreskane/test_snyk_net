using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public class StructureNode : EntityDomain, IAggregateRoot
    {
        public string Code { get; private set; }
        public int LevelId { get; private set; }
        public Level Level { get; private set; }
        public StructureArista StructureArista { get; private set; }
        public StructureNodeDefinition StructureNodoDefinition { get; private set; }
        public virtual ICollection<StructureDomain> Structures { get; set; }
        public virtual ICollection<StructureArista> AristasTo { get; set; }
        public virtual ICollection<StructureArista> AristasFrom { get; set; }
        public virtual ICollection<StructureNodeDefinition> StructureNodoDefinitions { get; set; }
        public virtual ICollection<StructureClientNode> StructureClientNodes { get; set; }

        public StructureNode()
        {
            Structures = new HashSet<StructureDomain>();
            AristasTo = new HashSet<StructureArista>();
            AristasFrom = new HashSet<StructureArista>();
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
            StructureClientNodes = new HashSet<StructureClientNode>();

        }

        public StructureNode(int id)
        {
            Id = id;
        }

        public StructureNode(string code, int levelId)
        {
            Code = code;
            LevelId = levelId;

            Structures = new HashSet<StructureDomain>();
            AristasTo = new HashSet<StructureArista>();
            AristasFrom = new HashSet<StructureArista>();
            StructureNodoDefinitions = new HashSet<StructureNodeDefinition>();
            StructureClientNodes = new HashSet<StructureClientNode>();

        }

        public void AddDefinition(StructureNodeDefinition definition)
        {
            StructureNodoDefinitions.Add(definition);
        }

        public void EditCode(string code)
        {
            Code = code;
        }

        public void EditLevel(Level level)
        {
            Level = level;
        }

        public void EditStructureArista(StructureArista structureArista)
        {
            StructureArista = structureArista;
        }

        public void EditStructureNodeDefinition(StructureNodeDefinition structureNodoDefinition)
        {
            StructureNodoDefinition = structureNodoDefinition;
        }
    }
}
