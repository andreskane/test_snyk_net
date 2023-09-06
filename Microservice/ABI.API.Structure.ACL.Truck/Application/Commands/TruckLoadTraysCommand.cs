using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckLoadTraysCommand : IRequest<bool>
    {
        public int VersionedId { get; set; }

        public StructureTruck StructureTruck { get; set; }
        public OpecpiniOut OpecpiniOut { get; set; }
    }

    public class TruckLoadTraysCommandHandler: IRequestHandler<TruckLoadTraysCommand, bool>
    {
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;
        private readonly IMediator _mediator;

        public TruckLoadTraysCommandHandler( ITruckService truckService,
                                             IApiTruck apiTruck,
                                             IMediator mediator)
        {
            _truckService = truckService;
            _apiTruck = apiTruck;
            _mediator = mediator;
        }


        public async Task<bool> Handle(TruckLoadTraysCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.EB, request.StructureTruck);

                await _apiTruck.Ptecdire(request.StructureTruck.Ptecdire);
                await _apiTruck.Ptecarea(request.StructureTruck.Ptecarea);
                await _apiTruck.Ptecgere(request.StructureTruck.Ptecgere);
                await _apiTruck.Ptecregi(request.StructureTruck.Ptecregi);
                await _apiTruck.Pteczoco(request.StructureTruck.Pteczoco);
                await _apiTruck.Pteczona(request.StructureTruck.Pteczona);
                await _apiTruck.Ptecterr(request.StructureTruck.Ptecterr);

                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.DEB, "Datos enviados a las Bandejas");
            }
            catch (Exception ex)
            {
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.EEB, ex.Message);
                await _mediator.Send(new TruckOpeRCHCommand { VersionedId = request.VersionedId, ValidityFrom = DateTimeOffset.UtcNow.Date.ToDateOffset(), OpeiniOut = request.OpecpiniOut });
                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.PendienteDeEnvio });

                return await Task.Run(() => false);
            }

            return await Task.Run(() => true);
        }

    }
}
