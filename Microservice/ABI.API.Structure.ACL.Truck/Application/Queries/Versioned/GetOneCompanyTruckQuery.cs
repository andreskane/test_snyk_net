using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneCompanyTruckQuery : IRequest<string>
    {
        public int StructureId { get; set; }
    }

    public class GetOneCompanyTruckQueryHandler : IRequestHandler<GetOneCompanyTruckQuery, string>
    {
        private readonly TruckACLContext _contextACL;
        private readonly StructureContext _context;

        public GetOneCompanyTruckQueryHandler(TruckACLContext contextACL, StructureContext context)
        {
            _contextACL = contextACL;
            _context = context;
        }

        public async Task<string> Handle(GetOneCompanyTruckQuery request, CancellationToken cancellationToken)
        {

            var structure =  await _context.Structures.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.StructureId);

            if (structure != null)
            {
                var companyTruck = await _contextACL.BusinessTruckPortals.AsNoTracking()
                                        .FirstOrDefaultAsync(b => b.Name == structure.Name
                                        && b.StructureModelId == structure.StructureModelId);

                if(companyTruck != null)
                {
                    return companyTruck.BusinessCode.PadLeft(3, '0');
                }

            }

            return string.Empty;
        }
    }
}
