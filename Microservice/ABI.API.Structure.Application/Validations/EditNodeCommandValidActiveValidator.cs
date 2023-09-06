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

    public class EditNodeCommandValidActiveValidator : AbstractValidator<EditNodeCommand>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;


        public EditNodeCommandValidActiveValidator(ILogger<EditNodeCommandValidActiveValidator> logger, IStructureNodeRepository repository)
        {
            _structureNodeRepository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(command => command).Must(ValidChild)
                .OnAnyFailure(x =>
                {
                    throw new ChildNodesActiveException();
                });

            RuleFor(command => command).Must(ValidParent)
                .OnAnyFailure(x =>
                {
                    throw new ParentNodesActiveException();
                });

            RuleFor(command => command).Must(ValidTerritoryClient)
                .OnAnyFailure(x =>
                {
                    throw new TerritoryClientActiveException();
                });
        }

        private bool ValidChild(EditNodeCommand editnode)
        {
            var children = _structureNodeRepository.GetNodoChildAllByNodeIdAsync(editnode.StructureId, editnode.Id).GetAwaiter().GetResult();

            if (children.Any())
            {
                var actualParent = _structureNodeRepository.GetAristaByNodeTo(editnode.StructureId, editnode.Id, editnode.ValidityFrom).GetAwaiter().GetResult();
                if (editnode.NodeIdParent.HasValue)
                { 
                    var editParent = _structureNodeRepository.GetAsync(editnode.NodeIdParent.Value).GetAwaiter().GetResult();

                    if (editParent != null && actualParent.NodeIdFrom != editnode.NodeIdParent)
                        return true;
                }

                foreach (var item in children)
                {
                    if (!editnode.Active)
                    {
                        var nodeDefinition = item.StructureNodoDefinitions
                            .FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);

                        if (nodeDefinition == null)
                            nodeDefinition = item.StructureNodoDefinitions
                                .FirstOrDefault(f =>
                                    f.ValidityFrom <= editnode.ValidityFrom &&
                                    f.ValidityTo >= editnode.ValidityTo &&
                                    f.MotiveStateId == (int)MotiveStateNode.Confirmed
                                );

                        if (nodeDefinition.Active)//Tiene nodos hijos activos
                            return false; //No permite editar
                    }
                }
            }

            return true;// puede editar
        }

        private bool ValidParent(EditNodeCommand editnode)
        {
            if (editnode.NodeIdParent.HasValue)
            {
                var actualParent = _structureNodeRepository.GetAristaByNodeTo(editnode.StructureId, editnode.Id, editnode.ValidityFrom).GetAwaiter().GetResult();
                var editParent = _structureNodeRepository.GetAsync(editnode.NodeIdParent.Value).GetAwaiter().GetResult();

                if (editParent != null && actualParent.NodeIdFrom == editnode.NodeIdParent)
                {
                    var nodeDefinition = editParent.StructureNodoDefinitions
                        .FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);

                    if (nodeDefinition == null)
                        nodeDefinition = editParent.StructureNodoDefinitions
                            .FirstOrDefault(f =>
                                f.ValidityFrom <= editnode.ValidityFrom &&
                                f.ValidityTo >= editnode.ValidityTo &&
                                f.MotiveStateId == (int)MotiveStateNode.Confirmed
                            );

                    if (!nodeDefinition.Active && editnode.Active)//Nodo padre no es activo
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
        private bool ValidTerritoryClient(EditNodeCommand editnode)
        {
            if (editnode.Id > 0 && editnode.LevelId == 8 && !editnode.Active)// inactivando Territorio
            {
                var existsClients = _structureNodeRepository.ExistsActiveTerritoryClientByNode(editnode.Id, editnode.ValidityFrom);
                if (existsClients != null)
                    return !existsClients.Result; // invierto el resultado, ya que si existe (true) debe retornar falso y viceversa
            }
            return true; // puede editar
        }
    }
}
