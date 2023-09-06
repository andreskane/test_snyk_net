using ABI.API.Structure.ACL.Truck.Application.Commands;
using Coravel.Invocable;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.BackgroundServices
{
    public class ProcessVersionedExecutor : IInvocable, IInvocableWithPayload<ProcessVersionedCommand>
    {
        private readonly ILogger<ProcessVersionedExecutor> _logger;
        private readonly IMediator _mediator;
        public ProcessVersionedCommand Payload { get; set; }

        public ProcessVersionedExecutor(
            ILogger<ProcessVersionedExecutor> logger,
            IMediator  mediator
        )
        {

            _logger = logger;
            _mediator = mediator;

        }
        public async Task Invoke()
        {
            try
            {
                //Verifica que las versiones que estan en Aceptadas ya estan implemantadas

                _logger.LogInformation("Set VersionedProcessPendingCommand");
                await _mediator.Send(new VersionedProcessPendingCommand()); 
                _logger.LogInformation("End VersionedProcessPendingCommand");
               
                _logger.LogInformation("Start TruckOperativeCommand");
                await _mediator.Send(new TruckOperativeCommand());
                _logger.LogInformation("End TruckOperativeCommand");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.StackTrace);
            }

        }
    }
}

