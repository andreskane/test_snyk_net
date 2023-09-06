using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.APIClient.Truck;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckOperativeCommand : IRequest<bool>
    {
        public TruckWritingPayload PlayLoad { get; set; }

    }

    public class TruckOperativeCommandHandler : IRequestHandler<TruckOperativeCommand, bool>
    {
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;
        private readonly IMediator _mediator;
        public TruckWritingPayload PlayLoad { get; set; }


        public TruckOperativeCommandHandler(  ITruckService truckService,
                                        IApiTruck apiTruck,
                                        IMediator mediator)
        {
            _truckService = truckService;
            _apiTruck = apiTruck;
            _mediator = mediator;

        }


        public async Task<bool> Handle(TruckOperativeCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var list = await _mediator.Send(new GetAllVersioneByStructureIdValidityStateId { StateId = (int)VersionedState.Aceptado });

                foreach (var itemAcep in list)
                {
                    try
                    {
                        var companyTruck = await _mediator.Send(new GetOneCompanyTruckQuery { StructureId = itemAcep.StructureId });

                        var iniOperative = await _truckService.GetStructureVersionTruckInput(companyTruck.ToInt(), itemAcep.Version);
                        var opestate = await _apiTruck.GetStructureVersionStatusTruck(iniOperative);

                        if (opestate != null)
                        {
                            foreach (var item in opestate.EstructuraVersiones.Level1)
                            {
                                if (item.ECIndTra == "S")
                                    await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = itemAcep.Id, State = VersionedState.Operativo });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await _truckService.SetVersionedLog(itemAcep.Id, VersionedLogState.EACP, ex.Message);
                        throw;
                    }
                }                
            }
            catch (Exception ex)
            {
                throw new GenericException("Problemas de cambio de estado a Operativo", ex);

            }

            return true;
        }
    }
}
