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
    public class CreateUpdateNodeEmployeeResponsableTerritoriesValidator : AbstractValidator<NodeCommand>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;

        private List<StructureNodeDefinition> _territories;
        private List<Tuple<int?, int?>> _responsableAttentionModes;


        public CreateUpdateNodeEmployeeResponsableTerritoriesValidator(IStructureNodeRepository repositoryStructureNode, IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _repositoryStructureNode = repositoryStructureNode;
            _attentionModeRoleRepo = attentionModeRoleRepo;

            _territories = new List<StructureNodeDefinition>();

            RuleFor(command => command)
                .Must(ValidResponsable).OnFailure(f => throw new NodeEmployeeResponsableTerritoriesException())
                .Must(ValidNoResponsable).OnFailure(f => throw new NodeEmployeeNoResponsableTerritoriesException());
        }


        protected override bool PreValidate(ValidationContext<NodeCommand> context, ValidationResult result)
        {
            var preValidate = base.PreValidate(context, result);

            if (!preValidate)
                return false;

            var command = context.InstanceToValidate;

            if (!command.EmployeeId.HasValue || !command.AttentionModeId.HasValue || command.LevelId != 8)
                return true;

            _territories = _repositoryStructureNode
                .GetTerritoriesByEmployeeId(command.StructureId, command.EmployeeId.Value, command.ValidityFrom)
                .GetAwaiter()
                .GetResult()
                .Where(w =>
                    (w.NodeId != command.Id) &&
                    (!w.VacantPerson.HasValue || w.VacantPerson == false)
                )
                .ToList();

            if (_territories.Any())
                _territories.Add(new StructureNodeDefinition(
                    0,
                    command.AttentionModeId,
                    command.RoleId,
                    command.SaleChannelId,
                    command.EmployeeId,
                    command.ValidityFrom,
                    command.Name,
                    command.Active
                ));

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
            if (!_territories.Any() || !request.EmployeeId.HasValue || !request.AttentionModeId.HasValue || request.LevelId != 8)
                return true;

            var allResponsable = _territories.All(all =>
                _responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.AttentionModeId) &&
                    a.Item2.Equals(all.RoleId)
                )
            );

            var responsable = _responsableAttentionModes.Any(a =>
                a.Item1.Equals(request.AttentionModeId) &&
                a.Item2.Equals(request.RoleId)
            );

            return (allResponsable == responsable);
        }

        private bool ValidNoResponsable(NodeCommand request)
        {
            if (!_territories.Any() || !request.EmployeeId.HasValue || !request.AttentionModeId.HasValue || request.LevelId != 8)
                return true;

            var allNoResponsable = _territories.All(all =>
                !_responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.AttentionModeId) &&
                    a.Item2.Equals(all.RoleId)
                )
            );

            var noResponsable = !_responsableAttentionModes.Any(a =>
                a.Item1.Equals(request.AttentionModeId) &&
                a.Item2.Equals(request.RoleId)
            );

            return (allNoResponsable == noResponsable);
        }
    }
}
