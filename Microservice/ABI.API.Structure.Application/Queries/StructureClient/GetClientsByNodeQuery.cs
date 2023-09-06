using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;

using MediatR;

namespace ABI.API.Structure.Application.Queries.StructureClient
{
    public class GetClientsByNodeQuery : IRequest<DTO.StructureQuantityAndClientDto>
    {
        public int StructureId { get; set; }
        public string NodeCode { get; set; }
        public int LevelId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }
    public class GetClientsByNodeQueryHandler : IRequestHandler<GetClientsByNodeQuery, DTO.StructureQuantityAndClientDto>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;

        public GetClientsByNodeQueryHandler(IStructureNodeRepository structureNodeRepository, IMediator mediator)
        {
            _structureNodeRepository = structureNodeRepository;
            _mediator = mediator;
        }

        public async Task<DTO.StructureQuantityAndClientDto> Handle(GetClientsByNodeQuery request, CancellationToken cancellationToken)
        {
            var result = new DTO.StructureQuantityAndClientDto();
            var node = await _structureNodeRepository.GetNodoOneByCodeLevelAsync(request.StructureId, request.NodeCode, request.LevelId);
            if (node == null)
                return result;

            List<DTO.StructureClientDTO> clients = new List<DTO.StructureClientDTO>();

            //SI ES TERRITORIO TENGO QUE ENVIAR LOS CLIENTES SINO TENGO QUE MANDAR SOLO LA CANTIDAD DEL TOTAL DE TODA LA RAMA
            if (node.LevelId == 8)
            {
                clients = await _mediator.Send(new GetOneNodeClientQuery { NodeId = node.Id, ValidityFrom = request.ValidityFrom }, cancellationToken);

                if (clients.Count == 0)
                    return result;

                result.Clients = clients
                            .GroupBy(p => new { p.Name, p.ClientId })
                            .Select(g => g.First())
                            .ToList();
            }
            else
            {
                var aristas = await _mediator.Send(new GetAllAristaQuery { StructureId = request.StructureId, NodeId = node.Id, ValidityFrom = request.ValidityFrom, LevelId=8, OnlyConfirmedAndCurrent = true }, cancellationToken);
                var nodesD = aristas.Select(s => s.NodeIdFrom);
                var nodesH = aristas.Select(s => s.NodeIdTo);
                var listNode = nodesD.Union(nodesH).Distinct().ToList();

                clients = await _mediator.Send(new GetAllNodeClientQuery { NodeIds = listNode, ValidityFrom = request.ValidityFrom }, cancellationToken);
                if (clients.Count == 0)
                    return result;
            }
            
            result.Quantity = clients
                            .GroupBy(p => new { p.Name, p.ClientId })
                            .Select(g => g.First())
                            .Count();

            return result;
        }
    }
}
