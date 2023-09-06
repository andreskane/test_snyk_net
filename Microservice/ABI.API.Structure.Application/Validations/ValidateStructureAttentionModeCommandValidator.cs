using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureAttentionModeCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public List<StructureModelDefinition> StructureModels { get; set; }

        public string MessageError { get; set; }

        public ValidateStructureAttentionModeCommandValidator()
        {
            NodesError = new List<DTO.StructureNodeDTO>();
        }

        /// <summary>
        /// Validates the attention mode.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public bool ValidateAttentionMode(IList<DTO.StructureNodeDTO> nodes)
        {
            MessageError = "Falta completar el modo de atención";

            var attentionModeIds = StructureModels.Where(m => m.IsAttentionModeRequired).Select(a => a.LevelId).ToArray();

            NodesError = nodes.Where(n => attentionModeIds.Contains(n.NodeLevelId) && n.NodeActive == true && !n.NodeAttentionModeId.HasValue).ToList();

            if (NodesError.Count > 0)
                return false;

            return true;

        }



    }

}
