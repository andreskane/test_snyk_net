using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Repository.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class DeleteNodeDraftCommandHandler : IRequestHandler<DeleteNodeDraftCommand, int>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IMediator _mediator;

        public DeleteNodeDraftCommandHandler(IStructureNodeRepository repositoryStructureNode, IMediator mediator)
        {
            _repositoryStructureNode = repositoryStructureNode ?? throw new ArgumentNullException(nameof(repositoryStructureNode));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<int> Handle(DeleteNodeDraftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nodoDefinition = await _repositoryStructureNode.GetNodoDefinitionByIdAsync(request.NodeDefinitionId);
                if (nodoDefinition != null)
                {
                    var nodesDefinitions = await _repositoryStructureNode.GetAllNodoDefinitionByNodeIdAsync(nodoDefinition.NodeId);

                    //Si tengo más de un nodo definición significa que es un borrador de un nodo ya creado
                    if (nodesDefinitions.Count > 1)
                        await UpdateCancelledNodeDefinitionAsync(request.StructureId, nodoDefinition.NodeId, nodoDefinition.ValidityFrom.Date);

                    var aristaDraft = await _repositoryStructureNode.GetAristaPendient(request.StructureId, nodoDefinition.NodeId, request.ValidityFrom);

                    if (aristaDraft != null)
                    {
                        _repositoryStructureNode.DeleteArista(aristaDraft);
                        await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    }

                    if (nodoDefinition.MotiveStateId == (int)MotiveStateNode.Draft)
                    {
                        _repositoryStructureNode.DeleteNodoDefinitions(nodoDefinition);
                        await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    }

                    //Nodo nuevo
                    if (nodesDefinitions.Count == 1 && nodesDefinitions[0].MotiveStateId == (int) MotiveStateNode.Draft)
                    {
                        var nodo = await _repositoryStructureNode.GetAsync(nodoDefinition.NodeId);
                        _repositoryStructureNode.Delete(nodo);
                        await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    }
                }

                return await Task.Run(() => request.NodeDefinitionId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        private async Task UpdateCancelledNodeDefinitionAsync(int structureId, int nodeId, DateTime date)
        {
            //vuelve a la version anterior que esta cancelado (si es que hay)
            var nodeDefCanceled = await _mediator.Send(new GetOneNodeDefinitionCanceledQuery { StructureId = structureId, NodeId = nodeId, ValidityFrom = date });

            if (nodeDefCanceled != null)
            {
                var nodeCanceled = await _repositoryStructureNode.GetNodoDefinitionByIdAsync(nodeDefCanceled.NodeDefinitionId);

                nodeCanceled.EditMotiveStateId((int)MotiveStateNode.Confirmed);

                _repositoryStructureNode.UpdateNodoDefinition(nodeCanceled);
            }
        }
    }
}
