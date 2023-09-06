using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetAllNodeByStructureIdQuery : IRequest<IList<PortalAristalDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetAllNodeByStructureIdQueryHandler : IRequestHandler<GetAllNodeByStructureIdQuery, IList<PortalAristalDTO>>
    {
        private readonly StructureContext _context;

        public GetAllNodeByStructureIdQueryHandler(IConfiguration configuration, StructureContext context)
        {
            _context = context;
        }

        public async Task<IList<PortalAristalDTO>> Handle(GetAllNodeByStructureIdQuery request, CancellationToken cancellationToken)
        {

            var dateMax = DateTimeOffset.MaxValue.ToOffset(-3); //HACER: Ojo multipaís

            //raíz y ramas de la estructura
            IQueryable<PortalAristalDTO> query1 = (from nd in _context.StructureNodes.AsNoTracking()
                                                from a in _context.StructureAristas.AsNoTracking().Where(a => nd.Id == a.NodeIdFrom).DefaultIfEmpty()
                                                from nh in _context.StructureNodes.AsNoTracking().Where(nh => a.NodeIdTo == nh.Id).DefaultIfEmpty()
                                                where a.StructureIdFrom == request.StructureId && (a.ValidityFrom <= request.ValidityFrom && a.ValidityTo <= dateMax)
                                                || (a.StructureIdFrom == request.StructureId && a.MotiveStateId == (int)MotiveStateNode.Draft)
                                                select new PortalAristalDTO
                                                {
                                                    StructureId = a.StructureIdFrom,
                                                    NodeId = nd.Id,
                                                    NodeCode = nd.Code,
                                                    NodeLevelId = nd.LevelId,
                                                    NodeIdTo = nh.Id
                                                });

            //hojas de la estructura
            IQueryable<PortalAristalDTO> query2 = (from nd in _context.StructureNodes.AsNoTracking()
                                                from a in _context.StructureAristas.AsNoTracking().Where(a => nd.Id == a.NodeIdFrom).DefaultIfEmpty()
                                                from nh in _context.StructureNodes.AsNoTracking().Where(nh => a.NodeIdTo == nh.Id).DefaultIfEmpty()
                                                where a.StructureIdFrom == request.StructureId && (a.ValidityFrom <= request.ValidityFrom && a.ValidityTo <= dateMax)
                                                && !_context.StructureAristas.AsNoTracking().Any(sp => sp.StructureIdFrom == request.StructureId &&
                                                    sp.ValidityFrom <= request.ValidityFrom && sp.ValidityTo <= dateMax && sp.NodeIdFrom == nh.Id)
                                                || (a.StructureIdFrom == request.StructureId && a.MotiveStateId == (int)MotiveStateNode.Draft
                                                && !_context.StructureAristas.AsNoTracking().Any(sp => sp.StructureIdFrom == request.StructureId
                                                    && sp.MotiveStateId == (int)MotiveStateNode.Draft && sp.NodeIdFrom == nh.Id)
                                                )
                                                select new PortalAristalDTO
                                                {
                                                    StructureId = a.StructureIdFrom,
                                                    NodeId = nh.Id,
                                                    NodeCode = nh.Code,
                                                    NodeLevelId = nh.LevelId,
                                                    NodeIdTo = null
                                                });

            var query = query1.AsEnumerable().Union(query2);

            var list = query.ToList();

            return await Task.Run(() => list);
        }
    }
}
