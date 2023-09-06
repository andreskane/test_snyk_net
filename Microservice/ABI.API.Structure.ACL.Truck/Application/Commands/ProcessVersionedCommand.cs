using ABI.API.Structure.ACL.Truck.Application.BackgroundServices;
using Coravel.Queuing.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{

    public class ProcessVersionedCommand : IRequest 
    { 
       
    }

    public class ProcessVersionedCommandHandler : IRequestHandler<ProcessVersionedCommand>
    {
        
        private readonly IQueue _queue;
 

        public ProcessVersionedCommandHandler(
            IQueue queue 
           )
        {
            _queue = queue;
          
        }

        public Task<Unit> Handle(ProcessVersionedCommand request, CancellationToken cancellationToken)
        {
            
            _queue.QueueInvocableWithPayload<ProcessVersionedExecutor, ProcessVersionedCommand>(request);
            return Task.FromResult(Unit.Value);

        }

         
    }
}
