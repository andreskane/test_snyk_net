using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Translators.Interface;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class TransformationPortalToTruckCommand : IRequest<StructureTruck>
    {
        public int VersionedId { get; set; }
        public int StructureId { get; set; }
        public OpecpiniOut OpecpiniOut { get; set; }
        public IList<NodePortalTruckDTO> Nodes { get; set; }
    }

    public class TransformationPortalToTruckCommandHandler : IRequestHandler<TransformationPortalToTruckCommand, StructureTruck>
    {
        private readonly IMediator _mediator;
        private readonly ITruckService _truckService;
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly ITranslatorsStructuresPortalToTruck _translatorsStructuresPortalTruck;


        public TransformationPortalToTruckCommandHandler(
            IMediator mediator, ITruckService truckService,
            IDBUHResourceRepository dBUHResourceRepository,
            IStructureNodeRepository repositoryStructureNode,
            IStructureNodePortalRepository repositoryStructureNodePortal,
            IMapeoTableTruckPortal mapeoTableTruckPortal,
            ITranslatorsStructuresPortalToTruck translatorsStructuresPortalTruck)
        {
            _mediator = mediator;
            _truckService = truckService;
            _dBUHResourceRepository = dBUHResourceRepository;
            _translatorsStructuresPortalTruck = translatorsStructuresPortalTruck;
        }


        public async Task<StructureTruck> Handle(TransformationPortalToTruckCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resource = await _dBUHResourceRepository.GetAllResource();
                
                var structureTruck =  await _translatorsStructuresPortalTruck.PortalToTruckAsync(request.OpecpiniOut, request.StructureId, request.Nodes.ToList(), resource.ToList());
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.TPT, structureTruck);

                return await Task.Run(() => structureTruck);

            }
            catch (Exception ex)
            {
                
                await _truckService.SetVersionedLog(request.VersionedId, VersionedLogState.ETPT, ex.ToString());
                await _mediator.Send(new TruckOpeRCHCommand { VersionedId = request.VersionedId, ValidityFrom = DateTimeOffset.UtcNow.Date.ToDateOffset(), OpeiniOut = request.OpecpiniOut }); //TODO: Ojo Multipais

                await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = request.VersionedId, State = VersionedState.PendienteDeEnvio });
                return null;
            }
        }
    }
}
