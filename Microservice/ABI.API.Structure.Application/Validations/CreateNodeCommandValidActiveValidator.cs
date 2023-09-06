using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{
    public class CreateNodeCommandValidActiveValidator : AbstractValidator<CreateNodeCommand>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;

        public CreateNodeCommandValidActiveValidator(ILogger<CreateNodeCommandValidActiveValidator> logger, IStructureNodeRepository repository)
        {
            _structureNodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(command => command).Must(ValidParent)
                .OnAnyFailure(x =>
                {
                    throw new ParentNodesActiveException();
                });
        }

        private bool ValidParent(CreateNodeCommand newNode)
        {
            if (newNode.NodeIdParent.HasValue)
            {
                var editParent = _structureNodeRepository.GetAsync(newNode.NodeIdParent.Value).GetAwaiter().GetResult();

                if (editParent != null)
                {
                    var nodeDefinition = editParent.StructureNodoDefinitions
                        .FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft || f.MotiveStateId == (int)MotiveStateNode.Confirmed);

                    if (nodeDefinition != null && !nodeDefinition.Active && newNode.Active)//Nodo padre no es activo
                        return false; //No permite editar
                }
            }
            return true; // puede editar
        }
    }
}
