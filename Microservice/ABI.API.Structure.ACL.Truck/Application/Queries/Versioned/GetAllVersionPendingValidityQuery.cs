using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetAllVersionPendingValidityQuery : IRequest<List<Domain.Entities.Versioned>>
    {
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllVersionPendingValidityQueryHandler : IRequestHandler<GetAllVersionPendingValidityQuery, List<Domain.Entities.Versioned>>
    {
        private readonly IVersionedRepository _repoVersion;

        public GetAllVersionPendingValidityQueryHandler(IVersionedRepository repoVersion)
        {
            _repoVersion = repoVersion;
        }

        public async Task<List<Domain.Entities.Versioned>> Handle(GetAllVersionPendingValidityQuery request, CancellationToken cancellationToken)
        {
            return (List<Domain.Entities.Versioned>)await _repoVersion.GetAllVersionsByValidity(request.ValidityFrom);
        }
    }
}
