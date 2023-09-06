using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNodeNameNullCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public StructureDomain Structure { get; set; }

        public string MessageError { get; set; }

        private readonly IStructureNodeRepository _structureNodeRepository;



        public ValidateStructureNodeNameNullCommandValidator(IStructureNodeRepository repository)
        {
            _structureNodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));

            NodesError = new List<DTO.StructureNodeDTO>();
        }


        public bool Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            if (!Structure.StructureModel.CanBeExportedToTruck)
                return true;

            MessageError = "Nombre del nodo vacío";

            var invalidNodes = nodes
                .Where(w => string.IsNullOrWhiteSpace(w.NodeName))
                .GroupBy(a => new { a.NodeId, a.NodeCode, a.NodeName, a.NodeLevelId })
                .Select(n => new DTO.StructureNodeDTO { NodeId = n.Key.NodeId, NodeCode = n.Key.NodeCode, NodeName = n.Key.NodeName, NodeLevelId = n.Key.NodeLevelId })
                .Where(c =>
                {
                    var node = _structureNodeRepository.GetNodoDefinitionPendingAsync(c.NodeId).GetAwaiter().GetResult();

                    if (node != null && !string.IsNullOrWhiteSpace(node.Name))
                        return false;

                    return true;
                });

            NodesError.AddRange(invalidNodes);

            if (NodesError.Count > 0)
                return false;

            return true;
        }
    }
}
