using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetOneStructureIdByNodeIdQuery : IRequest<StructureDomain>
    {
        public int NodeId { get; set; }
    }

    public class GetOneStructureIdByNodeIdQueryHandler : IRequestHandler<GetOneStructureIdByNodeIdQuery, StructureDomain>
    {
        private readonly StructureContext _context;

        public GetOneStructureIdByNodeIdQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<StructureDomain> Handle(GetOneStructureIdByNodeIdQuery request, CancellationToken cancellationToken)
        {
            var structureNodeFrom = await (from a in _context.StructureAristas.AsNoTracking()
                                           join e in _context.Structures.AsNoTracking().Include(m => m.StructureModel).AsNoTracking()
                                           .Include(n => n.Node).ThenInclude(d => d.StructureNodoDefinitions).AsNoTracking() on a.StructureIdFrom equals e.Id
                                           where a.NodeIdFrom == request.NodeId
                                           select e
                    ).FirstOrDefaultAsync();

            if (structureNodeFrom != null)
                return await Task.Run(() => structureNodeFrom);

            var structureNodeTo = await (from a in _context.StructureAristas.AsNoTracking()
                                         join e in _context.Structures.AsNoTracking().Include(m => m.StructureModel).AsNoTracking()
                                         .Include(n => n.Node).ThenInclude(d => d.StructureNodoDefinitions).AsNoTracking() on a.StructureIdFrom equals e.Id
                                         where a.NodeIdTo == request.NodeId
                                         select e
           ).FirstOrDefaultAsync();

            return await Task.Run(() => structureNodeTo);

        }
    }
}
