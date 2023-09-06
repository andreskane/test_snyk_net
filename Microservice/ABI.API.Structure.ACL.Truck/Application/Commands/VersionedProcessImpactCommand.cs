using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedProcessImpactCommand : IRequest<bool>
    {
        public TruckWritingPayload Payload { get; set; }
        public Versioned VersionedPendingPortal { get; set; }
    }

    public class VersionedProcessImpactCommandHandler : IRequestHandler<VersionedProcessImpactCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITruckService _truckService;

        public VersionedProcessImpactCommandHandler(IMediator mediator, ITruckService truckService)
        {
            _mediator = mediator;
            _truckService = truckService;
        }

        public async Task<bool> Handle(VersionedProcessImpactCommand request, CancellationToken cancellationToken)
        {

            Versioned versionedProcess = null;

            if (request.Payload.VersionsToUnify.Count > 0)
            {
                //desabilita versiones anteriores y deja solo la ultima para ejecutar
                //Es para el proceso Automatico de pendientes
                versionedProcess = await _mediator.Send(new VersionsToUnifyCommand { Payload = request.Payload });

                var structure = await _mediator.Send(new GetByIdQuery { StructureId = request.Payload.StructureId });

                var PlayLoadPending = new TruckWritingPayload
                {
                    Date = versionedProcess.Validity,
                    StructureId = versionedProcess.StructureId,
                    Username = versionedProcess.User,
                    StructureName = structure.Name
                };

                request.Payload = PlayLoadPending;
            }
            else
            {
                versionedProcess = await _mediator.Send(new VersionedGenerateVersionCommand { StructureId = request.Payload.StructureId, ValidityFrom = request.Payload.Date, User = request.Payload.Username });
            }

            var pendingVersionTruck = await _mediator.Send(new StructureVersionTruckStatusCommand { StructureId = request.Payload.StructureId });

            var companyTruck = await _mediator.Send(new GetOneCompanyTruckQuery { StructureId = request.Payload.StructureId });

            await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = versionedProcess.Id, State = VersionedState.Procesando });

            IsPreviousOrSameVersion(versionedProcess, pendingVersionTruck);

            await _truckService.SetVersionedLog(versionedProcess.Id, VersionedLogState.EPR, pendingVersionTruck);

            if (pendingVersionTruck.StructureEdit && pendingVersionTruck.IsTrasp
                || pendingVersionTruck.StructureEdit && pendingVersionTruck.IsPrevious && !pendingVersionTruck.IsSameVersion
                || pendingVersionTruck.StructureEdit && pendingVersionTruck.IsSameVersion && !pendingVersionTruck.IsPrevious)
            {// continua con el porceso
                var message = await _mediator.Send(new VersionedSameDateCommand { VersionedId = versionedProcess.Id, PendingTruck = pendingVersionTruck, CompanyTruck = companyTruck, PlayLoad = request.Payload });
            }
            else
            {
                //queda como pendiente la versionado Notifica    
                var message = await _mediator.Send(new VersionedPendingNotificationCommand { VersionedId = versionedProcess.Id, PlayLoad = request.Payload, PendingTruck = pendingVersionTruck });
            }

            return await Task.Run(() => true);
        }

        /// <summary>
        /// Determines whether [is previous or same version] [the specified versioned process].
        /// </summary>
        /// <param name="versionedProcess">The versioned process.</param>
        /// <param name="pendingVersionTruck">The pending version truck.</param>
        private static void IsPreviousOrSameVersion(Versioned versionedProcess, DTO.PendingVersionTruckDTO pendingVersionTruck)
        {
            // versiones con menor fecha que la actual
            if (pendingVersionTruck.LastVersionDate.HasValue && versionedProcess.Validity < pendingVersionTruck.LastVersionDate.Value)
            {
                pendingVersionTruck.IsPrevious = true;
            }

            // versiones con misma fecha
            if (pendingVersionTruck.LastVersionDate.HasValue && versionedProcess.Validity == pendingVersionTruck.LastVersionDate.Value)
            {
                pendingVersionTruck.IsSameVersion = true;
            }
        }
    }
}
