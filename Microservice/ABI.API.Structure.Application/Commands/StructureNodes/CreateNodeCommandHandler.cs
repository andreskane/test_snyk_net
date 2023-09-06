using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Repository.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand, int>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;

        private readonly IStructureRepository _repositoryStructure;

        public CreateNodeCommandHandler(IStructureNodeRepository repository, IStructureRepository repositoryStructure)
        {
            _repositoryStructureNode = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryStructure = repositoryStructure ?? throw new ArgumentNullException(nameof(repositoryStructure));
        }

        public async Task<int> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _repositoryStructureNode.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var entityId = 0;
                    var node = new StructureNode(request.Code, request.LevelId);
                    var resulNode = _repositoryStructureNode.Add(node);
                    var resultSave = await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    if (resultSave)
                    {
                        await AddDefinition(request, resulNode, cancellationToken);

                        await IsRootNode(request, resulNode, cancellationToken);

                        if (request.IsRootNode.HasValue && !request.IsRootNode.Value)
                        {
                            await CreateNodeRelationship(request, node, cancellationToken);
                        }
                    }

                    await _repositoryStructureNode.UnitOfWork.CommitTransactionAsync(transaction);

                    entityId = resulNode.Id;

                    return await Task.Run(() => entityId);
                }
                catch (Exception ex)
                {
                    _repositoryStructureNode.UnitOfWork.RollbackTransaction();

                    throw new RepositoryException(ex.Message, ex);
                }
            }
        }

        #region Methods private


        private async Task AddDefinition(CreateNodeCommand request, StructureNode resulNode, CancellationToken cancellationToken)
        {
            var normalizedNodeDefinition = new StructureNodeDefinition(
                resulNode.Id,
                request.AttentionModeId,
                request.RoleId,
                request.SaleChannelId,
                request.EmployeeId,
                request.ValidityFrom,
                request.Name,
                request.Active
            )
            .NormalizeString();

            normalizedNodeDefinition.EditMotiveStateId((int)MotiveStateNode.Draft);

            SetVacantePerson(normalizedNodeDefinition);

            _repositoryStructureNode.AddNodoDefinition(normalizedNodeDefinition);

            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task IsRootNode(CreateNodeCommand request, StructureNode node, CancellationToken cancellationToken)
        {
            if (request.IsRootNode.HasValue && request.IsRootNode.Value)
            {
                var structure = await _repositoryStructure.GetAsync(request.StructureId);

                structure.SetRootNodeId(node.Id);

                _repositoryStructure.Update(structure);

                await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }

        private async Task CreateNodeRelationship(CreateNodeCommand request, StructureNode node, CancellationToken cancellationToken)
        {
            var arista = new StructureArista(request.StructureId, request.NodeIdParent.Value, request.StructureId, node.Id, RelationshipNodeType.Contains.Id, request.ValidityFrom);

            arista.EditMotiveStateId((int)MotiveStateNode.Draft);
            arista.EditValidityFrom(request.ValidityFrom);

            _repositoryStructureNode.AddArista(arista);

            await _repositoryStructureNode.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }


        /// <summary>
        /// Sets the vacante person.
        /// </summary>
        /// <param name="nodeDefinitionNew">The node definition new.</param>
        private static void SetVacantePerson(StructureNodeDefinition nodeDefinitionNew)
        {

            if (nodeDefinitionNew.RoleId.HasValue && !nodeDefinitionNew.EmployeeId.HasValue )
            {
                nodeDefinitionNew.EditEmployeeId(null);
                nodeDefinitionNew.EditVacantPerson(true);
            }

            if (!nodeDefinitionNew.RoleId.HasValue && nodeDefinitionNew.EmployeeId.HasValue)
            {
                nodeDefinitionNew.EditEmployeeId(null);
                nodeDefinitionNew.EditVacantPerson(true);
            }

            if (!nodeDefinitionNew.RoleId.HasValue && !nodeDefinitionNew.EmployeeId.HasValue )
            {
                nodeDefinitionNew.EditEmployeeId(null);
                nodeDefinitionNew.EditVacantPerson(true);
            }

            if (nodeDefinitionNew.RoleId.HasValue && nodeDefinitionNew.EmployeeId.HasValue )
            {
                nodeDefinitionNew.EditVacantPerson(false);
            }
            
        }


        #endregion
    }
}
