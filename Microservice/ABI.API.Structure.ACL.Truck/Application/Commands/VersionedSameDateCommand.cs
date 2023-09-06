using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedSameDateCommand : IRequest<int>
    {
        public TruckWritingPayload PlayLoad { get; set; }
        public int VersionedId { get; set; }
        public PendingVersionTruckDTO PendingTruck { get; set; }
        public string CompanyTruck { get; set; }
    }

    public class VersionedSameDateCommanddHandler : IRequestHandler<VersionedSameDateCommand, int>
    {
        private readonly IMediator _mediator;


        public VersionedSameDateCommanddHandler(IMediator mediator )
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(VersionedSameDateCommand request, CancellationToken cancellationToken)
        {//La versión Nueva tiene la misma Fecha que truck

            var vercionPendingPortal = await _mediator.Send(new GetOneVersioneByIdQuery { VersionedId = request.VersionedId });

            if(request.PendingTruck.LastVersionDate.HasValue 
                && request.PendingTruck.LastVersionDate.Value.Date == vercionPendingPortal.Validity.Date)
            {
                // Debe cancelar y enviar de nuevo los cambios portal (SI) -Inifica
               await _mediator.Send(new VersionedCanceledVersionAndResendCommand { Versioned = vercionPendingPortal, VersionTruck = request.PendingTruck.VersionTruck, CompanyTruck = request.CompanyTruck, PlayLoad = request.PlayLoad});
            }
            else 
            {
                // consulta si tengo versiones pendientes (NO)
                await _mediator.Send(new VersionedPendingInPortalCommand { VersionedId = request.VersionedId, PendingTruck = request.PendingTruck, CompanyTruck = request.CompanyTruck, PlayLoad = request.PlayLoad });
            }
 
            return await Task.Run(() => 0);
        }
    }
}
