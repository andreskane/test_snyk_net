using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructureDomainQuery : IRequest<StructureDomain>
    {

        public int StructureId { get; set; }
    }

    public class GetStructureDomainQueryHandler : IRequestHandler<GetStructureDomainQuery, StructureDomain>
    {
        private readonly StructureContext _context;

        public GetStructureDomainQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<StructureDomain> Handle(GetStructureDomainQuery request, CancellationToken cancellationToken)
        {
            var structure = await (from est in _context.Structures.AsNoTracking()
                                   .Include(m => m.StructureModel).ThenInclude(c=>c.Country).AsNoTracking()
                                   .Include(n => n.Node).ThenInclude(d => d.StructureNodoDefinitions).AsNoTracking()
                                   where est.Id == request.StructureId
                                   select est
                    ).FirstOrDefaultAsync();

            return structure;
        }
    }
}
