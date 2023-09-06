using ABI.API.Structure.ACL.Truck.Application.Extensions.TruckStep;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using Coravel.Events.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckOpeAPRCommand : IRequest<bool>
    {
        public int VersionedId { get; set; }
        public OpecpiniOut OpeiniOut { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public TruckWritingPayload PlayLoad { get; set; }

    }

    public class TruckOpeAPRCommandHandler : IRequestHandler<TruckOpeAPRCommand, bool>
    {
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;
        private readonly IMediator _mediator;
        private readonly IDispatcher _queueDispatcher;
        public TruckWritingPayload PlayLoad { get; set; }


        public TruckOpeAPRCommandHandler(
            ITruckService truckService,
            IApiTruck apiTruck,
            IMediator mediator,
            IDispatcher queueDispatcher)
        {
            _truckService = truckService;
            _apiTruck = apiTruck;
            _mediator = mediator;
            _queueDispatcher = queueDispatcher;
        }


        public async Task<bool> Handle(TruckOpeAPRCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var opeiniAPR = await _truckService.SetActionAPR(request.ValidityFrom, request.OpeiniOut.Epeciniin.Empresa, request.OpeiniOut.Epeciniin.NroVer);
                var opeAPR = await _apiTruck.SetOpecpini(opeiniAPR);
                var errors = opeAPR.Msglog.Level1.Where(e => e.ECLogSts == "ERR").ToList();

                if (errors.Count > 0)
                {
                    await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.EBT, opeAPR);

                    //Rechazo del lado de truck la version por que tiene errores
                    await _mediator.Send(new TruckOpeUPDCommand { VersionedId = request.VersionedId, ValidityFrom = request.ValidityFrom, OpeiniOut = request.OpeiniOut });
                    await _mediator.Send(new TruckOpeRCHCommand { VersionedId = request.VersionedId, ValidityFrom = request.ValidityFrom, OpeiniOut = request.OpeiniOut });

                    await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.Rechazado });


                    await _queueDispatcher.Broadcast(request.PlayLoad.ToDoneEvent());

                    return false;
                }
                else
                    await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.ACP, opeAPR);

                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.Aceptado });

                await _queueDispatcher.Broadcast(request.PlayLoad.ToDoneEvent());


                return true;
            }
            catch (Exception ex)
            {
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.EACP, ex.Message);

                return false;
            }
        }
    }
}
