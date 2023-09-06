using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedCanceledVersionAndResendCommand : IRequest<bool>
    {
        public TruckWritingPayload PlayLoad { get; set; }
        public Versioned Versioned { get; set; }
        public string VersionTruck { get; set; }
        public string CompanyTruck { get; set; }
    }

    public class VersionedCanceledVersionAndResendCommandHandler : IRequestHandler<VersionedCanceledVersionAndResendCommand, bool>
    {
        private readonly IMediator _mediator;
        

        public VersionedCanceledVersionAndResendCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<bool> Handle(VersionedCanceledVersionAndResendCommand request, CancellationToken cancellationToken)
        {

            var opeIni = new OpecpiniOut();

            var result = true;

            opeIni.Epeciniin.NroVer = request.VersionTruck.PadLeft(6, '0');
            opeIni.Epeciniin.Empresa = request.CompanyTruck;


            var VersionReject = await _mediator.Send(new GetOneVersionByVerTruckQuery { VerTruck = opeIni.Epeciniin.NroVer });

            if (VersionReject != null)
            {
                await _mediator.Send(new TruckOpeUPDCommand { VersionedId = VersionReject.Id, OpeiniOut = opeIni, ValidityFrom = request.Versioned.Date });
                await _mediator.Send(new TruckOpeRCHCommand { VersionedId = VersionReject.Id, OpeiniOut = opeIni, ValidityFrom = request.Versioned.Date, KeepVersionPending = false });

                //envio a truck ultima version
                result = await _mediator.Send(new TruckSendVersionCommand { Versioned = request.Versioned, PlayLoad = request.PlayLoad });

                if (result)
                {
                    var message = $"Versión unificada con el lote Truck Nro: {request.Versioned.Version} - Versión Portal: {request.Versioned.Id}";
                    await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = VersionReject.Id, State = VersionedState.Unificado, Message = message });
                }
                else
                {
                    // Si rechaza ultima version vuelve a enviar la version anterior
                    result = await _mediator.Send(new TruckSendVersionCommand { Versioned = VersionReject, PlayLoad = request.PlayLoad });
                }
            }

            return await Task.Run(() => result);
        }
    }
}
