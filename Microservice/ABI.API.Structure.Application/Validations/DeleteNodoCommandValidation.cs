using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;

namespace ABI.API.Structure.Application.Validations
{
    public class DeleteNodoCommandValidation : AbstractValidator<DeleteNodeCommand>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;

        public DeleteNodoCommandValidation(ILogger<DeleteNodoCommandValidation> logger, IStructureNodeRepository repository)
        {
            _structureNodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(command => command).Must(ValidContainsChildNodes)
                            .OnAnyFailure(x =>
                            {
                                throw new ContainsChildNodesException();
                            });
        }

        private bool ValidContainsChildNodes(DeleteNodeCommand node)
        {
            var result = _structureNodeRepository.ContainsChildNodes(node.StructureId, node.Id).Result;
            return !result;
        }

    }
}
