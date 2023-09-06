using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{

    public class CreateUpdateNodeResponsableCommandValidator : AbstractValidator<NodeCommand>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;

        private List<StructureNode> _nodeSiblings;
        private List<Tuple<int?, int?>> _responsableAttentionModes;


        public CreateUpdateNodeResponsableCommandValidator(
            IStructureNodeRepository repositoryStructureNode,
            IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _repositoryStructureNode = repositoryStructureNode;
            _attentionModeRoleRepo = attentionModeRoleRepo;

            _nodeSiblings = new List<StructureNode>();

            RuleFor(command => command)
                .Must(ValidResponsable).OnFailure(f => throw new NodeResponsableException())
                .Must(ValidNoResponsable).OnFailure(f => throw new NodeNoResponsableException());
        }


        protected override bool PreValidate(ValidationContext<NodeCommand> context, ValidationResult result)
        {
            var preValidate = base.PreValidate(context, result);

            if (!preValidate)
                return false;

            var command = context.InstanceToValidate;

            if (!command.AttentionModeId.HasValue || command.LevelId != 8)
                return true;

            _nodeSiblings = _repositoryStructureNode
                .GetNodoChildAllByNodeIdAsync(command.StructureId, command.NodeIdParent.Value).GetAwaiter().GetResult();

            var attentionModeRoleRepo = _attentionModeRoleRepo.GetAll().GetAwaiter().GetResult();

            _responsableAttentionModes = attentionModeRoleRepo
                .Where(w => w.EsResponsable)
                .Select(s => new Tuple<int?, int?>(s.AttentionModeId, s.RoleId))
                .Distinct()
                .ToList();

            return true;
        }

        private bool ValidResponsable(NodeCommand request)
        {
            if (!_nodeSiblings.Any() || !request.AttentionModeId.HasValue || request.LevelId != 8)
                return true;

            var allResponsable = _nodeSiblings.All(all =>
                _responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.StructureNodoDefinitions.First().AttentionModeId) &&
                    a.Item2.Equals(all.StructureNodoDefinitions.First().RoleId)
                )
            );

            if (!allResponsable)
                return true;

            return _responsableAttentionModes.Any(a =>
                a.Item1.Equals(request.AttentionModeId) &&
                a.Item2.Equals(request.RoleId)
            );
        }

        private bool ValidNoResponsable(NodeCommand request)
        {
            if (!_nodeSiblings.Any() || !request.AttentionModeId.HasValue || request.LevelId != 8)
                return true;

            var allResponsable = _nodeSiblings.All(all =>
                _responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.StructureNodoDefinitions.First().AttentionModeId) &&
                    a.Item2.Equals(all.StructureNodoDefinitions.First().RoleId)
                )
            );

            if (allResponsable)
                return true;

            return !_responsableAttentionModes.Any(a =>
                a.Item1.Equals(request.AttentionModeId) &&
                a.Item2.Equals(request.RoleId)
            );
        }
    }
}
