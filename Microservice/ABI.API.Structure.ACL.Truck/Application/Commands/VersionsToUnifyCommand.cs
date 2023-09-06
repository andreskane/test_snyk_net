using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionsToUnifyCommand : IRequest<Versioned>
    {
        public TruckWritingPayload Payload { get; set; }


        public class VersionsToUnifyCommandHandler : IRequestHandler<VersionsToUnifyCommand, Versioned>
        {
            private readonly IMediator _mediator;

            public VersionsToUnifyCommandHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Versioned> Handle(VersionsToUnifyCommand request, CancellationToken cancellationToken)
            {
                var list = request.Payload.VersionsToUnify;

                var max = list.Max(t => t.Date);
                var result = list.Where(t => t.Date != max).ToList();

                var versionedPendingPortal = list.FirstOrDefault(t => t.Date == max);

                foreach (var item in result)
                {
                    await _mediator.Send(new VersionedUpdateStateVersionCommand { VersionedId = item.Id, State = VersionedState.Unificado });
                }

                return await Task.Run(() => versionedPendingPortal);
            }
        }
    }
}
