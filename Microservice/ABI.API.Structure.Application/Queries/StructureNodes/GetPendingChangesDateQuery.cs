using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetPendingChangesDateQuery : IRequest<DateTimeOffset?>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetPendingChangesDateQueryHandler : IRequestHandler<GetPendingChangesDateQuery, DateTimeOffset?>
    {
        private readonly StructureContext _context;

        public GetPendingChangesDateQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DateTimeOffset?> Handle(GetPendingChangesDateQuery request, CancellationToken cancellationToken)
        {

            var resultsND = await (from ndd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Where(w => w.MotiveStateId == (int)MotiveStateNode.Draft && w.ValidityFrom > request.ValidityFrom)
                                 select new
                                 {
                                     ndd.ValidityFrom
                                 }
                            ).ToListAsync();

            var resultsA = await (from a in _context.StructureAristas.AsNoTracking()
           .Where(w => w.MotiveStateId == (int)MotiveStateNode.Draft && w.ValidityFrom > request.ValidityFrom)
                             select new
                             {
                                 a.ValidityFrom
                             }
                    ).ToListAsync();


            var result = resultsND.Union(resultsA).Distinct().FirstOrDefault();

            if(result != null)
                return result.ValidityFrom;

            return null;
        }
    }
}
