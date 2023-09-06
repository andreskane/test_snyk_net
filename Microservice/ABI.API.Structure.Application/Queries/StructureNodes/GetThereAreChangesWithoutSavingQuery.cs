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
    public class GetThereAreChangesWithoutSavingQuery : IRequest<bool>
    {
        public int StructureId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetThereAreChangesWithoutSavingQueryHandler : IRequestHandler<GetThereAreChangesWithoutSavingQuery, bool>
    {
        private readonly StructureContext _context;

        public GetThereAreChangesWithoutSavingQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(GetThereAreChangesWithoutSavingQuery request, CancellationToken cancellationToken)
        {
            var results = await (
                from n in _context.StructureNodes.AsNoTracking()
                join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                from ndd in _context.StructureNodeDefinitions.AsNoTracking()
                    .Where(w => w.NodeId == n.Id && w.MotiveStateId == (int)MotiveStateNode.Draft)
                    .DefaultIfEmpty()
                from ad in _context.StructureAristas.AsNoTracking()
                    .Where(w => w.NodeIdTo == n.Id && w.MotiveStateId == (int)MotiveStateNode.Draft)
                    .DefaultIfEmpty()
                where
                    a.StructureIdFrom == request.StructureId && a.StructureIdTo == request.StructureId &&
                    (
                        (a.ValidityFrom > request.ValidityFrom) ||
                        (a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom)
                    ) &&
                    (ndd != null || ad != null)
                select n
            )
            .CountAsync();


            var root = (
            from s in _context.Structures.AsNoTracking()
            join nd in _context.StructureNodeDefinitions.AsNoTracking()
                .Include(i => i.Node)
                on s.RootNodeId equals nd.NodeId
            where
                s.Id == request.StructureId &&
                nd.MotiveStateId == (int)MotiveStateNode.Draft
            select nd).Count();

            if (results == 0)
                results = root;

            return results > 0;
        }
    }
}
