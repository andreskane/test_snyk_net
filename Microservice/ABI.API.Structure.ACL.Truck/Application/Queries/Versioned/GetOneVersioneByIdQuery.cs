using ABI.API.Structure.ACL.Truck.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneVersioneByIdQuery : IRequest<Domain.Entities.Versioned>
    {
        public int VersionedId { get; set; }
    }

    public class GetVersioneByIdQueryHandler : IRequestHandler<GetOneVersioneByIdQuery, Domain.Entities.Versioned>
    {
        private readonly TruckACLContext _context;

        public GetVersioneByIdQueryHandler()
        {
        }

        public GetVersioneByIdQueryHandler(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Versioned> Handle(GetOneVersioneByIdQuery request, CancellationToken cancellationToken)
        {
            var ver = (from s in _context.Versioneds.AsNoTracking()
                       where s.Id == request.VersionedId
                       select s).FirstOrDefault();

            return await Task.Run(() => ver);
        }
    }
}
