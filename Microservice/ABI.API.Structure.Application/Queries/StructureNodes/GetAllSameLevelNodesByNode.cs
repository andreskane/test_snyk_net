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
    public class GetAllSameLevelNodesByNode : IRequest<IList<DTO.ItemNodeDTO>>
    {
        public int NodeId { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }
    }
    public class GetAllSameLevelNodesByNodeHandler : IRequestHandler<GetAllSameLevelNodesByNode, IList<DTO.ItemNodeDTO>>
    {
        private readonly StructureContext _context;

        public GetAllSameLevelNodesByNodeHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<DTO.ItemNodeDTO>> Handle(GetAllSameLevelNodesByNode request, CancellationToken cancellationToken)
        {
            var nodeParent = await
                (from n in _context.StructureNodes.AsNoTracking()
                 join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                 from a in _context.StructureAristas.AsNoTracking().Where(a => a.NodeIdTo == nd.NodeId).DefaultIfEmpty()
                 from p in _context.StructureNodes.AsNoTracking().Where(w => w.Id == a.NodeIdFrom)
                 where
                    (n.Id == request.NodeId && nd.ValidityFrom <= request.ValidityFrom) ||
                    (n.Id == request.NodeId && nd.MotiveStateId == (int)MotiveStateNode.Draft)
                 select new
                 {
                     a.NodeIdFrom,
                     p.LevelId,
                     a.StructureIdFrom
                 }
                )
                .FirstOrDefaultAsync();

            if (nodeParent == null)
                return new List<DTO.ItemNodeDTO>();

            if (nodeParent.LevelId == 1)
            {
                var rootNodes = await
                    (from n in _context.StructureNodes.AsNoTracking()
                     join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                     join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdFrom
                     where
                        a.StructureIdFrom == nodeParent.StructureIdFrom && a.StructureIdTo == nodeParent.StructureIdFrom &&
                        a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom &&
                        n.LevelId == 1 &&
                        nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom
                     select new DTO.ItemNodeDTO
                     {
                         Id = n.Id,
                         Code = n.Code,
                         Name = nd.Name
                     }
                    )
                    .Distinct()
                    .OrderBy(o => o.Name)
                    .ToListAsync();

                return rootNodes;
            }
            else
            {
                var nodesSameLevelParent = await
                    (from n in _context.StructureNodes.AsNoTracking()
                     join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
                     join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                     where
                        a.StructureIdFrom == nodeParent.StructureIdFrom && a.StructureIdTo == nodeParent.StructureIdFrom &&
                        a.ValidityFrom <= request.ValidityFrom && a.ValidityTo >= request.ValidityFrom &&
                        n.LevelId == nodeParent.LevelId &&
                        nd.ValidityFrom <= request.ValidityFrom && nd.ValidityTo >= request.ValidityFrom
                     select new DTO.ItemNodeDTO
                     {
                         Id = n.Id,
                         Code = n.Code,
                         Name = nd.Name
                     })
                     .Distinct()
                     .OrderBy(o => o.Name)
                     .ToListAsync();

                return nodesSameLevelParent;
            }
        }
    }
}
