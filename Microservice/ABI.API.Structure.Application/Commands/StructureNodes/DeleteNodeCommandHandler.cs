using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Repository.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, int>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IStructureRepository _repositoryStructure;

        public DeleteNodeCommandHandler(IStructureNodeRepository repositoryStructureNode, IStructureRepository repositoryStructure)
        {
            _repositoryStructureNode = repositoryStructureNode ?? throw new ArgumentNullException(nameof(repositoryStructureNode));
            _repositoryStructure = repositoryStructure ?? throw new ArgumentNullException(nameof(repositoryStructure));
        }

        public async Task<int> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _repositoryStructureNode.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Guid transactionId;

                    if (request.NodeIdParent.HasValue)
                    {
                        //Elimina relacion
                        var arista = await _repositoryStructureNode.GetArista(request.StructureId, request.NodeIdParent.Value, request.Id, request.ValidityFrom, request.ValidityTo);
                        _repositoryStructureNode.DeleteArista(arista);
                        await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    }

                    //Se elimina el nodo si solo se encuentra en una estructura
                    var exists = await _repositoryStructureNode.ExistsInStructures(request.Id);

                    if (!exists)
                    {
                        if (!request.NodeIdParent.HasValue)
                        {
                            //Update Estructura
                            await UpdateStructuraAsync(request.StructureId, cancellationToken);
                        }

                        //Elimina Definicion
                        await DeleteDefinition(request, cancellationToken);

                        //Elimina Nodo
                        await DeleteNode(request, cancellationToken);

                        await _repositoryStructureNode.UnitOfWork.CommitTransactionAsync(transaction);
                    }

                    transactionId = transaction.TransactionId;

                    return await Task.Run(() => request.Id);
                }
                catch (Exception ex)
                {
                    _repositoryStructureNode.UnitOfWork.RollbackTransaction();

                    throw new RepositoryException(ex.Message, ex);
                }
            }
        }

        #region Methods

        /// <summary>
        /// Updates the structura asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task UpdateStructuraAsync(int structureId, CancellationToken cancellationToken)
        {
            var structure = await _repositoryStructure.GetAsync(structureId);

            structure.SetRootNodeIdNull();

            _repositoryStructure.Update(structure);

            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }

        /// <summary>
        /// Deletes the definition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task DeleteDefinition(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var nodoDefinition = await _repositoryStructureNode.GetNodoDefinitionAsync(request.Id, request.ValidityFrom, request.ValidityTo);
            _repositoryStructureNode.DeleteNodoDefinitions(nodoDefinition);
            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }

        /// <summary>
        /// Deletes the node.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task DeleteNode(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var nodo = await _repositoryStructureNode.GetAsync(request.Id);
            _repositoryStructureNode.Delete(nodo);
            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        #endregion
    }
}
