using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedCancelAllChangesCommand : IRequest<bool>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class VersionedCancelAllChangesCommandHandler : IRequestHandler<VersionedCancelAllChangesCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IVersionedRepository _versionedRepository;
        private readonly ITruckService _truckService;


        public VersionedCancelAllChangesCommandHandler(
            IMediator mediator, 
            IVersionedRepository versionedRepository, 
            ITruckService truckService)
        {
            _mediator = mediator;
            _versionedRepository = versionedRepository;
            _truckService = truckService;
        }


        public async Task<bool> Handle(VersionedCancelAllChangesCommand request, CancellationToken cancellationToken)
        {
            var version = _versionedRepository
                .Filter(w =>
                    w.StructureId == request.StructureId &&
                    w.Validity == request.ValidityFrom &&
                    (
                        w.StatusId == (int)VersionedState.PendienteDeEnvio ||
                        w.StatusId == (int)VersionedState.Aceptado
                    )
                )
                .FirstOrDefault();

            if (version == null)
                return false;

            try
            {
                if (version.StatusId == (int)VersionedState.PendienteDeEnvio)
                {
                    await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = version.Id, State = VersionedState.Cancelado }, cancellationToken);
                    return true;
                }

                var companyTruck = await _mediator.Send(new GetOneCompanyTruckQuery { StructureId = request.StructureId }, cancellationToken);
                var opeIni = new OpecpiniOut
                {
                    Epeciniin = new Epecini
                    {
                        NroVer = version.Version,
                        Empresa = companyTruck
                    }
                };

                await _mediator.Send(new TruckOpeUPDCommand { VersionedId = version.Id, OpeiniOut = opeIni, ValidityFrom = request.ValidityFrom.Date }, cancellationToken);
                await _mediator.Send(new TruckOpeRCHCommand { VersionedId = version.Id, OpeiniOut = opeIni, ValidityFrom = request.ValidityFrom.Date, KeepVersionPending = false }, cancellationToken);

                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = version.Id, State = VersionedState.Cancelado }, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                await _truckService.SetVersionedLog(version.Id, VersionedLogState.EACP, ex.Message);

                return false;
            }
        }
    }
}
