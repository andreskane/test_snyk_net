using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckOpeNewVersionCommand : IRequest<OpecpiniOut>
    {
        public int VersionedId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class TruckOpeNewVersionCommandHandler : IRequestHandler<TruckOpeNewVersionCommand, OpecpiniOut>
    {
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;


        public TruckOpeNewVersionCommandHandler(ITruckService truckService, IApiTruck apiTruck)
        {
            _truckService = truckService;
            _apiTruck = apiTruck;
        }


        public async Task<OpecpiniOut> Handle(TruckOpeNewVersionCommand request, CancellationToken cancellationToken)
        {
            OpecpiniInput opeini = null;

            try
            {
                opeini = await _truckService.SetActionNewVersion(request.ValidityFrom, "001");
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.CNV, opeini);
               
                var opeiniOut = await _apiTruck.SetOpecpini(opeini);
                return opeiniOut;
            }
            catch (Exception)
            {
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.ECNV, opeini);
            }

            return await Task.Run(() => new OpecpiniOut());
        }
    }
}
