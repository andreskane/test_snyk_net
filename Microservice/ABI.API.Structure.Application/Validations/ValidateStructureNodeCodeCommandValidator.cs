using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNodeCodeCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public List<StructureModelDefinition> StructureModels { get; set; }

        public string MessageError { get; set; }

        public ValidateStructureNodeCodeCommandValidator()
        {
            NodesError = new List<DTO.StructureNodeDTO>();
        }

        /// <summary>
        /// Validates the attention mode.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public bool ValidateNodeCode(IList<DTO.StructureNodeDTO> nodes)
        {
            MessageError = "Código del nodo repetido";

            foreach (var item in StructureModels)
            {
                var nodesLevel = (from nd in nodes.AsQueryable()
                                  where
                                    nd.NodeMotiveStateId == (int)MotiveStateNode.Confirmed &&
                                    nd.NodeLevelId == item.LevelId &&
                                    nd.NodeActive == true
                                  select new
                                  {
                                      nd.NodeId,
                                      nd.NodeName,
                                      nd.NodeCode,
                                      nd.NodeActive
                                  }).Distinct().ToList();

                var nodesDuplicate = nodesLevel.GroupBy(o => o.NodeCode)
                                    .Select(g => new { NodeCode = g.Key, orderCount = g.Count() })
                                    .Where(g => g.orderCount > 1)
                                    .ToList();

                if (nodesDuplicate.Count > 0)
                {
                    var nodeCodes = nodesDuplicate.Select(a => a.NodeCode).ToArray();

                    var errors = nodesLevel.Where(n => nodeCodes.Contains(n.NodeCode)).OrderBy(o => o.NodeCode).ToList();

                    foreach (var itemError in errors)
                    {
                        NodesError.Add(new DTO.StructureNodeDTO
                        {
                            NodeId = itemError.NodeId,
                            NodeName = itemError.NodeName,
                            NodeCode = itemError.NodeCode,
                            NodeActive = itemError.NodeActive,
                            NodeLevelId = item.LevelId
                        });
                    }
                }
            }

            if (NodesError.Count > 0)
                return false;

            return true;

        }
    }
}
