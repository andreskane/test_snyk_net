using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structure
{
    public class GetAllByCodeQuery : IRequest<StructureDomain>
    {
        public string Code { get; set; }

    }

    public class GetAllByCodeQueryHandler : IRequestHandler<GetAllByCodeQuery, StructureDomain>
    {
        private readonly StructureContext _context;

        public GetAllByCodeQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<StructureDomain> Handle(GetAllByCodeQuery request, CancellationToken cancellationToken)
        {

            return await _context.Structures.FirstOrDefaultAsync(s => s.Code == request.Code);
            
        }

    }
}
