using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedCancelChangeCommand : IRequest<Unit>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public string Username { get; set; }
    }

    public class VersionedCancelChangeCommandHandler : IRequestHandler<VersionedCancelChangeCommand, Unit>
    {
        private readonly IMediator _mediator;


        public VersionedCancelChangeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<Unit> Handle(VersionedCancelChangeCommand request, CancellationToken cancellationToken)
        {
            var canceled = await _mediator.Send(new VersionedCancelAllChangesCommand { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            if (!canceled)
                return Unit.Value;

            await _mediator.Send(new VersionedProcessImpactCommand
            {
                Payload = new TruckStep.TruckWritingPayload
                {
                    StructureId = request.StructureId,
                    Date = request.ValidityFrom,
                    Username = request.Username
                }
            });

            return Unit.Value;
        }
    }
}
