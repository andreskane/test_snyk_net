using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedGenerateVersionCommand : IRequest<Versioned>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public string User { get; set; }
    }

    public class VersionedGenerateVersionCommandHandler : IRequestHandler<VersionedGenerateVersionCommand, Versioned>
    {
        private readonly IMediator _mediator;
        private readonly ITruckService _truckService;


        public VersionedGenerateVersionCommandHandler(IMediator mediator, ITruckService truckService)
        {
            _mediator = mediator;
            _truckService = truckService;
    }


        public async Task<Versioned> Handle(VersionedGenerateVersionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.User))
                request.User = "-";

            var versioned = await _truckService.SetVersionedNew(request.StructureId, request.ValidityFrom, request.User);

            var nodes = await _mediator.Send(new GetAllNodesSentTruckQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });
            var aristas = await _mediator.Send(new GetAllAristasSentTruckQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });


            foreach (var itemNode in nodes)
            {
                await _truckService.SetVersionedNode(versioned.Id, itemNode.NodeId, itemNode.NodeDefinitionId ?? 0);
            }

            foreach (var itemAsrista in aristas)
            {
                await _truckService.SetVersionedArista(versioned.Id, itemAsrista.AristaId ?? 0);
            }

            
                return await Task.Run(() => versioned);
        }
    }
}
