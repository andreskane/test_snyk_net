using ABI.API.Structure.ACL.Truck.Application.DTO;
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
    public class GetStructureComparePortalQuery : IRequest<IList<PortalStructureNodeDTO>>
    {

        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? Active { get; set; }
    }

    public class GetStructureComparePortalQueryHandler : IRequestHandler<GetStructureComparePortalQuery, IList<PortalStructureNodeDTO>>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;

        public GetStructureComparePortalQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<DTO.PortalStructureNodeDTO>> Handle(GetStructureComparePortalQuery request, CancellationToken cancellationToken)
        {
            var listNode = await _mediator.Send(new GetAllNodeByStructureIdQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            var nodes = listNode.Select(n => n.NodeId).Distinct().ToList();

            var nodesMaxVersion = (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                                   where ndn.ValidityFrom <= request.ValidityFrom && nodes.Contains(ndn.NodeId)
                                   group ndn by ndn.NodeId into g
                                   select new
                                   {
                                       NodeId = g.Key,
                                       ValidityFrom = g.Max(e => e.ValidityFrom)
                                   }).Where(n => nodes.Contains(n.NodeId)).ToList();

            var query = (from jer in listNode.AsQueryable()
                         join ndn in _context.StructureNodeDefinitions.AsNoTracking() on jer.NodeId equals ndn.NodeId
                         join ndmax in nodesMaxVersion.AsQueryable() on new { Key1 = ndn.NodeId, Key2 = ndn.ValidityFrom } equals new { Key1 = ndmax.NodeId, Key2 = ndmax.ValidityFrom }

                         select new PortalStructureNodeDTO
                         {
                             NodeId = jer.NodeId,
                             NodeName = ndn.Name,
                             NodeCode = jer.NodeCode,
                             NodeActive = ndn.Active,
                             NodeLevelId = jer.NodeLevelId,

                             NodeEmployeeId = ndn.EmployeeId,

                             NodeValidityFrom = ndn.ValidityFrom,
                             NodeValidityTo = ndn.ValidityTo,
                             NodeRoleId = ndn.RoleId,
                             NodeAttentionModeId = ndn.AttentionModeId,

                             ContainsNodeId = jer.NodeIdTo


                         }).OrderBy(s => s.NodeLevelId).ThenBy(a => a.NodeId);



            if (request.Active.HasValue)
            {
                query = (IOrderedQueryable<DTO.PortalStructureNodeDTO>)query.Where(n => n.NodeActive == request.Active.Value);
            }


            var list = query.ToList();

            // Si solo hay una config. Estructura o con nodo Pais
            if (list.Count == 0)
            {
                var node = await _mediator.Send(new GetStructureNodeRootQuery { StructureId = request.StructureId });

                if (node != null)
                    list.Add(node);
            }

            return await Task.Run(() => list);
        }
    }
}
