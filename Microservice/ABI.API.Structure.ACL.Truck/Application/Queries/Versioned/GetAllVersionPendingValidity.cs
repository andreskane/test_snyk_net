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
    public class GetAllVersionPendingValidity : IRequest<List<Domain.Entities.Versioned>>
    {
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllVersionPendingValidityHandler : IRequestHandler<GetAllVersionPendingValidity, List<Domain.Entities.Versioned>>
    {
        private readonly TruckACLContext _context;

        public GetAllVersionPendingValidityHandler(TruckACLContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Versioned>> Handle(GetAllVersionPendingValidity request, CancellationToken cancellationToken)
        {
            var ver = (from s in _context.Versioneds.AsNoTracking()
                       where s.StatusId == (int) VersionedState.PendienteDeEnvio && s.Validity == request.ValidityFrom
                             select s).OrderBy(o=> o.Date).ToList();

            return await Task.Run(() => ver);
        }
    }
}
