using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;

using MediatR;

namespace ABI.API.Structure.Application.Commands.RequestsTray
{
    public class DeleteChangeCommand : IRequest, IDeleteChange
    {
        public int Id { get; set; }
        public bool DeleteConfirm { get; set; }
    }

    public class DeleteChangeCommandHandler : IRequestHandler<DeleteChangeCommand>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IChangeTrackingRepository _repo;
        private readonly IChangeTrackingStatusRepository _repoStatus;

        public DeleteChangeCommandHandler(
            IMediator mediator,
            ICurrentUserService currentUserService,
            IChangeTrackingRepository repo,
            IChangeTrackingStatusRepository repoStatus)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _repo = repo;
            _repoStatus = repoStatus;
        }

        public async Task<Unit> Handle(DeleteChangeCommand request, CancellationToken cancellationToken)
        {
            var change = await _repo.GetById(request.Id);

            if (change != null)
            {
                if (change.IdObjectType == (int)ChangeTrackingObjectType.Node && IsDependencyChange(change))
                    await RevertDependencyChange(request, change, cancellationToken);
                else
                {
                    if (change.IdObjectType == (int)ChangeTrackingObjectType.Arista)
                    {
                        await _mediator.Send(new RevertChangeCommand
                        {
                            ObjectType = ChangeTrackingObjectType.Arista,
                            NodeId = change.IdDestino,
                            Value = change.ChangedValueArista.AristaActual.OldValue.NodeIdFrom.ToString(),
                            ValidityFrom = change.ValidityFrom
                        }, cancellationToken);
                    }
                    else
                        await RevertChange(change, cancellationToken);
                }

                await _mediator.Send(new VersionedCancelChangeCommand
                {
                    StructureId = change.IdStructure,
                    ValidityFrom = change.ValidityFrom,
                    Username = _currentUserService.UserName
                }, cancellationToken);
            }
            await DeleteChange(change);
            return Unit.Value;
        }

        private async Task DeleteChange(Domain.Entities.ChangeTracking change)
        {
            var changeStatus = await _repoStatus.GetByChangeId(change.Id);

            if (changeStatus != null)
                await _repoStatus.Delete(changeStatus);

            await _repo.Delete(change);
        }

        private async Task RevertDependencyChange(DeleteChangeCommand request, Domain.Entities.ChangeTracking change, CancellationToken cancellationToken)
        {
            //Recupero los cambios asociados al NodoDefinicion
            var changesRelations = await _repo.GetByOriginAndDestinationIdAndValidity(change.IdOrigen, change.IdDestino, change.ValidityFrom, true, cancellationToken);

            //Filtro solamente los que son de tipo: Rol - Modo Atención - Persona
            changesRelations = changesRelations.Where(x => x.IdChangeType == 5 || x.IdChangeType == 6 || (x.IdChangeType == 4 && x.ChangedValueNode.Field == "AttentionMode")).ToList();

            //Si no confirmó el cambio y tengo cambios dependientes con el filtro anterior, arrojo la excepción. Si solo tengo uno de los 3 dependientes, eliminar.
            if (!request.DeleteConfirm && changesRelations.Count > 1)
                throw new ConfirmException("Los campos Persona - Rol - Modo de atención están relacionados. Si elimina un cambio en alguno de estos también se eliminaran si hay relacionados.");
            else
            {
                foreach (var item in changesRelations)
                {
                    await RevertChange(item, cancellationToken);
                    if (request.Id != item.Id)
                        await DeleteChange(item);
                }
            }
        }

        private async Task RevertChange(Domain.Entities.ChangeTracking item, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RevertChangeCommand
            {
                ObjectType = ChangeTrackingObjectType.Node,
                NodeId = item.ChangedValueNode.Node.Id.Value,
                Field = item.ChangedValueNode.Field,
                Value = item.ChangedValueNode.OldValue,
                ValidityFrom = item.ValidityFrom
            }, cancellationToken);
        }

        private static bool IsDependencyChange(Domain.Entities.ChangeTracking change)
        {
            if (change.IdChangeType == 5 || change.IdChangeType == 6)
                return true;
            if (change.IdChangeType == 4 && change.ChangedValueNode.Field == "AttentionMode")
                return true;
            return false;
        }
    }
}
