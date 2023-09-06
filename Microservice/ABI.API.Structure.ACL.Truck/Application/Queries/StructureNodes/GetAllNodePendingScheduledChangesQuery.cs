using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetAllNodePendingScheduledChangesQuery : IRequest<IList<PortalNodePendingDTO>>
    {
        public int StructureId { get; set; }
    }

    public class GetAllNodePendingScheduledChangesQueryHandler : IRequestHandler<GetAllNodePendingScheduledChangesQuery, IList<PortalNodePendingDTO>>
    {
        private readonly StructureContext _context;

        public GetAllNodePendingScheduledChangesQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<PortalNodePendingDTO>> Handle(GetAllNodePendingScheduledChangesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow.ToOffset(-3); //TODO: Ojo con multipaís

            // tipo borrador hojas
            IQueryable<PortalNodePendingDTO> query1 = (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                       join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                       from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdFrom).DefaultIfEmpty()
                                                       where a.StructureIdFrom == request.StructureId && nd.ValidityFrom > now
                                                       select new PortalNodePendingDTO
                                                       {
                                                           StructureId = a.StructureIdFrom,
                                                           NodeId = nd.NodeId,
                                                           NodeName = nd.Name,
                                                           NodeCode = n.Code,
                                                           NodeLevelId = n.LevelId,
                                                           NodeDefinitionId = nd.Id,
                                                           NodeValidityFrom = nd.ValidityFrom,
                                                           TypeVersion = "F"
                                                       }).Distinct();

            // tipo borrador raiz
            IQueryable<PortalNodePendingDTO> query2 = (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                       join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                       from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                                                       where a.StructureIdFrom == request.StructureId && nd.ValidityFrom > now
                                                       select new PortalNodePendingDTO
                                                       {
                                                           StructureId = a.StructureIdFrom,
                                                           NodeId = nd.NodeId,
                                                           NodeName = nd.Name,
                                                           NodeCode = n.Code,
                                                           NodeLevelId = n.LevelId,
                                                           NodeDefinitionId = nd.Id,
                                                           NodeValidityFrom = nd.ValidityFrom,
                                                           TypeVersion = "F"
                                                       }).Distinct();

            var query = query1.Union(query2);

            var list = query.ToList();

            return await Task.Run(() => list);
        }

    }
}
