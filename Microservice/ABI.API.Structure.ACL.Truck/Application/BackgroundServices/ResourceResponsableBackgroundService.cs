using System;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;

using Microsoft.Extensions.Logging;

namespace ABI.API.Structure.ACL.Truck.Application.BackgroundServices
{
    public class ResourceResponsableBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
	{
		private readonly ISyncResourceResponsable worker;
		private readonly ILogger<SyncResourceResponsable> _logger;
		private readonly ISyncLogRepository _syncRepo;

		public ResourceResponsableBackgroundService(ISyncResourceResponsable worker,ISyncLogRepository syncRepo, ILogger<SyncResourceResponsable> logger)
        {
			this.worker = worker;
			_syncRepo = syncRepo;
			_logger = logger;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await SyncLog("ResourceResponsableBackgroundService terminated.");
			try
			{
				await worker.DoWork(stoppingToken);
			}
			catch (Exception ex)
			{
				_logger.LogInformation(ex.ToString());
				await SyncLog(ex.ToString());
			}

			_logger.LogInformation("ResourceResponsableBackgroundService terminated.");
			await SyncLog("ResourceResponsableBackgroundService terminated.");
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
