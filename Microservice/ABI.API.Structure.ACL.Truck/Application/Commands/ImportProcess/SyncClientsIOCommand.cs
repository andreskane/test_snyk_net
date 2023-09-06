using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess
{
    public class SyncClientsIOCommand : IRequest
    {
        public int ProccessId { get; set; }
    }
    public class SyncClientsIOCommandHandler : IRequestHandler<SyncClientsIOCommand>
    {
        private readonly ILogger<SyncClientsIOCommand> _logger;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IPortalService _portalService;
        private readonly ITruckToPortalService _truckToPortalService;
        private readonly ISyncLogRepository _syncLogRepo;
        private readonly IEstructuraClienteTerritorioIORepository _clienteRepository;
        private readonly IStructureClientRepository _structureClientRepository;
        private readonly IImportProcessRepository _importProcessRepository;

        public SyncClientsIOCommandHandler(
            ILogger<SyncClientsIOCommand> logger,
            IMapeoTableTruckPortal mapeoTableTruckPortal,
            IPortalService portalService,
            ITruckToPortalService truckToPortalService,
            ISyncLogRepository syncLogRepo,
            IEstructuraClienteTerritorioIORepository clienteRepository,
            IStructureClientRepository structureClientNodeRepository,
            IImportProcessRepository importProcessRepository
        )
        {
            _logger = logger;
            _mapeoTableTruckPortal = mapeoTableTruckPortal;
            _portalService = portalService;
            _truckToPortalService = truckToPortalService;
            _syncLogRepo = syncLogRepo;
            _clienteRepository = clienteRepository;
            _structureClientRepository = structureClientNodeRepository;
            _importProcessRepository = importProcessRepository;
        }

        public async Task<Unit> Handle(SyncClientsIOCommand request, CancellationToken cancellationToken)
        {
            await SyncLog("SyncClientsIOService start.");
            var importProcess = await _importProcessRepository.GetImportProcessById(request.ProccessId, default);
            var mapeoTruck = await _mapeoTableTruckPortal.GetOneBusinessTruckPortal(importProcess.CompanyId.Substring(0, 2));

            //todo:las estrucutras se buscan por codigo/id no nombre pueden existir nombres duplicados
            var structure = await _portalService.GetStrucureByName(mapeoTruck.Name);
            if (structure == null)
                return Unit.Value;

            var processClients = await _clienteRepository.GetLastDataByCountryNoTracking(importProcess.CompanyId, cancellationToken); //Recupero los clientes por pais
            if (processClients != null && processClients.Count > 0)
            {
                var territorys = await _truckToPortalService.GetNodesTerritoryAsync(structure.Id); //Recupero todos los territorios de la estructura                
                var validityFrom = DateTimeOffset.UtcNow.Today(-3);//todo:esto solo sirve para argentina, trabajar el tema de multipais

                var source = TransformToClientNode(processClients, validityFrom, territorys);//Transformo en lo que se deberá comparar y son los nuevos
                var destino = await _structureClientRepository.GetAllCurrentByStructureId(structure.Id, cancellationToken);//Recupero los clientes vigentes de la estructura

                await SyncAdded(source, destino, structure.Id); //Agrego los importados
                await SyncDeleted(source, destino, structure.Id, validityFrom); //cierro vigencia de los que estan
                await SyncModify(source, destino, structure.Id, validityFrom); //Cerrar vigencia del modificado y crear el nuevo
            }
            else
            {
                await SyncLog("SyncClientsIOService - There are no clients to import in the structure: " + mapeoTruck.Name);
            }
            await SyncLog("SyncClientsIOService complete.");

            return Unit.Value;
        }

        private async Task SyncLog(string message)
        {
            _logger.LogInformation(message);

            await _syncLogRepo.Create(new SyncLog
            {
                Timestamp = DateTime.UtcNow,
                Message = message
            });
        }

        private static IList<StructureClientNode> TransformToClientNode(IList<EstructuraClienteTerritorioIO> clients, DateTimeOffset date, List<StructureNode> territorys)
        {
            //todo:ver de mejorar con algun mapper
            var structureClientNodes = new List<StructureClientNode>();

            foreach (var item in territorys)
            {
                var territorysClients = clients.Where(c => c.CliTrrId == item.Code).ToList();

                foreach (var itemClient in territorysClients)
                {

                    var client = new StructureClientNode(item.Id, itemClient.CliNom, itemClient.CliId, itemClient.CliSts, date);
                    structureClientNodes.Add(client);
                }
            }

            return structureClientNodes;
        }
        private async Task SyncAdded(IList<StructureClientNode> source, IList<StructureClientNode> destino, int structureId)
        {
            await SyncLog("Syncing added items...");

            List<StructureClientNode> resultToAdd = source.Where(p => !destino.Any(p2 => p2.ClientId == p.ClientId && p2.NodeId == p.NodeId)).ToList();
            await _structureClientRepository.BulkInsertAsync(resultToAdd, structureId);

            await SyncLog($"Sync done. Clients added ({resultToAdd.Count})");
        }
        private async Task SyncDeleted(IList<StructureClientNode> source, IList<StructureClientNode> destino, int structureId, DateTimeOffset validityFrom)
        {
            await SyncLog("Syncing Delete - close validity items...");

            List<StructureClientNode> deleteResources = destino.Where(p => !source.Any(p2 => p2.NodeId == p.NodeId && p2.ClientId == p.ClientId)).ToList();

            //todo:aca hacer un proceso bulk
            deleteResources.ForEach(a =>
            {
                a.EditValidityTo(validityFrom.AddDays(-1));
            });

            await _structureClientRepository.BulkUpdateAsync(deleteResources, structureId);
            await SyncLog($"Sync Delete done. Clients affected ({deleteResources.Count})");
        }
        private async Task SyncModify(IList<StructureClientNode> source, IList<StructureClientNode> destino, int structureId, DateTimeOffset validityFrom)
        {
            await SyncLog("Syncing close validity items...");

            List<StructureClientNode> updateResources = destino
                .Where(p => source.Any(p2 => 
                                p2.NodeId == p.NodeId && 
                                p2.ClientId == p.ClientId &&
                                p2.ClientState != p.ClientState)
                ).ToList();

            //todo:aca hacer un proceso bulk
            updateResources.ForEach(a =>
            {
                a.EditValidityTo(validityFrom.AddDays(-1));
            });

            List<StructureClientNode> modifyClients = source
                .Where(p => destino.Any(p2 => 
                                p2.ClientId == p.ClientId && 
                                p2.NodeId == p.NodeId &&
                                p2.ClientState != p.ClientState)
                ).ToList();

            await _structureClientRepository.BulkUpdateAsync(updateResources, structureId);
            await _structureClientRepository.BulkInsertAsync(modifyClients, structureId);

            await SyncLog($"Sync done. Clients affected ({modifyClients.Count})");
        }
    }
}
