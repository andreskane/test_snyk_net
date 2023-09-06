using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Exceptions;
using Coravel.Queuing.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class SaveChangesWithoutSavingCommandHandler : IRequestHandler<SaveChangesWithoutSavingCommand, bool>
    {
        private readonly IStructureNodeRepository _repository;
        private readonly IQueue _queue;
        private readonly IChangeTrackingRepository _changeTracking;
        private readonly IChangeTrackingStatusRepository _changeTrackingStatus;
        private readonly ICurrentUserService _currentUser;
        private readonly IMediator _mediator;

        public SaveChangesWithoutSavingCommandHandler(IStructureNodeRepository repository, IChangeTrackingRepository changeTracking,
            ICurrentUserService currentUser, IQueue queue, IMediator mediator, IChangeTrackingStatusRepository changeTrackingStatus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _changeTracking = changeTracking ?? throw new ArgumentNullException(nameof(changeTracking));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _changeTrackingStatus = changeTrackingStatus ?? throw new ArgumentNullException(nameof(changeTrackingStatus));
        }


        public async Task<bool> Handle(SaveChangesWithoutSavingCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _repository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var nodes = await _mediator.Send(new GetStructureNodesPendingWithoutSavingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

                    foreach (var item in nodes)
                    {
                        if (item.NodeMotiveStateId == (int)MotiveStateNode.Draft)
                        {
                            var nodeDefininitioOld = await _repository.GetNodoDefinitionPreviousByNodeIdAsync(item.NodeId, request.ValidityFrom);

                            if (nodeDefininitioOld != null)
                            {
                                nodeDefininitioOld.EditValidityTo(request.ValidityFrom.AddDays(-1));

                                _repository.UpdateNodoDefinition(nodeDefininitioOld);
                            }
                            else
                            {
                                nodeDefininitioOld = await _repository.GetNodoDefinitionValidityByNodeIdDroppedAsync(item.NodeId, request.ValidityFrom); //obtengo el dropped de la misma fecha ya que esto modificando
                                if (nodeDefininitioOld == null)
                                    nodeDefininitioOld = new StructureNodeDefinition();//Si es null es porque es un nodo nuevo
                            }

                            var nodeDesf = await _repository.GetNodoDefinitionByIdAsync(item.NodeDefinitionId);


                            nodeDesf.EditValidityFrom(request.ValidityFrom);
                            nodeDesf.EditMotiveStateId((int)MotiveStateNode.Confirmed);

                            var nodeFuture = await _mediator.Send(new GetOneNodePendingScheduledChangesQuery { StructureId = request.StructureId, NodeId = nodeDesf.NodeId, ValidityFrom = nodeDesf.ValidityFrom });

                            if (nodeFuture != null)
                                nodeDesf.EditValidityTo(nodeFuture.NodeValidityFrom.AddDays(-1));
                 
                            _repository.UpdateNodoDefinition(nodeDesf);
                                
                            await RegisterNodeChanges(request.StructureId, request.ValidityFrom, nodeDefininitioOld, nodeDesf);
                        }

                        if (item.AristaMotiveStateId == (int)MotiveStateNode.Draft)
                        {
                            var newArista = await _repository.GetAristaPendient(request.StructureId, item.NodeId, request.ValidityFrom);

                            if (newArista != null)
                            {
                                newArista.EditValidityFrom(request.ValidityFrom);
                                newArista.EditMotiveStateId((int)MotiveStateNode.Confirmed);

                                _repository.UpdateArista(newArista);

                                var oldArista = await _repository.GetAristaPrevious(request.StructureId, item.NodeId, request.ValidityFrom);
                                if (oldArista != null)
                                {
                                    oldArista.EditValidityTo(request.ValidityFrom.AddDays(-1));
                                    _repository.UpdateArista(oldArista);
                                }
                                else
                                    oldArista = new StructureArista();
                                var oldValidity = oldArista.ValidityTo;
                                await RegisterAristaChanges(request.StructureId, oldValidity, request.ValidityFrom, newArista.Id, oldArista, newArista);
                            }
                        }
                    }

                    await _repository.UnitOfWork.CommitTransactionAsync(transaction);

                    // envio a Truck los cambios
                    await SendToTruck(request.StructureId, request.ValidityFrom);

                    return true;
                }
                catch (Exception ex)
                {
                    _repository.UnitOfWork.RollbackTransaction();

                    throw new RepositoryException(ex.Message, ex);
                }
            }
        }

        private async Task SendToTruck(int structureId, DateTimeOffset date)
        {
            var structure = await _mediator.Send(new Queries.Structures.GetByIdQuery { Id = structureId });

            if (structure != null && structure.StructureModel != null && structure.StructureModel.CanBeExportedToTruck)
            {

                var param = new TruckWritingPayload
                {
                    StructureId = structureId,
                    StructureName = structure.Name,
                    Date = date,
                    Username = _currentUser.UserName
                };

                _queue.QueueInvocableWithPayload<TruckWritingExecutor, TruckWritingPayload>(param);
            }
        }

        private async Task RegisterNodeChanges(int structureId, DateTimeOffset validityFrom, StructureNodeDefinition oldValue, StructureNodeDefinition newValue)
        {
            var user = new User
            {
                Id = _currentUser.UserId,
                Name = _currentUser.UserName
            };

            var changesNode = CompareNodes(oldValue, newValue);
            var pathNodes = await GetNodesPath(structureId, validityFrom, newValue.Node.Id);

            foreach (ChangeTrackingNode item in changesNode)
            {
                var changeType = GetChangeType(item.Field);

                item.Node = new ItemNode { 
                    Code = newValue.Node.Code,
                    Id = newValue.Node.Id,
                    Name = newValue.Name
                };

                var change = new ChangeTracking
                {
                    User = user,
                    CreateDate = DateTimeOffset.UtcNow.ToOffset(new TimeSpan(-3,0,0)),
                    IdStructure = structureId,
                    IdObjectType = (int)ChangeTrackingObjectType.Node,
                    ChangedValueNode = item,
                    IdChangeType = changeType,
                    NodePath = pathNodes,
                    ValidityFrom = validityFrom,
                    IdDestino = newValue.Id,
                    IdOrigen = newValue.Id
                };

                await _changeTracking.Create(change);
                await RegisterStatusChange(change);
                await UpdatePreviusChange(change, oldValue.Id, oldValue.Id);
            }
        }

        private async Task RegisterAristaChanges(int structureId, DateTimeOffset oldValidityFrom, DateTimeOffset validityFrom, int objectId, StructureArista oldValue, StructureArista newValue)
        {
            var user = new User
            {
                Id = _currentUser.UserId,
                Name = _currentUser.UserName
            };
            var pathNodes = await GetNodesPath(structureId, validityFrom, newValue.NodeIdTo);
            var change = new ChangeTracking
            {
                User = user,
                CreateDate = DateTimeOffset.UtcNow.ToOffset(new TimeSpan(-3, 0, 0)),
                IdStructure = structureId,
                IdObjectType = (int)ChangeTrackingObjectType.Arista,
                ChangedValueArista = new ChangeTrackingArista { 
                    AristaActual = new ItemAristaActual
                    {
                        AristaId = oldValue.Id,
                        OldValue = new ItemAristaCompare
                        {
                            StructureIdFrom = structureId,
                            NodeIdFrom = oldValue.NodeIdFrom,
                            AristaValidityTo = oldValidityFrom,
                            Description= await GetNodeName(oldValue.NodeIdFrom)
                        },
                        NewValue = new ItemAristaCompare
                        {
                            StructureIdFrom = structureId,
                            NodeIdFrom = newValue.NodeIdFrom,
                            AristaValidityTo = newValue.ValidityTo,
                            Description= await GetNodeName(newValue.NodeIdFrom)
                        }
                    },
                    AristaNueva = new ItemArista
                    {
                        AristaId = objectId,
                        StructureIdFrom = structureId,
                        NodeIdFrom = newValue.NodeIdFrom,
                        AristaValidityFrom = newValue.ValidityFrom,
                        AristaValidityTo = newValue.ValidityTo
                    }
                },
                IdChangeType = (int)ChangeType.Structure,
                NodePath = pathNodes,
                ValidityFrom = validityFrom,
                IdOrigen = newValue.NodeIdFrom,
                IdDestino = newValue.NodeIdTo
            };

            await _changeTracking.Create(change);
            await RegisterStatusChange(change);
            await UpdatePreviusChange(change, oldValue.NodeIdFrom, oldValue.NodeIdTo);
        }

        private async Task<string> GetNodeName(int nodeIdFrom)
        {
            var nodeName = "n/a";
            if (nodeIdFrom>0)
            {
                var nodeDefinition = await _repository.GetNodoDefinitionValidityByNodeIdAsync(nodeIdFrom);
                if (nodeDefinition == null)
                    nodeDefinition = await _repository.GetNodoDefinitionPendingAsync(nodeIdFrom);
                
                if (nodeDefinition != null)
                    nodeName = nodeDefinition.Name;
            }
            return nodeName;
        }

        private async Task RegisterStatusChange(ChangeTracking change)
        {
            var changeStatus = new ChangeTrackingStatus
            {
                CreatedDate = change.CreateDate,
                IdChangeTracking = change.Id,
                IdStatus = (int)ChangeTrackingStatusCode.Confirmed
            };
            await _changeTrackingStatus.Create(changeStatus);
        }

        /// <summary>
        /// En caso de estar modificando un cambio programado tengo que actualizar el estado
        /// </summary>
        /// <param name="change"></param>
        /// <param name="idOrigen"></param>
        /// <param name="idDestino"></param>
        /// <returns></returns>
        private async Task UpdatePreviusChange(ChangeTracking change, int idOrigen, int idDestino)
        {
            var previousChange = await _changeTracking.GetByOriginAndDestinationIdAndValidity(idOrigen, idDestino, change.ValidityFrom);
            if (previousChange != null && previousChange.Count >0)
            {
                previousChange = previousChange.Where(x => x.IdChangeType == change.IdChangeType && x.IdObjectType == change.IdObjectType).ToList();
                if (previousChange.Count > 0)
                {
                    int changeId = 0;
                    if (change.IdChangeType == (int)ChangeType.Structure && change.IdObjectType == (int)ChangeTrackingObjectType.Node)
                    {
                        var oldChange = previousChange.FirstOrDefault(x => x.ChangedValueNode.FieldName == change.ChangedValueNode.FieldName);
                        if (oldChange != null)
                        {
                            changeId = oldChange.Id;
                        }
                    }
                    else
                    {
                        var oldChange = previousChange.FirstOrDefault();
                        if (oldChange != null)
                        {
                            changeId = oldChange.Id;
                        }
                    }
                    if (changeId > 0)
                    {
                        var statusOldChange = await _changeTrackingStatus.GetByChangeId(changeId);
                        statusOldChange.IdStatus = (int)ChangeTrackingStatusCode.Cancelled;
                        _changeTrackingStatus.Update(statusOldChange);
                    }
                }
            }
        }

        /// <summary>
        /// Compares the nodes.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private List<ChangeTrackingNode> CompareNodes(StructureNodeDefinition oldValue, StructureNodeDefinition newValue)
        {
            var nodes = new List<ChangeTrackingNode>();

            PropertyInfo[] Props1 = oldValue.GetType().GetProperties();
            PropertyInfo[] Props2 = newValue.GetType().GetProperties();

            foreach (PropertyInfo pInfo1 in Props1)
            {

                PropertyInfo pInfo2 = Props2.FirstOrDefault(f => f.Name == pInfo1.Name);

                var value1 = pInfo1.GetValue(oldValue, null);
                var value2 = pInfo2.GetValue(newValue, null);

                CompareChanges(nodes, oldValue, newValue, pInfo1, value1, value2);
            }
            return nodes;
        }

        /// <summary>
        /// Compares the changes.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="pInfo1"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        private void CompareChanges(List<ChangeTrackingNode> nodes, StructureNodeDefinition node1, StructureNodeDefinition node2, PropertyInfo pInfo1, object value1, object value2)
        {
            var validProperties = new List<string>
            {
                "Name",
                "Active",
                "AttentionModeId",
                "RoleId",
                "SaleChannelId",
                "EmployeeId",
                "NodeParentId"
            };

            if (!Equals(value1, value2) && validProperties.Any(name => pInfo1.Name.Equals(name)))
            {
                var change = new ChangeTrackingNode
                {
                    OldValue = string.Empty,
                    NewValue = value2 != null ? value2.ToString().ToUpper() : string.Empty
                };

                if (node1 != null)
                {
                    change.OldValue = value1 != null ? value1.ToString().ToUpper() : string.Empty;
                }

                GetFieldName(pInfo1, change);
                change.FieldName = GetNameData(change.Field);
                nodes.Add(change);
            }
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <param name="pInfo1"></param>
        /// <param name="change"></param>
        private void GetFieldName(PropertyInfo pInfo1, ChangeTrackingNode change)
        {
            switch (pInfo1.Name)
            {
                case "AttentionModeId":
                    change.Field = "AttentionMode";
                    break;
                case "EmployeeId":
                    change.Field = "EmployeeId";
                    break;
                case "Name":
                    change.Field = "Name";
                    break;
                case "Active":
                    change.Field = "Active";
                    break;
                case "Code":
                    change.Field = "Code";
                    break;
                case "NodeParentId":
                    change.Field = "NodeParentId";
                    break;
                case "RoleId":
                    change.Field = "Role";
                    break;
                case "SaleChannelId":
                    change.Field = "SaleChannel";
                    break;
                default:
                    change.Field = $"{pInfo1.Name}Id";
                    break;
            }
        }

        /// <summary>
        /// Gets the name data.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string GetNameData(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                switch (field)
                {
                    case "AttentionMode":
                        return "Modo de atención";
                    case "Role":
                        return "Rol";
                    case "EmployeeId":
                        return "Persona";
                    case "SaleChannel":
                        return "Canal de venta";
                    case "Name":
                        return "Nombre";
                    case "Active":
                        return "Estado";
                    case "Code":
                        return "Código";
                    case "NodeParentId":
                        return "Depende de";
                    default:
                        break;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Gets the type of change.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private int GetChangeType(string propertyName)
        {
            switch (propertyName)
            {
                case "Name":
                case "Active":
                case "AttentionMode":
                case "SaleChannel":
                    return (int)ChangeType.Structure;

                case "Role":
                    return (int)ChangeType.Role;

                case "EmployeeId":
                    return (int)ChangeType.Employee;

                default:
                    return (int)ChangeType.Structure;
            }
        }

        private async Task<NodesPath> GetNodesPath(int structureId, DateTimeOffset validity, int nodeId)
        {
            var path = new NodesPath();
            path.Ids = new List<int>();
            var nodeRootId = await _mediator.Send(new GetStructureNodeRootQuery { StructureId = structureId });
            if (nodeRootId.RootNodeId == nodeId)
                path.Ids.Add(nodeId);
            else
            {
                var ids = await _mediator.Send(new GetNodeTreeByNodeIdQuery { StructureId = structureId, NodeRootId = nodeRootId.RootNodeId, ValidityFrom = validity, NodeId = nodeId });
                path.Ids.AddRange(ids);
            }
            return path;
        }
    }
}

