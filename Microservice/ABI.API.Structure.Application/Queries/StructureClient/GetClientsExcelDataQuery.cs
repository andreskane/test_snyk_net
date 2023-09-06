using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;

using MediatR;

namespace ABI.API.Structure.Application.Queries.StructureClient
{
    public class GetClientsExcelDataQuery : IRequest<MemoryStream>
    {
        public int StructureId { get; set; }
        public string NodeCode { get; set; }
        public int LevelId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }
    public class GetClientsExcelDataQueryHandler : IRequestHandler<GetClientsExcelDataQuery, MemoryStream>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;

        public GetClientsExcelDataQueryHandler(IStructureNodeRepository structureNodeRepository, IMediator mediator)
        {
            _structureNodeRepository = structureNodeRepository;
            _mediator = mediator;
        }

        public async Task<MemoryStream> Handle(GetClientsExcelDataQuery request, CancellationToken cancellationToken)
        {
            var node = await _structureNodeRepository.GetOneNodoByCodeLevelAsync(request.StructureId, request.NodeCode, request.LevelId);
            if (node == null)
                return null;

            List<StructureClientExcelDto> result = new List<StructureClientExcelDto>();
            string codigoZona;
            string codigoJefatura;
            if (node.LevelId == 6)
            {
                codigoJefatura = node.Code;
                var zonas = await _structureNodeRepository.GetNodoChildAllConfirmByNodeIdAsync(request.StructureId, node.Id, request.ValidityFrom);
                foreach (var zona in zonas)
                {
                    codigoZona = zona.Code;
                    result.AddRange(await GetClientsExcelZone(request.StructureId, zona.Id, request.ValidityFrom, codigoJefatura, codigoZona, cancellationToken));
                }
            }
            else if (node.LevelId == 7)
            {
                codigoZona = node.Code;
                var jefatura = await _structureNodeRepository.GetNodeParentByNodeId(request.StructureId, node.Id, request.ValidityFrom);
                codigoJefatura = jefatura.Code;
                result.AddRange(await GetClientsExcelZone(request.StructureId, node.Id, request.ValidityFrom, codigoJefatura, codigoZona, cancellationToken));
            }
            else if (node.LevelId == 8)
            {
                var zona = await _structureNodeRepository.GetNodeParentByNodeId(request.StructureId, node.Id, request.ValidityFrom);
                codigoZona = zona.Code;
                var jefatura = await _structureNodeRepository.GetNodeParentByNodeId(request.StructureId, zona.Id, request.ValidityFrom);
                codigoJefatura = jefatura.Code;
                result.AddRange(await GetClientsExcelTerritory(node.Id, request.ValidityFrom, codigoJefatura, codigoZona, node.Code, cancellationToken));
            }
            return CreateExcelFile.StreamExcelDocument<StructureClientExcelDto>(result, "Template.xlsx");
        }

        private async Task<IList<StructureClientExcelDto>> GetClientsExcelZone(
            int structureId,
            int zoneId,
            DateTimeOffset validityFrom,
            string codigoJefatura,
            string codigoZona,
            CancellationToken cancellationToken)
        {
            List<StructureClientExcelDto> result = new List<StructureClientExcelDto>();
            var territories = await _structureNodeRepository.GetNodoChildAllConfirmByNodeIdAsync(structureId, zoneId, validityFrom);
            foreach (var territory in territories)
            {
                result.AddRange(await GetClientsExcelTerritory(territory.Id, validityFrom, codigoJefatura, codigoZona, territory.Code, cancellationToken));
            }
            return result;
        }

        private async Task<IList<StructureClientExcelDto>> GetClientsExcelTerritory(
            int nodeId,
            DateTimeOffset validity,
            string codigoJefatura,
            string codigoZona,
            string territoryCode,
            CancellationToken cancellationToken) 
        {
            var clients = await _mediator.Send(new GetOneNodeClientQuery { NodeId = nodeId, ValidityFrom = validity }, cancellationToken);
            if (clients != null && clients.Count > 0)
            {
                return (from cli in clients
                          select new StructureClientExcelDto
                          {
                              ClienteId = cli.ClientId,
                              Nombre = cli.Name,
                              CodigoJefatura = codigoJefatura,
                              CodigoZona = codigoZona,
                              CodigoTerriotiro = territoryCode
                          }).ToList();
            }
            return new List<StructureClientExcelDto>();
        }
    }
}
