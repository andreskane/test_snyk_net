using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNodeLevelCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public List<StructureModelDefinition> StructureModels { get; set; }

        public string MessageError { get; set; }

        public ValidateStructureNodeLevelCommandValidator()
        {
            NodesError = new List<DTO.StructureNodeDTO>();
        }

        /// <summary>
        /// Validates the attention mode.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public bool ValidateLevel(IList<DTO.StructureNodeDTO> nodes)
        {
            MessageError = string.Empty;

            var lastLevel = StructureModels.OrderBy(s => s.Id).LastOrDefault();
            var firtLevel = StructureModels.OrderBy(s => s.Id).FirstOrDefault();

            var levelNodes = nodes
                .Where(n => n.NodeLevelId == firtLevel.LevelId && n.NodeActive.Value)
                .ToList();

            foreach (var item in levelNodes)
            {
                CheckLevel(nodes, item, lastLevel);
            }

            if (NodesError.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Checks the level.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="node">The node.</param>
        /// <param name="lastLevel">The last level.</param>
        private void CheckLevel(IList<DTO.StructureNodeDTO> nodes, DTO.StructureNodeDTO node, StructureModelDefinition lastLevel)
        {
            if (node.ContainsNodeId.HasValue)
            {
                var childs = nodes.Where(f => f.NodeId == node.ContainsNodeId.Value && f.NodeActive.Value).ToList();

                if (childs.Count == 0)
                {
                    CheckLevelChild(nodes, node, lastLevel);
                }

                foreach (var itemChild in childs)
                {
                    if (itemChild != null)
                    {
                        CheckLevel(nodes, itemChild, lastLevel);
                    }
                    else
                    {
                        NodesError.Add(node);
                    }
                }
            }
            else
                if (node.NodeLevelId != lastLevel.LevelId)
            {
                NodesError.Add(node);
            }

        }

        /// <summary>
        /// Checks the level child.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="node">The node.</param>
        /// <param name="lastLevel">The last level.</param>
        private void CheckLevelChild(IList<DTO.StructureNodeDTO> nodes, DTO.StructureNodeDTO node, StructureModelDefinition lastLevel)
        {
            var NodesContains = nodes.Where(f => f.NodeId == node.NodeId && f.NodeActive.Value).Select(s => s.ContainsNodeId).ToList();

            var NodeschildActive = nodes.Where(f => NodesContains.Contains(f.NodeId) && f.NodeActive.Value).ToList();

            if (NodeschildActive.Count == 0 && node.NodeLevelId != lastLevel.LevelId)
            {
                var nodeDupli = NodesError.FirstOrDefault(n => n.NodeId == node.NodeId);
                if (nodeDupli == null)
                    NodesError.Add(node);
            }
        }
    }


    public class Structure
    {
        public List<StructureLevel> Levels { get; set; }



        public Structure()
        {
            Levels = new List<StructureLevel>();
        }
    }

    public class StructureLevel
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }

        public List<StructureNodeDTO> Nodes { get; set; }

        public StructureLevel()
        {
            Nodes = new List<StructureNodeDTO>();
        }

    }

    public class StructureLevelNode
    {
        public virtual int NodeId { get; set; }
        public virtual int? NodeParentId { get; set; }
        public virtual int? ContainsNodeId { get; set; }
        public virtual string NodeName { get; set; }
        public virtual int NodeCode { get; set; }
        public virtual int NodeLevelId { get; set; }
        public virtual string NodeLevelName { get; set; }
        public virtual bool NodeActive { get; set; }

        public List<StructureLevelNode> Nodes { get; set; }

        public StructureLevelNode()
        {
            Nodes = new List<StructureLevelNode>();
        }

    }
}
