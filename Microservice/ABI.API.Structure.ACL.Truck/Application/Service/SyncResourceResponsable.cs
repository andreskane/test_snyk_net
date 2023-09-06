using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class SyncResourceResponsable : ISyncResourceResponsable
    {
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IResourceResponsableRepository _resourceResponsableRepository;
        private readonly ISyncLogRepository _syncRepo;
        private readonly ILogger<SyncResourceResponsable> _logger;

        public SyncResourceResponsable(
            IDBUHResourceRepository dBUHResourceRepository, 
            IResourceResponsableRepository resourceResponsableRepository,
            ISyncLogRepository syncRepo,
            ILogger<SyncResourceResponsable> logger)
        {
            _dBUHResourceRepository = dBUHResourceRepository;
            _resourceResponsableRepository = resourceResponsableRepository;
            _syncRepo = syncRepo;
            _logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            await SyncLog("SyncResourceResponsable started.");

            try
            {
                await Synchronize();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                await SyncLog("SyncResourceResponsable " + ex.ToString());
            }

            await SyncLog("SyncResourceResponsable terminated.");
        }

        public async Task Synchronize()
        {
            try
            {
                var Source = TransformToResourceResponsable(await _dBUHResourceRepository.GetAllResource());
                var Destino = await _resourceResponsableRepository.GetAll();

                //busco los valores de que no estan en destino y los doy de alta - Relacion tipo truck
                List<ResourceResponsable> newResources = Source.Where(p => !Destino.Any(p2 => p2.TruckId == p.TruckId && p2.VendorCategory == p.VendorCategory)).ToList();
                await _resourceResponsableRepository.BulkInsertOrUpdateAsync(newResources);

                //busco los valores de que no estan en origen y los doy de baja 
                List<ResourceResponsable> deleteResources = Destino.Where(p => !Source.Any(p2 => p2.TruckId == p.TruckId && p2.VendorCategory == p.VendorCategory)).ToList();
                await _resourceResponsableRepository.BulkDeleteteAsync(deleteResources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private static List<ResourceResponsable> TransformToResourceResponsable(IList<ResourceDTO> items)
        {
            var result = new List<ResourceResponsable>();

            foreach (var item in items)
            {
                var itemR = new ResourceResponsable();
        
                var relations = item.Relations.Where(x => x.Type == 1).ToList();
                foreach (var itemRelation in relations)
                {
                    itemR.ResourceId = item.Id;
                    itemR.TruckId = itemRelation.Attributes.VdrCod;
                    itemR.IsVacant = itemRelation.Attributes.Vacante == "S";
                    itemR.VendorCategory = itemRelation.Attributes.VdrTpoCat;
                    itemR.CountryId = item.CountryId;
                    result.Add(itemR);
                }
            }
            return result;
        }

        private async Task SyncLog(string message)
        {
            _logger.LogInformation(message);

            await _syncRepo.Create(new SyncLog
            {
                Timestamp = DateTime.UtcNow,
                Message = message
            });
        }
    }
}
