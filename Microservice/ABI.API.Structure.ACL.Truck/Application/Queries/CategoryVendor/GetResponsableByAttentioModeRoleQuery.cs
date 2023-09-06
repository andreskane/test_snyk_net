using ABI.API.Structure.ACL.Truck.Application.Queries.TypeVendor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.CategoryVendor
{
    public class GetResponsableByAttentioModeRoleQuery : IRequest<bool>
    {
        public int AttentionModeId { get; set; }
        public int? RoleId { get; set; }
    }
    public class GetResponsableByAttentioModeRoleHandler : IRequestHandler<GetResponsableByAttentioModeRoleQuery, bool>
    {
        private readonly IMediator _mediator;

        public GetResponsableByAttentioModeRoleHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(GetResponsableByAttentioModeRoleQuery request, CancellationToken cancellationToken)
        {
            bool esResponsable = false;

            var result = await _mediator.Send(new GetByAttentionModeIdQuery { AttentionModeId = request.AttentionModeId, RoleId = request.RoleId });
            if (result != null)
            {
                var category = await _mediator.Send(new GetByVendorTruckIdQuery { VendorTruckId = result.VendorTruckId });
                if (category != null)
                    esResponsable = category.CategoryResponsable == "S";
                    
            }

            return await Task.Run(() => esResponsable);
        }
    }
}
