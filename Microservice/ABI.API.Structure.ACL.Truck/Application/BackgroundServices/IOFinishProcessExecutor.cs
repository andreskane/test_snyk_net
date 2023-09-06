using ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using Coravel.Invocable;

using MediatR;

using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.BackgroundServices
{
    public class IOFinishProcessExecutor : IInvocable, IInvocableWithPayload<IOLoadFinishedCommand>
    {
        private readonly IEstructuraClienteTerritorioIORepository _ImportProcessIORepository;
        private readonly IImportProcessRepository _importProcessRepository;
        private readonly ILogger<IOFinishProcessExecutor> _logger;
        private readonly IMediator _mediator;

        public IOLoadFinishedCommand Payload { get; set; }

        public IOFinishProcessExecutor(
            IEstructuraClienteTerritorioIORepository importProcessIORepository,
            IImportProcessRepository importProcessRepository,
            ILogger<IOFinishProcessExecutor> logger,
            IMediator mediator
        ) 
        {
            _ImportProcessIORepository = importProcessIORepository;
            _importProcessRepository = importProcessRepository;
            _logger = logger;
            _mediator = mediator;
        }
       
        public async Task Invoke()
        {
            try
            {
                _logger.LogInformation("Set State Import Process");
                await _importProcessRepository.SetState(Payload.ProccessId, Payload.State, default);
                _logger.LogInformation("End set State Import Process");

                _logger.LogInformation("Start Delete bulk Import Process");
                await _ImportProcessIORepository.BulkDeleteRestProcess(Payload.ProccessId, default);
                _logger.LogInformation("End Delete bulk Import Process");

                _logger.LogInformation("Start Synchronize Import Process");
                var command = new SyncClientsIOCommand { ProccessId = Payload.ProccessId };
                await _mediator.Send(command);
                _logger.LogInformation("End Synchronize Import Process");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }              
        }
    }
}
