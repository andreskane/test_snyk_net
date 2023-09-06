using ABI.API.Structure.ACL.Truck.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneFirstVersionPending : IRequest<Domain.Entities.Versioned>
    {
    }

    public class GetOneFirstVersionPendingHandler : IRequestHandler<GetOneFirstVersionPending, Domain.Entities.Versioned>
    {
        private readonly TruckACLContext _context;

        public GetOneFirstVersionPendingHandler(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Versioned> Handle(GetOneFirstVersionPending request, CancellationToken cancellationToken)
        {
            var ver = (from s in _context.Versioneds.AsNoTracking()
                       where s.StatusId == (int) VersionedState.PendienteDeEnvio
                             select s).OrderBy(o=> o.Validity).ThenBy(x=>x.Date).FirstOrDefault();

            return await Task.Run(() => ver);
        }
    }
}
