using ABI.API.Structure.ACL.Truck.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllVersioneByStructureIdValidityStateId : IRequest<IList<Domain.Entities.Versioned>>
    {
        public int? StructureId { get; set; }
        public int? StateId { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
    }

    public class GetOneVersioneByStructureIdValidityHandler : IRequestHandler<GetAllVersioneByStructureIdValidityStateId, IList<Domain.Entities.Versioned>>
    {
        private readonly TruckACLContext _context;

        public GetOneVersioneByStructureIdValidityHandler(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<IList<Domain.Entities.Versioned>> Handle(GetAllVersioneByStructureIdValidityStateId request, CancellationToken cancellationToken)
        {
            var ver = (from s in _context.Versioneds.AsNoTracking()
                       select s);

            if (request.StructureId.HasValue)
                ver = ver.Where(v => v.StructureId == request.StructureId);

            if (request.ValidityFrom.HasValue)
                ver = ver.Where(v => v.Validity == request.ValidityFrom);

            if (request.StateId.HasValue)
                ver = ver.Where(v => v.StatusId == request.StateId);

            var items = ver.ToListAsync();

            return await Task.Run(() => items);
        }
    }
}
