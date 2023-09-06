using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Extensions.TruckStep;
using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using Coravel.Events.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedPendingNotificationCommand : IRequest<bool>
    {
        public TruckWritingPayload PlayLoad { get; set; }
        public int VersionedId { get; set; }
        public PendingVersionTruckDTO PendingTruck { get; set; }
    }

    public class VersionedPendingNotificationCommandHandler : IRequestHandler<VersionedPendingNotificationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IDispatcher _queueDispatcher;


        public VersionedPendingNotificationCommandHandler(IMediator mediator, IDispatcher queueDispatcher)
        {
            _mediator = mediator;
            _queueDispatcher = queueDispatcher;
        }


        public async Task<bool> Handle(VersionedPendingNotificationCommand request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetByIdQuery { StructureId = request.PlayLoad.StructureId }, cancellationToken);


            await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId,
                                                                            State = VersionedState.Procesando,
                                                                            Message = "Hay una versión pendiente en Truck"});

            var pendingPortal = new PendingVersionPortalDTO
            {
                StructureId = request.PlayLoad.StructureId,
                StructureName = structure != null ? structure.Name : string.Empty
            };

            var date = request.PendingTruck.LastVersionDate.HasValue ? request.PendingTruck.LastVersionDate.Value.ToString("dd/MM/yyyy") : "-";

            // await _queueDispatcher.Broadcast(request.PlayLoad.ToWarningEvent(message));

            await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.Procesando, Message = request.PendingTruck.Message });
            await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.PendienteDeEnvio });
            await _queueDispatcher.Broadcast(request.PlayLoad.ToDoneEvent());

            return await Task.Run(() => false);
        }
    }
}
