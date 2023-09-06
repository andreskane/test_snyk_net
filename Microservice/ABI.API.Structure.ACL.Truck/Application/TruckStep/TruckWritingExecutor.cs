using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.Extensions.TruckStep;
using Coravel.Events.Interfaces;
using Coravel.Invocable;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.TruckStep
{
    public class TruckWritingExecutor : IInvocable, IInvocableWithPayload<TruckWritingPayload>
    {
        public TruckWritingPayload Payload { get; set; }

        private readonly ILogger<TruckWritingExecutor> _logger;
        private readonly IDispatcher _queueDispatcher;
        private readonly IMediator _mediator;

        public TruckWritingExecutor(ILogger<TruckWritingExecutor> logger,  IDispatcher queueDispatcher, IMediator mediator)
        {
            _logger = logger;
            _queueDispatcher = queueDispatcher;
            _mediator = mediator;
        }

        public async Task Invoke()
        {
            await _queueDispatcher.Broadcast(Payload.ToStartEvent());

            try
            {
                //Comienza el proceso de impacto o lo deja pendiente
                var command = new VersionedProcessImpactCommand { Payload = Payload};
                await _mediator.Send(command);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                await _queueDispatcher.Broadcast(Payload.ToErrorEvent());
            }
        }
    }
}
