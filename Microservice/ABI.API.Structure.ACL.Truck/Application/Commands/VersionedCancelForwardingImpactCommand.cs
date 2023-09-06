using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedCancelForwardingImpactCommand : IRequest<bool>
    {
        public TruckWritingPayload PlayLoad { get; set; }
        public Versioned Versioned { get; set; }
        public string VersionTruck { get; set; }
        public string CompanyTruck { get; set; }
    }

    public class VersionedCancelForwardingImpactCommandHandler : IRequestHandler<VersionedCancelForwardingImpactCommand, bool>
    {
        private readonly IMediator _mediator;
        

        public VersionedCancelForwardingImpactCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<bool> Handle(VersionedCancelForwardingImpactCommand request, CancellationToken cancellationToken)
        {

            var opeIni = new OpecpiniOut();

            opeIni.Epeciniin.NroVer = request.VersionTruck;
            opeIni.Epeciniin.Empresa = request.CompanyTruck;
            request.Versioned.Version = request.VersionTruck.PadLeft(6, '0');
            request.Versioned.GenerateVersionDate = DateTimeOffset.UtcNow;

            await _mediator.Send(new VersionedUpdateVersionCommand { Versioned = request.Versioned });

            await _mediator.Send(new TruckOpeUPDCommand { VersionedId = request.Versioned.Id, OpeiniOut = opeIni, ValidityFrom = request.Versioned.Date });

            // 1. Obtengo los datos con los cambios
            var nodes = await _mediator.Send(new GetAllNodesSentTruckByVersionedIdQuery { VersionedId = request.Versioned.Id });
            // 2. Realizo tranformaciones de Portal a Truck
            var structureTruck = await _mediator.Send(new TransformationPortalToTruckCommand { StructureId = request.Versioned.StructureId, VersionedId = request.Versioned.Id, Nodes = nodes, OpecpiniOut = opeIni });
            // 3. Envio a TRuck en las bandejas
            if (structureTruck != null)
            {
                var loadTrays = await _mediator.Send(new TruckLoadTraysCommand { VersionedId = request.Versioned.Id, StructureTruck = structureTruck });
                // 4. APR - envio version para validar y aprobar
                if (loadTrays)
                {
                    var apr = await _mediator.Send(new TruckOpeAPRCommand { VersionedId = request.Versioned.Id, OpeiniOut = opeIni, ValidityFrom = request.Versioned.Validity, PlayLoad = request.PlayLoad });
                }
            }
        
            return await Task.Run(() => false);
        }
    }
}
