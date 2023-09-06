using System;
using System.Linq;

using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using V = ABI.Framework.MS.Net.RestClient;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.Logging;

namespace ABI.API.Structure.Application.Validations
{
    public class DeleteChangeCommandValidActiveValidator : AbstractValidator<DeleteChangeCommand>
    {
        private readonly IChangeTrackingRepository _changeTrackingRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private ChangeTracking _change;
        private EditNodeCommand _editnode;

        public DeleteChangeCommandValidActiveValidator(ILogger<DeleteChangeCommandValidActiveValidator> logger, IChangeTrackingRepository changeTrackingRepository, IStructureNodeRepository repository)
        {
            _structureNodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _changeTrackingRepository = changeTrackingRepository ?? throw new ArgumentNullException(nameof(changeTrackingRepository));
            _change = new ChangeTracking();

            RuleFor(command => command).Must(ValidChild)
                .OnAnyFailure(x =>
                {
                    throw new V.BadRequestException("Uno o varios niveles que contiene, se encuentra activo.");
                });

            RuleFor(command => command).Must(ValidParent)
                .OnAnyFailure(x =>
                {
                    throw new V.BadRequestException("El nivel Superior, se encuentra inactivo.");
                });

            RuleFor(command => command).Must(ValidTerritoryClient)
                .OnAnyFailure(x =>
                {
                    throw new V.BadRequestException("El territorio tiene al menos un cliente activo.");
                });
        }

        protected override bool PreValidate(ValidationContext<DeleteChangeCommand> context, ValidationResult result)
        {
            var preValidate = base.PreValidate(context, result);

            if (!preValidate)
                return false;

            var command = context.InstanceToValidate;

            _change = _changeTrackingRepository.GetById(command.Id, false).GetAwaiter().GetResult();
            int nodeId = 0;
            int? parentId = null;
            bool active = false;
            int levelId = 0;
            if (_change.IdObjectType == (int)ChangeTrackingObjectType.Node && _change.IdChangeType == 4 && _change.ChangedValueNode.Field == "Active")
            {
                var nodeDefinitionChange = _structureNodeRepository.GetNodoDefinitionByIdAsync(_change.IdOrigen, false).GetAwaiter().GetResult();
                nodeId = nodeDefinitionChange.NodeId;
                var arista = _structureNodeRepository.GetAristaPrevious(_change.IdStructure, nodeId, _change.ValidityFrom).GetAwaiter().GetResult();
                parentId = arista.NodeIdFrom;
                active = _change.ChangedValueNode.OldValue.ToUpper() == "TRUE";
                levelId = nodeDefinitionChange.Node.LevelId;
            }
            if (_change.IdObjectType == (int)ChangeTrackingObjectType.Arista)
            {
                var node = _structureNodeRepository.GetAsync(_change.IdDestino).GetAwaiter().GetResult();
                nodeId = node.Id;
                levelId = node.LevelId;
                parentId = _change.ChangedValueArista.AristaActual.OldValue.NodeIdFrom;
            }

            _editnode = new EditNodeCommand(nodeId, parentId, _change.IdStructure, "", "", active, null, null, null, null, levelId, _change.ValidityFrom, DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            return true;
        }

        private bool ValidChild(DeleteChangeCommand deleteChange)
        {
            var children = _structureNodeRepository.GetNodoChildAllByNodeIdAsync(_editnode.StructureId, _editnode.Id).GetAwaiter().GetResult();

            if (children.Any())
            {
                var actualParent = _structureNodeRepository.GetAristaByNodeTo(_editnode.StructureId, _editnode.Id, _editnode.ValidityFrom, false).GetAwaiter().GetResult();
                if (_editnode.NodeIdParent.HasValue)
                {
                    var editParent = _structureNodeRepository.GetAsync(_editnode.NodeIdParent.Value).GetAwaiter().GetResult();

                    if (editParent != null && actualParent.NodeIdFrom != _editnode.NodeIdParent)
                        return true;
                }

                foreach (var item in children)
                {
                    if ((_change.IdObjectType == (int)ChangeTrackingObjectType.Node && _change.IdChangeType == 4 && _change.ChangedValueNode.Field == "Active") && !_editnode.Active)
                    {
                        var nodeDefinition = item.StructureNodoDefinitions
                            .FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);

                        if (nodeDefinition == null)
                            nodeDefinition = item.StructureNodoDefinitions
                                .FirstOrDefault(f =>
                                    f.ValidityFrom <= _editnode.ValidityFrom &&
                                    f.ValidityTo >= _editnode.ValidityTo &&
                                    f.MotiveStateId == (int)MotiveStateNode.Confirmed
                                );

                        if (nodeDefinition.Active)//Tiene nodos hijos activos
                            return false; //No permite editar
                    }
                }
            }

            return true;// puede editar
        }

        private bool ValidParent(DeleteChangeCommand changeNode)
        {
            if (_editnode.NodeIdParent.HasValue)
            {
                var actualParent = _structureNodeRepository.GetAristaByNodeTo(_change.IdStructure, _editnode.Id, _change.ValidityFrom, false).GetAwaiter().GetResult();
                var editParent = _structureNodeRepository.GetAsync(_editnode.NodeIdParent.Value).GetAwaiter().GetResult();

                if (editParent != null && actualParent.NodeIdFrom == _editnode.NodeIdParent)
                {
                    var nodeDefinition = editParent.StructureNodoDefinitions
                        .FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);

                    if (nodeDefinition == null)
                        nodeDefinition = editParent.StructureNodoDefinitions
                            .FirstOrDefault(f =>
                                f.ValidityFrom <= _editnode.ValidityFrom &&
                                f.ValidityTo >= _editnode.ValidityTo &&
                                f.MotiveStateId == (int)MotiveStateNode.Confirmed
                            );

                    if (!nodeDefinition.Active && _editnode.Active)//Nodo padre no es activo
                        return false; //No permite editar
                }
            }
            return true; // puede editar
        }

        /// <summary>
        /// Valida que sea un territorio que se esté inactivando y revisa si existe por lo menos un cliente asociado al nodo y con fecha desde menor 
        /// o igual a la fecha desde del nodo a editar y que esté con vigencia abierta, con estado activo "1"
        /// </summary>
        /// <param name="editnode"></param>
        /// <returns></returns>
        private bool ValidTerritoryClient(DeleteChangeCommand changeNode)
        {
            if (_change.IdObjectType == (int)ChangeTrackingObjectType.Node && _change.IdChangeType == 4 && _change.ChangedValueNode.Field == "Active")
            {
                if (_editnode.Id > 0 && _editnode.LevelId == 8 && !_editnode.Active)// inactivando Territorio
                {
                    var existsClients = _structureNodeRepository.ExistsActiveTerritoryClientByNode(_editnode.Id, _editnode.ValidityFrom).GetAwaiter().GetResult();
                    return !existsClients; // invierto el resultado, ya que si existe (true) debe retornar falso y viceversa
                }
            }
            return true; // puede editar
        }
    }
}
