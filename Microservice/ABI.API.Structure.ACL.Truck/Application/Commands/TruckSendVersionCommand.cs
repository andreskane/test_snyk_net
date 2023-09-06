using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TruckSendVersionCommand : IRequest<bool>
    {
        public Versioned Versioned { get; set; }
        public TruckWritingPayload PlayLoad { get; set; }
    }

    public class TruckSendVersionCommandHandler : IRequestHandler<TruckSendVersionCommand, bool>
    {
        private readonly IMediator _mediator;

        public TruckSendVersionCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(TruckSendVersionCommand request, CancellationToken cancellationToken)
        {

            // 1. Obtengo los datos con los cambios
            var nodes = await _mediator.Send(new GetAllNodesSentTruckByVersionedIdQuery { VersionedId = request.Versioned.Id });
            var aristas = await _mediator.Send(new GetAllAristasSentTruckByVersionedIdQuery { VersionedId = request.Versioned.Id });

            var listNodes = nodes.Union(aristas).ToList();

            listNodes = listNodes.GroupBy(g => g.NodeId).Select(s => s.First()).ToList();

            // 2. Genero nueva Version en Truck
            var truckNewVersion = await _mediator.Send(new TruckOpeNewVersionCommand { VersionedId = request.Versioned.Id, ValidityFrom = request.Versioned.Validity });
            request.Versioned.Version = truckNewVersion.Epeciniin.NroVer;
            request.Versioned.GenerateVersionDate = DateTimeOffset.UtcNow;
            await _mediator.Send(new VersionedUpdateVersionCommand { Versioned = request.Versioned });

            // 3. Realizo tranformaciones de Portal a Truck
            var structureTruck = await _mediator.Send(new TransformationPortalToTruckCommand { StructureId = request.Versioned.StructureId, VersionedId = request.Versioned.Id, Nodes = listNodes, OpecpiniOut = truckNewVersion });

            if (structureTruck != null)
            {

                // 4. Envio a TRuck en las bandejas
                var loadTrays = await _mediator.Send(new TruckLoadTraysCommand { StructureTruck = structureTruck, VersionedId = request.Versioned.Id, OpecpiniOut = truckNewVersion });

                // 5. APR - envio version para validar y aprobar
                if (loadTrays)
                {
                    var apr = await _mediator.Send(new TruckOpeAPRCommand { VersionedId = request.Versioned.Id, OpeiniOut = truckNewVersion, ValidityFrom = request.Versioned.Validity, PlayLoad = request.PlayLoad });
                }


                return await Task.Run(() => true);
            }

            return await Task.Run(() => false);
        }

    }
}
