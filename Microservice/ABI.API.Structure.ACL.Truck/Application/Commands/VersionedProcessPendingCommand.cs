using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedProcessPendingCommand : IRequest<bool>
    {

    }

    public class VersionedProcessPendingCommandHandler : IRequestHandler<VersionedProcessPendingCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITruckService _truckService;


        public VersionedProcessPendingCommandHandler(IMediator mediator, ITruckService truckService)
        {
            _mediator = mediator;
            _truckService = truckService;
        }

        public async Task<bool> Handle(VersionedProcessPendingCommand request, CancellationToken cancellationToken)
        {
 
            var versionPortalPending = await _mediator.Send(new GetOneFirstVersionPending());

   
            if (versionPortalPending != null)
            {
                await _truckService.SetVersionedLog(versionPortalPending.Id, VersionedLogState.IGP, "Inicio proceso automático VersionedProcessPendingCommand");

                var listVersion = await _mediator.Send(new GetAllVersionPendingValidityQuery { ValidityFrom = versionPortalPending.Validity });

                var payload = new TruckWritingPayload
                {
                    StructureId = versionPortalPending.StructureId,
                    Date = versionPortalPending.Date,
                    Username = versionPortalPending.User,
                    VersionsToUnify = listVersion
                };

                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = versionPortalPending.Id, State = VersionedState.Procesando, Message = "Procesando Versión Pendiente en Portal" });
                await _mediator.Send(new VersionedProcessImpactCommand { Payload = payload });
            }

            return await Task.Run(() => true);
        }
    }
}
