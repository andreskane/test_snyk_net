using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedPendingInPortalCommand : IRequest<bool>
    {
        public int VersionedId { get; set; }
        public PendingVersionTruckDTO PendingTruck { get; set; }
        public string CompanyTruck { get; set; }
        public TruckWritingPayload PlayLoad { get; set; }
    }

    public class VersionedPendingInPortalCommandHandler : IRequestHandler<VersionedPendingInPortalCommand, bool>
    {
        private readonly IMediator _mediator;

        public VersionedPendingInPortalCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<bool> Handle(VersionedPendingInPortalCommand request, CancellationToken cancellationToken)
        {
            //Tengo un versión pendiente Portal? Y es menor la vigencia
            var versionPortal = await _mediator.Send(new GetOneVersioneByIdQuery { VersionedId = request.VersionedId });
            var versionPortalPending = await _mediator.Send(new GetOneFirstVersionPending());

            if (versionPortalPending != null && (versionPortalPending.Validity < versionPortal.Validity))
            {
                await _mediator.Send(new VersionedPendingVersionIsLowerPortalCommand { VersionPortal = versionPortal, VersionPortalPending = versionPortalPending, PendingTruck = request.PendingTruck, CompanyTruck = request.CompanyTruck, PlayLoad = request.PlayLoad });
            }
            else
            {
                await _mediator.Send(new VersionedNewVersionMinorTruckVersionCommand { VersionPortal = versionPortal, PendingTruck = request.PendingTruck, CompanyTruck = request.CompanyTruck, PlayLoad = request.PlayLoad });
            }
    

            return await Task.Run(() => true);
        }
    }
}
   