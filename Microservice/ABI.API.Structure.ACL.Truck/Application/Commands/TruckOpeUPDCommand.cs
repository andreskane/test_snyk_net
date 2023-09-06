using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckOpeUPDCommand : IRequest<bool>
    {
        public int VersionedId { get; set; }
        public OpecpiniOut OpeiniOut { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }

    }

    public class TruckOpeUPDCommandHandler : IRequestHandler<TruckOpeUPDCommand, bool>
    {
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;
        private readonly IMediator _mediator;


        public TruckOpeUPDCommandHandler( 
            ITruckService truckService,
            IApiTruck apiTruck,
            IMediator mediator)
        {
            _truckService = truckService;
            _apiTruck = apiTruck;
            _mediator = mediator;
        }


        public async Task<bool> Handle(TruckOpeUPDCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var opeiniUDP = await _truckService.SetActionUPD(request.ValidityFrom, request.OpeiniOut.Epeciniin.Empresa, request.OpeiniOut.Epeciniin.NroVer);
                var opeUDP = await _apiTruck.SetOpecpini(opeiniUDP);

                

                return true;
            }
            catch (Exception ex)
            {
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.EACP, ex.Message);
                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.PendienteDeEnvio });

                return false;
            }
        }

    }
}
