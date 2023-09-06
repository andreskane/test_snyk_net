using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateEditDateNodeValidityCommandValidator : AbstractValidator<EditNodeCommand>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;

        public ValidateEditDateNodeValidityCommandValidator(ILogger<ValidateEditDateNodeValidityCommandValidator> logger, IStructureNodeRepository repositoryStructureNode)
        {

            _repositoryStructureNode = repositoryStructureNode;

            RuleFor(command => command).Must(ValidDate)
            .OnAnyFailure(x =>
            {
                throw new NodeEditSameDateException();
            });
        }

        private bool ValidDate(EditNodeCommand node)
        {
            var nodeOld =  _repositoryStructureNode.GetNodoDefinitionValidityByNodeIdAsync(node.Id,node.ValidityFrom).GetAwaiter().GetResult();

            if (nodeOld !=  null && nodeOld.ValidityFrom == node.ValidityFrom)
                return false;

            return true;
        }
    }

}
