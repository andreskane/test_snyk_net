using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneVersionByVerTruckQuery : IRequest<Domain.Entities.Versioned>
    {
        public string VerTruck { get; set; }
    }

    public class GetOneVersionByVerTruckQueryHandler : IRequestHandler<GetOneVersionByVerTruckQuery, Domain.Entities.Versioned>
    {
        private readonly IVersionedRepository _repoVersion;

        public GetOneVersionByVerTruckQueryHandler(IVersionedRepository repoVersion)
        {
            _repoVersion = repoVersion;
        }

        public async Task<Domain.Entities.Versioned> Handle(GetOneVersionByVerTruckQuery request, CancellationToken cancellationToken)
        {
            return await _repoVersion.GetOneVersionByVerTruck(request.VerTruck);
        }
    }
}
