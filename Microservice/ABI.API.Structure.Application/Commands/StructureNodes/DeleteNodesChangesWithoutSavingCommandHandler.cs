using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Repository.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class DeleteNodesChangesWithoutSavingCommandHandler : IRequestHandler<DeleteNodesChangesWithoutSavingCommand, bool>
    {
        private readonly IStructureNodeRepository _repository;
        private readonly IMediator _mediator;

        public DeleteNodesChangesWithoutSavingCommandHandler(IStructureNodeRepository repository, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(DeleteNodesChangesWithoutSavingCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _repository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var nodes = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

                    //ordeno para que se eliminen los nodos de los niveles inferior primero desde territorio
                    foreach (var item in nodes.OrderByDescending(n => n.NodeLevelId).ToList())
                    {
                        if (item.NodeMotiveStateId == (int)MotiveStateNode.Draft)
                        {
                            var nodeDesf = await _repository.GetNodoDefinitionByIdAsync(item.NodeDefinitionId);

                            //vuelve a la version anterior que esta cancelado
                            var nodeDefCanceled = await _mediator.Send(new GetOneNodeDefinitionCanceledQuery { StructureId = request.StructureId, NodeId = nodeDesf.NodeId, ValidityFrom = nodeDesf.ValidityFrom });

                            if (nodeDefCanceled != null)
                            {
                                var nodeCanceled = await _repository.GetNodoDefinitionByIdAsync(nodeDefCanceled.NodeDefinitionId);

                                nodeCanceled.EditMotiveStateId((int)MotiveStateNode.Confirmed);

                                _repository.UpdateNodoDefinition(nodeCanceled);
                            }

                            _repository.DeleteNodoDefinitions(nodeDesf);
                        }

                        if (item.AristaMotiveStateId == (int)MotiveStateNode.Draft)
                        {
                            var arista = await _repository.GetAristaByNodeTo(request.StructureId, item.NodeId, request.ValidityFrom);

                            if (arista != null && arista.MotiveStateId == (int)MotiveStateNode.Draft)
                                _repository.DeleteArista(arista);
                        }

                        // elimina nodo si no tiene arista y no tiene nodo_definicion
                        if(item.AristaMotiveStateId == (int)MotiveStateNode.Draft && item.NodeMotiveStateId == (int)MotiveStateNode.Draft)
                        {
                           var nodeDefinition = await _repository.GetAllNodoDefinitionByNodeIdAsync(item.NodeId);

                            if(nodeDefinition.Count == 1 && nodeDefinition[0].MotiveStateId == (int)MotiveStateNode.Draft)
                            {
                                var node = await _repository.GetAsync(item.NodeId);
                                _repository.Delete(node);
                            }
                        }
                    }

                    await _repository.UnitOfWork.CommitTransactionAsync(transaction);
                    return await Task.Run(() => true);
                }
                catch (Exception ex)
                {
                    _repository.UnitOfWork.RollbackTransaction();
                    throw new RepositoryException(ex.Message, ex);
                }
            }
        }
    }
}
