using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.TypeVendor
{
    public class GetByAttentionModeIdQuery : IRequest<TypeVendorTruckPortal>
    {
        public int AttentionModeId { get; set; }
        public int? RoleId { get; set; }
    }

    public class GetByAttentionModeIdHandler : IRequestHandler<GetByAttentionModeIdQuery, TypeVendorTruckPortal>
    {
        private readonly TruckACLContext _context;

        public GetByAttentionModeIdHandler(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<TypeVendorTruckPortal> Handle(GetByAttentionModeIdQuery request, CancellationToken cancellationToken)
        {
            var item = (from s in _context.TypeVendorsTruckPortal
                         where s.RoleId == request.RoleId && s.AttentionModeId == request.AttentionModeId
                         select s).FirstOrDefault();
            return await Task.Run(() => item);
        }
    }
}