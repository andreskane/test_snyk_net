using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using Coravel.Events.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedNewVersionMinorTruckVersionCommand : IRequest<int>
    {
        public Versioned VersionPortal { get; set; }
        public PendingVersionTruckDTO PendingTruck { get; set; }
        public string CompanyTruck { get; set; }
        public TruckWritingPayload PlayLoad { get; set; }
    }

    public class VersionedNewVersionMinorTruckVersionCommandHandler : IRequestHandler<VersionedNewVersionMinorTruckVersionCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IVersionedRepository _versionedRepository;
        private readonly IDispatcher _queueDispatcher;
        private readonly ITruckService _truckService;


        public VersionedNewVersionMinorTruckVersionCommandHandler(IMediator mediator, 
                                                                    IVersionedRepository versionedRepository,
                                                                    IDispatcher queueDispatcher
                                                                    , ITruckService truckService)
        {
            _mediator = mediator;
            _versionedRepository = versionedRepository;
            _queueDispatcher = queueDispatcher;
            _truckService = truckService;
        }


        public async Task<int> Handle(VersionedNewVersionMinorTruckVersionCommand request, CancellationToken cancellationToken)
        {
            //La nueva versión es de fecha menor que  la versión de Truck
            await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.EPR, request.PendingTruck);
            await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.EPR, request.VersionPortal);


            if (request.PendingTruck.LastVersionDate.HasValue &&request.VersionPortal.Validity < request.PendingTruck.LastVersionDate.Value)
            {
                await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.IGP, "Cancelo la versión anterior y la dejo pendiente");

                // Cancelo la version de truck y la dejo pendiente.
                // Y luego envio la nueva version que tiene un fecha de vigencia menor a la actual que truck.
                var date = request.PendingTruck.LastVersionDate.Value;   
                var versionTruck = request.PendingTruck.VersionTruck.PadLeft(6, '0');

                await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.IGP, versionTruck);

                var versionInTruck = await _versionedRepository.GetByNroVerValidity(versionTruck, date);

                await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.IGP, $"Versiones a cancelar {versionInTruck.Count}");
                await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.IGP, versionInTruck);

                var opeIni = new OpecpiniOut();

                opeIni.Epeciniin.Empresa = request.CompanyTruck;

                foreach (var item in versionInTruck)
                {
                    opeIni.Epeciniin.NroVer = item.Version;

                    var opeUPD =  await _mediator.Send(new TruckOpeUPDCommand { VersionedId = item.Id, OpeiniOut = opeIni, ValidityFrom = item.Validity });

                    if (opeUPD)
                    {
                        var opeRCH =  await _mediator.Send(new TruckOpeRCHCommand { VersionedId = item.Id, OpeiniOut = opeIni, ValidityFrom = item.Validity });
                        await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = item.Id, State = VersionedState.PendienteDeEnvio });

                        //var message = $"La versión enviada a truck con vigencia con fecha {request.PendingTruck.LastVersionDate.Value:dd/MM/yyyy} vuelve a estado pendiente en Portal, por que la nueva con vigencia enviada {request.VersionPortal.Date:dd/MM/yyyy} es menor";
                        //await _queueDispatcher.Broadcast(request.PlayLoad.ToWarningEvent(message));
                    }
                }
            }

            //envio a truck
            await _truckService.SetVersionedLog(request.VersionPortal.Id, VersionedLogState.EPR, $"Envio version Id: {request.VersionPortal.Id}");
            await _mediator.Send(new TruckSendVersionCommand { Versioned = request.VersionPortal, PlayLoad = request.PlayLoad});
            

            return await Task.Run(() => 0);
        }
    }
}
