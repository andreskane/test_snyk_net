using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{
    public class CreateUpdateNodeEmployeeResponsableZonesValidator : AbstractValidator<NodeCommand>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;

        private List<StructureNodeDefinition> _zonesTerritories;
        private List<Tuple<int?, int?>> _responsableAttentionModes;
        private bool _noValidate;
        private bool _allResponsable;

        
        public CreateUpdateNodeEmployeeResponsableZonesValidator(IStructureNodeRepository repositoryStructureNode, IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _repositoryStructureNode = repositoryStructureNode;
            _attentionModeRoleRepo = attentionModeRoleRepo;

            _zonesTerritories = new List<StructureNodeDefinition>();

            RuleFor(command => command)
                .Must(ValidResponsable).OnFailure(f => throw new NodeEmployeeResponsableZonesException())
                .Must(ValidNoResponsable).OnFailure(f => throw new NodeEmployeeNoResponsableZonesException());
        }


        protected override bool PreValidate(ValidationContext<NodeCommand> context, ValidationResult result)
        {
            var preValidate = base.PreValidate(context, result);

            if (!preValidate)
                return false;

            var command = context.InstanceToValidate;

            if (!command.EmployeeId.HasValue || command.LevelId != 7)
            {
                _noValidate = true;
                return true;
            }

            _zonesTerritories = _repositoryStructureNode
                .GetTerritoriesByZonesEmployeeId(command.StructureId, command.EmployeeId.Value, command.ValidityFrom)
                .GetAwaiter()
                .GetResult()
                .Where(w =>
                    (w.NodeId != command.Id) &&
                    (!w.VacantPerson.HasValue || w.VacantPerson == false)
                )
                .ToList();

            if (_zonesTerritories.Any())
            {
                var territoriesDefinitions = _repositoryStructureNode
                    .GetNodoChildAllByNodeIdAsync(command.StructureId, command.Id)
                    .GetAwaiter()
                    .GetResult()
                    .SelectMany(s => s.StructureNodoDefinitions)
                    .Where(w => w.ValidityFrom <= command.ValidityFrom && w.ValidityTo >= command.ValidityFrom)
                    .ToList();

                var territoriesDraft = territoriesDefinitions
                    .Where(w => w.MotiveStateId == (int)MotiveStateNode.Draft)
                    .ToList();


                var territories = territoriesDefinitions
                    .Where(w => !territoriesDraft.Any(a => a.NodeId == w.NodeId && w.MotiveStateId == (int)MotiveStateNode.Confirmed))
                    .ToList();

                if (territories.Any())
                    _zonesTerritories.AddRange(territories);
            }

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
            if (!_zonesTerritories.Any() || _noValidate)
                return true;

            var allResponsable = _zonesTerritories.All(all =>
                _responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.AttentionModeId) &&
                    a.Item2.Equals(all.RoleId)
                )
            );

            if (!allResponsable)
                return true;

            _allResponsable = true;

            return true;
        }

        private bool ValidNoResponsable(NodeCommand request)
        {
            if (!_zonesTerritories.Any() || _allResponsable || _noValidate)
                return true;

            var allNoResponsable = _zonesTerritories.All(all =>
                !_responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.AttentionModeId) &&
                    a.Item2.Equals(all.RoleId)
                )
            );

            if (!allNoResponsable)
                return false;

            return true;
        }
    }
}
