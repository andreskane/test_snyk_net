using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetAllNodePendingChangesWithoutSavingQuery : IRequest<IList<PortalNodePendingDTO>>
    {
        public int StructureId { get; set; }
    }

    public class GetAllNodePendingChangesWithoutSavingQueryHandler : IRequestHandler<GetAllNodePendingChangesWithoutSavingQuery, IList<PortalNodePendingDTO>>
    {

        private readonly IMediator _mediator;
        private readonly StructureContext _context;


        public GetAllNodePendingChangesWithoutSavingQueryHandler(IMediator mediator, StructureContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IList<PortalNodePendingDTO>> Handle(GetAllNodePendingChangesWithoutSavingQuery request, CancellationToken cancellationToken)
        {

            // tipo borrador hojas
            IQueryable<PortalNodePendingDTO> query1 = (from nd in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.Node)
                                                       from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdFrom).DefaultIfEmpty()
                                                       where a.StructureIdFrom == request.StructureId && nd.MotiveStateId == (int)MotiveStateNode.Draft
                                                       select new PortalNodePendingDTO
                                                       {
                                                           StructureId = request.StructureId,
                                                           NodeId = nd.NodeId,
                                                           NodeName = nd.Name,
                                                           NodeCode = nd.Node.Code,
                                                           NodeLevelId = nd.Node.LevelId,
                                                           NodeDefinitionId = nd.Id,
                                                           //TypeVersion = GetTypeVersionChangesWithoutSaving(nodeMaxVersion,nd.NodeId)
                                                       }).Distinct();

            // tipo borrador raiz
            IQueryable<PortalNodePendingDTO> query2 = (from nd in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.Node)
                                                       from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                                                       where a.StructureIdFrom == request.StructureId && nd.MotiveStateId == (int)MotiveStateNode.Draft
                                                       select new PortalNodePendingDTO
                                                       {
                                                           StructureId = request.StructureId,
                                                           NodeId = nd.NodeId,
                                                           NodeName = nd.Name,
                                                           NodeCode = nd.Node.Code,
                                                           NodeLevelId = nd.Node.LevelId,
                                                           NodeDefinitionId = nd.Id,
                                                           //TypeVersion = GetTypeVersionChangesWithoutSaving(nodeMaxVersion,nd.NodeId)
                                                       }).Distinct();

            var query = query1.Union(query2);
            var list = query.ToList();

            if (list.Count > 0)
            {
                var nodes = list.Select(n => n.NodeId).Distinct().ToList();

                var nodeMaxVersion = (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                                      where nodes.Contains(ndn.NodeId)

                                      group ndn by ndn.NodeId into g
                                      select new PortalNodeMaxVersionDTO
                                      {
                                          NodeId = g.Key,
                                          ValidityFrom = g.Max(e => e.ValidityFrom)
                                      }).ToList();

                // TODO: Mejorar esto ver por que no lo puedo agregar dentro de new NodePendingCustom
                // error: Can't process set operations after client evaluation, consider moving the operation before the last Select() call (see issue #16243)
                foreach (var item in list)
                {
                    item.TypeVersion = nodeMaxVersion.ToTypeVersion(item.NodeId);
                }
            }
            else
            { //tiene un borrardor Raiz

                var nodeRoot = await _mediator.Send(new GetOneNodeRootPendingQuery { StructureId = request.StructureId });

                if (nodeRoot != null)
                    list.Add(nodeRoot);
            }

            return await Task.Run(() => list);

        }
    }
}
