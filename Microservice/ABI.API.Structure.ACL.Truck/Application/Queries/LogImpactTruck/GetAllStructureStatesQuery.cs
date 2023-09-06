using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.LogImpactTruck
{
    public class GetAllStructureStatesQuery : IRequest<IList<VersionedStructureStateDTO>>
    {
    }

    public class GetAllStructureStatesQueryHandler : IRequestHandler<GetAllStructureStatesQuery, IList<VersionedStructureStateDTO>>
    {
        private readonly TruckACLContext _context;


        public GetAllStructureStatesQueryHandler(TruckACLContext context)
        {
            _context = context;
        }


        public async Task<IList<VersionedStructureStateDTO>> Handle(GetAllStructureStatesQuery request, CancellationToken cancellationToken)
        {

            var results = await (_context.Versioneds.AsNoTracking()
                               .GroupBy(a => new { a.StructureId, a.StatusId })
                               .Select(n => new VersionedStructureStateDTO
                               {
                                   StructureId = n.Key.StructureId,
                                   StateId = n.Key.StatusId
                               }
                               )
                           ).ToListAsync();


            return results;
        }
    }
}
