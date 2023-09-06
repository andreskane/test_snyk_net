using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureSaleChannelCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public List<StructureModelDefinition> StructureModels { get; set; }

        public string MessageError { get; set; }

        public ValidateStructureSaleChannelCommandValidator()
        {
            NodesError = new List<DTO.StructureNodeDTO>();
        }

        /// <summary>
        /// Validates the attention mode.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public bool ValidateSaleChannel(IList<DTO.StructureNodeDTO> nodes)
        {
            MessageError = "Falta completar el canal de venta";

            var saleChannelIds = StructureModels.Where(m => m.IsSaleChannelRequired).Select(a => a.LevelId).ToList();

            NodesError = nodes.Where(n => saleChannelIds.Contains(n.NodeLevelId) && n.NodeActive == true && !n.NodeSaleChannelId.HasValue).ToList();

            if (NodesError.Count > 0)
                return false;

            return true;

        }



    }

}
