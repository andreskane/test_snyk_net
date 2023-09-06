using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Coravel.Events.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedPendingVersionIsLowerPortalCommand : IRequest<bool>
    {
        public Versioned VersionPortal { get; set; }
        public Versioned VersionPortalPending { get; set; }
        public PendingVersionTruckDTO PendingTruck { get; set; }
        public string CompanyTruck { get; set; }
        public TruckWritingPayload PlayLoad { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{ABI.API.Structure.ACL.Truck.Application.Commands.VersionedPendingVersionIsLowerPortalCommand, System.Boolean}" />
    public class VersionedPendingVersionIsLowerPortalCommandHandler : IRequestHandler<VersionedPendingVersionIsLowerPortalCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IDispatcher _queueDispatcher;

        public VersionedPendingVersionIsLowerPortalCommandHandler(IMediator mediator, IDispatcher queueDispatcher)
        {
            _mediator = mediator;
            _queueDispatcher = queueDispatcher;
        }

        public async Task<bool> Handle(VersionedPendingVersionIsLowerPortalCommand request, CancellationToken cancellationToken)
        {
            // La fecha de la versión pendiente  es menor a la nueva ?

            if ( request.VersionPortalPending.Validity < request.VersionPortal.Validity)
            {
                //Envio a aprocesar la pendiente y la nueva la dejo como pendiente
                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionPortalPending.Id, State = VersionedState.Procesando });
                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionPortal.Id, State = VersionedState.PendienteDeEnvio });

                //var message = $"La versión con vingencia {request.VersionPortal:dd/MM/yyyy} queda pendiente en Portal, por que hay una version previa para enviar con vigencia {request.VersionPortalPending.Date:dd/MM/yyyy}";
                //await _queueDispatcher.Broadcast(request.PlayLoad.ToWarningEvent(message));


                await _mediator.Send(new VersionedProcessImpactCommand { VersionedPendingPortal = request.VersionPortalPending, Payload = request.PlayLoad });

            }
            else
            {
                ///envio la nueva Version Portal
                await _mediator.Send(new VersionedNewVersionMinorTruckVersionCommand { VersionPortal = request.VersionPortal, PendingTruck = request.PendingTruck , CompanyTruck = request.CompanyTruck, PlayLoad = request.PlayLoad });
            }


            return await Task.Run(() => true);
        }
    }
}
