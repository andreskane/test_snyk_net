using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Repository.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class EditNodeCommandHandler : IRequestHandler<EditNodeCommand, int>
    {
        private readonly StructureContext _context;
        private readonly IStructureNodeRepository _repositoryStructureNode;


        public EditNodeCommandHandler(IStructureNodeRepository repository, StructureContext context)
        {
            _repositoryStructureNode = repository ?? throw new ArgumentNullException(nameof(repository));
            _context = context;
        }


        public async Task<int> Handle(EditNodeCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _repositoryStructureNode.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var normalizedRequest = request.NormalizeString();

                    var nodeDefinitionNew = new StructureNodeDefinition(
                        normalizedRequest.Id,
                        normalizedRequest.AttentionModeId,
                        normalizedRequest.RoleId,
                        normalizedRequest.SaleChannelId,
                        normalizedRequest.EmployeeId,
                        normalizedRequest.ValidityFrom,
                        normalizedRequest.Name,
                        normalizedRequest.Active
                    );

                    nodeDefinitionNew.EditMotiveStateId((int)MotiveStateNode.Draft);

                    var nodeDefinition = await _repositoryStructureNode.GetNodoDefinitionAsync(normalizedRequest.Id, normalizedRequest.ValidityFrom, normalizedRequest.ValidityTo);
                    var nodeDefinitionLastCurrent = await _repositoryStructureNode.GetNodoDefinitionLastCurrentAsync(normalizedRequest.Id, normalizedRequest.ValidityTo);
                    var previousArista = await _repositoryStructureNode.GetAristaPrevious(request.StructureId, request.Id, request.ValidityFrom);
                    var currentArista = await _repositoryStructureNode.GetAristaByNodeTo(request.StructureId, request.Id, request.ValidityFrom);

                    if (nodeDefinition != null && nodeDefinition.MotiveStateId == (int)MotiveStateNode.Confirmed)
                    {
                        // edita y anula nodos con la misma fecha de vigencia
                        if (nodeDefinition.ValidityFrom == request.ValidityFrom)
                        {

                            if (CompareNodes(nodeDefinitionNew, nodeDefinition))
                            {
                                nodeDefinition.EditMotiveStateId((int)MotiveStateNode.Dropped);

                                _repositoryStructureNode.UpdateNodoDefinition(nodeDefinition);

                                nodeDefinitionNew.EditVacantPerson(nodeDefinition.VacantPerson);
                                SetVacantePerson(nodeDefinitionNew);

                                await AddDefinition(nodeDefinitionNew, cancellationToken);
                            }
                        }
                        else //Compara si hay diferencia genera una nueva version
                            if (CompareNodes(nodeDefinitionNew, nodeDefinition))
                            {
                                nodeDefinitionNew.EditVacantPerson(nodeDefinition.VacantPerson);
                                SetVacantePerson(nodeDefinitionNew);

                                await AddDefinition(nodeDefinitionNew, cancellationToken);
                            }
                    }
                    else
                    {
                        var node = await _repositoryStructureNode.GetAsync(normalizedRequest.Id);

                        if (node != null && nodeDefinition != null)
                        {
                            if (!CompareNodes(nodeDefinitionNew, nodeDefinitionLastCurrent) &&
                                request.NodeIdParent.HasValue &&
                                currentArista.NodeIdFrom == request.NodeIdParent.Value
                                && nodeDefinition.MotiveStateId == (int)MotiveStateNode.Confirmed)
                            {
                                _repositoryStructureNode.DeleteNodoDefinitions(nodeDefinition);
                            }
                            else
                            {
                                if (CompareNodes(nodeDefinitionNew, nodeDefinition))
                                {

                                    nodeDefinition.EditName(normalizedRequest.Name);
                                    nodeDefinition.EditActive(normalizedRequest.Active);
                                    nodeDefinition.EditAttentionModeId(normalizedRequest.AttentionModeId);
                                    nodeDefinition.EditRoleId(normalizedRequest.RoleId);
                                    nodeDefinition.EditSaleChannelId(normalizedRequest.SaleChannelId);
                                    nodeDefinition.EditEmployeeId(normalizedRequest.EmployeeId);

                                    SetVacantePerson(nodeDefinition);

                                    _repositoryStructureNode.UpdateNodoDefinition(nodeDefinition);
                                    await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                                }
                            }
                        }
                        if (node != null && node.Id == request.Id && node.Code != request.Code)
                        {
                            node.EditCode(request.Code);
                            _repositoryStructureNode.Update(node);
                            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                        }
                    }

                    if (currentArista != null)
                    {
                        if (currentArista.MotiveStateId == (int)MotiveStateNode.Confirmed)
                        {
                            if (request.NodeIdParent.HasValue && currentArista.NodeIdFrom != request.NodeIdParent.Value)
                            {
                                if (currentArista.ValidityFrom == request.ValidityFrom)
                                {
                                    currentArista.SetNodeParent(request.NodeIdParent.Value);
                                    await UpdateArista(request, currentArista, cancellationToken);
                                }
                                else
                                {
                                    await AddArista(request, cancellationToken);
                                }
                            }
                        }
                        else
                        {
                            if (previousArista != null && request.NodeIdParent.HasValue && request.NodeIdParent == previousArista.NodeIdFrom)
                            {
                                await DeleteArista(request, currentArista, cancellationToken);
                            }
                            else if (request.NodeIdParent.HasValue && request.NodeIdParent != currentArista.NodeIdFrom)
                            {
                                currentArista.SetNodeParent(request.NodeIdParent.Value);
                                await UpdateArista(request, currentArista, cancellationToken);
                            }
                        }
                    }
                    await _repositoryStructureNode.UnitOfWork.CommitTransactionAsync(transaction);
                    return normalizedRequest.Id;
                }
                catch (Exception ex)
                {
                    _repositoryStructureNode.UnitOfWork.RollbackTransaction();
                    throw new RepositoryException(ex.Message, ex);
                }
            }
        }

        private static void SetVacantePerson(StructureNodeDefinition nodeDefinitionNew)
        {
            if (nodeDefinitionNew.VacantPerson.HasValue)
            {
                if (nodeDefinitionNew.RoleId.HasValue && !nodeDefinitionNew.EmployeeId.HasValue && !nodeDefinitionNew.VacantPerson.Value)
                {
                    nodeDefinitionNew.EditEmployeeId(null);
                    nodeDefinitionNew.EditVacantPerson(true);
                }

                if (!nodeDefinitionNew.RoleId.HasValue && nodeDefinitionNew.EmployeeId.HasValue && !nodeDefinitionNew.VacantPerson.Value)
                {
                    nodeDefinitionNew.EditEmployeeId(null);
                    nodeDefinitionNew.EditVacantPerson(true);
                }

                if (!nodeDefinitionNew.RoleId.HasValue && !nodeDefinitionNew.EmployeeId.HasValue && !nodeDefinitionNew.VacantPerson.Value)
                {
                    nodeDefinitionNew.EditEmployeeId(null);
                    nodeDefinitionNew.EditVacantPerson(true);
                }

                if (nodeDefinitionNew.RoleId.HasValue && nodeDefinitionNew.EmployeeId.HasValue && nodeDefinitionNew.VacantPerson.Value)
                {
                    nodeDefinitionNew.EditVacantPerson(false);
                }
            }
        }

        #region Methods

        private async Task AddDefinition(StructureNodeDefinition node, CancellationToken cancellationToken)
        {
            _repositoryStructureNode.AddNodoDefinition(node);

            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task AddArista(EditNodeCommand request, CancellationToken cancellationToken)
        {
            var newArista = new StructureArista(
                request.StructureId,
                request.NodeIdParent.Value,
                request.StructureId,
                request.Id,
                1,
                request.ValidityFrom
            );

            newArista.EditMotiveStateId((int)MotiveStateNode.Draft);
            _repositoryStructureNode.AddArista(newArista);

            var parent = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.NodeIdParent &&
                    w.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo
                       || (w.MotiveStateId == (int)MotiveStateNode.Draft &&
                    w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo)
                )
                .FirstOrDefaultAsync();

            var node = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.Id &&
                    w.MotiveStateId == (int)MotiveStateNode.Draft
                )
                .FirstOrDefaultAsync();

            if (node == null)
            {
                node = await _context.StructureNodeDefinitions
                    .Where(w =>
                        w.NodeId == request.Id &&
                        w.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                        w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo
                    )
                    .FirstOrDefaultAsync();
            }

            if (parent != null && node.Active != parent.Active)
            {
                if (node.MotiveStateId == (int)MotiveStateNode.Draft)
                {
                    node.EditActive(parent.Active);
                    _repositoryStructureNode.UpdateNodoDefinition(node);
                }
                else
                {
                    var newDefinition = new StructureNodeDefinition(
                        node.NodeId,
                        node.AttentionModeId,
                        node.RoleId,
                        node.SaleChannelId,
                        node.EmployeeId,
                        request.ValidityFrom,
                        node.Name,
                        parent.Active
                    );

                    newDefinition.EditValidityTo(node.ValidityTo);
                    newDefinition.EditMotiveStateId((int)MotiveStateNode.Draft);
                    newDefinition.EditVacantPerson(node.VacantPerson);

                    _repositoryStructureNode.AddNodoDefinition(newDefinition);
                }
            }

            await UpdateActiveInBranch(request.StructureId, request.Id, parent.Active, request.ValidityFrom);
            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task UpdateArista(EditNodeCommand request, StructureArista arista, CancellationToken cancellationToken)
        {
            _repositoryStructureNode.UpdateArista(arista);

            var parent = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.NodeIdParent &&
                      (w.MotiveStateId == (int)MotiveStateNode.Confirmed || w.MotiveStateId == (int)MotiveStateNode.Draft) &&
                    w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo
                )
                .FirstOrDefaultAsync();

            var node = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.Id &&
                    w.MotiveStateId == (int)MotiveStateNode.Draft
                )
                .FirstOrDefaultAsync();

            if (node == null)
            {
                node = await _context.StructureNodeDefinitions
                    .Where(w =>
                        w.NodeId == request.Id &&
                        w.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                        w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo
                    )
                    .FirstOrDefaultAsync();
            }

            if (node.Active != parent.Active)
            {
                if (node.MotiveStateId == (int)MotiveStateNode.Draft)
                {
                    node.EditActive(parent.Active);
                    _repositoryStructureNode.UpdateNodoDefinition(node);
                }
                else
                {
                    var newDefinition = new StructureNodeDefinition(
                        node.NodeId,
                        node.AttentionModeId,
                        node.RoleId,
                        node.SaleChannelId,
                        node.EmployeeId,
                        request.ValidityFrom,
                        node.Name,
                        parent.Active
                    );

                    newDefinition.EditValidityTo(node.ValidityTo);
                    newDefinition.EditMotiveStateId((int)MotiveStateNode.Draft);
                    newDefinition.EditVacantPerson(node.VacantPerson);

                    _repositoryStructureNode.AddNodoDefinition(newDefinition);
                }

            }

            await UpdateActiveInBranch(request.StructureId, request.Id, parent.Active, request.ValidityFrom);
            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task DeleteArista(EditNodeCommand request, StructureArista arista, CancellationToken cancellationToken)
        {
            await Task.Run(() =>_repositoryStructureNode.DeleteArista(arista), cancellationToken);

            var parent = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.NodeIdParent &&
                    w.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    w.ValidityFrom <= request.ValidityFrom && w.ValidityTo >= request.ValidityTo
                )
                .FirstOrDefaultAsync();

            var node = await _context.StructureNodeDefinitions
                .Where(w =>
                    w.NodeId == request.Id &&
                    w.MotiveStateId == (int)MotiveStateNode.Draft
                )
                .FirstOrDefaultAsync();

            if (node != null)
                _repositoryStructureNode.DeleteNodoDefinitions(node);

            await UpdateActiveInBranch(request.StructureId, request.Id, parent.Active, request.ValidityFrom);
            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private bool CompareNodes(StructureNodeDefinition val1, StructureNodeDefinition val2)
        {

            var propertyValidate = new List<string> { "AttentionModeId", "RoleId", "SaleChannelId", "EmployeeId", "Name", "Active" };

            PropertyInfo[] Props1 = val1.GetType().GetProperties();
            PropertyInfo[] Props2 = val2.GetType().GetProperties();

            foreach (PropertyInfo pInfo1 in Props1)
            {
                PropertyInfo pInfo2 = Props2.FirstOrDefault(f => f.Name == pInfo1.Name);

                if (propertyValidate.Contains(pInfo1.Name))
                {
                    var value1 = pInfo1.GetValue(val1, null);
                    var value2 = pInfo2.GetValue(val2, null);

                    if (!Equals(value1, value2))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private async Task UpdateActiveInBranch(int structureId, int nodeId, bool active, DateTimeOffset validityFrom)
        {
            var children = await (
                from nd in _context.StructureNodeDefinitions
                join a in _context.StructureAristas on nd.NodeId equals a.NodeIdTo
                where
                    a.NodeIdFrom == nodeId &&
                    a.StructureIdFrom == structureId && a.StructureIdTo == structureId &&
                    a.ValidityFrom <= validityFrom && a.ValidityTo >= validityFrom &&
                    a.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    nd.ValidityFrom <= validityFrom && nd.ValidityTo >= validityFrom &&
                    (nd.MotiveStateId == (int)MotiveStateNode.Draft || nd.MotiveStateId == (int)MotiveStateNode.Confirmed)
                select nd
            )
            .ToListAsync();

            var grouped = children.GroupBy(g => g.NodeId);

            foreach (var c in grouped)
            {
                var draft = c.FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Draft);
                var actual = c.FirstOrDefault(f => f.MotiveStateId == (int)MotiveStateNode.Confirmed);

                if (draft != null && actual != null && actual.Active == active)
                {
                    _repositoryStructureNode.DeleteNodoDefinitions(draft);
                }
                else if (draft != null && draft.Active != active)
                {
                    draft.EditActive(active);
                    _repositoryStructureNode.UpdateNodoDefinition(draft);
                }
                else if (actual != null && actual.Active != active)
                {
                    var newDefinition = new StructureNodeDefinition(
                        actual.NodeId,
                        actual.AttentionModeId,
                        actual.RoleId,
                        actual.SaleChannelId,
                        actual.EmployeeId,
                        validityFrom,
                        actual.Name,
                        active
                    );

                    newDefinition.EditValidityTo(actual.ValidityTo);
                    newDefinition.EditMotiveStateId((int)MotiveStateNode.Draft);
                    newDefinition.EditVacantPerson(actual.VacantPerson);

                    _repositoryStructureNode.AddNodoDefinition(newDefinition);
                }

                await UpdateActiveInBranch(structureId, c.Key, active, validityFrom);
            }
        }

        #endregion
    }
}
