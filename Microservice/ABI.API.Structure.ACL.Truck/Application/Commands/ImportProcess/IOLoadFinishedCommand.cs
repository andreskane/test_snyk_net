
using ABI.API.Structure.ACL.Truck.Application.BackgroundServices;
using ABI.API.Structure.ACL.Truck.Domain.Enums;

using Coravel.Queuing.Interfaces;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess
{
    public class IOLoadFinishedCommand : IRequest
    {
        public int ProccessId { get; set; }
        public int RowsCount { get; set; }
        public ImportProcessState State { get; set; }
    }

    public class IOLoadFinishedCommandHandler : IRequestHandler<IOLoadFinishedCommand>
    {
        public const int CuentaOtrasId = 999;
        private readonly IQueue _queue;
 

        public IOLoadFinishedCommandHandler(IQueue queue)
        {
            _queue = queue;            
        }

        public Task<Unit> Handle(IOLoadFinishedCommand request, CancellationToken cancellationToken)
        {

            _queue.QueueInvocableWithPayload<IOFinishProcessExecutor, IOLoadFinishedCommand>(request);

           
            return Task.FromResult(Unit.Value);

        }
    }

}