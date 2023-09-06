using ABI.API.Structure.Application.DTO.Extension;
using ABI.API.Structure.Application.DTO.Interfaces;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structure
{
    public class GetAllBranchByNodeIdQuery : IRequest<IStructureBranchDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }

    }

    public class GetAllBranchByNodeIdQueryHandler : IRequestHandler<GetAllBranchByNodeIdQuery, IStructureBranchDTO>
    {
        private readonly IMediator _mediator;
        private readonly StructureContext _context;

        public GetAllBranchByNodeIdQueryHandler(IMediator mediator, StructureContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IStructureBranchDTO> Handle(GetAllBranchByNodeIdQuery request, CancellationToken cancellationToken)
        {

            var aristas = await _mediator.Send(new GetAllAristaQuery { StructureId = request.StructureId, NodeId = request.NodeId, ValidityFrom = request.ValidityFrom});

            var nodesD = aristas.Select(s => s.NodeIdFrom);
            var nodesH = aristas.Select(s => s.NodeIdTo);
            var listNode = nodesD.Union(nodesH).Distinct().ToList();

            var nodesMax = (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                         where ndn.ValidityFrom <= request.ValidityFrom && (ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                         && listNode.Contains(ndn.NodeId))
                         group ndn by ndn.NodeId into g
                         select new 
                         {
                             NodeId = g.Key,
                             ValidityFrom = g.Max(e => e.ValidityFrom)
                         }).Where(n => listNode.Contains(n.NodeId)).ToList();



            var queryFrom = (from a in aristas
                         join ndn in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).AsNoTracking()
                         .Include(n => n.Role).AsNoTracking().Include(n => n.SaleChannel).AsNoTracking() on a.NodeIdFrom equals ndn.NodeId
                         join ndmax in nodesMax.AsQueryable() on new { Key1 = ndn.NodeId, Key2 = ndn.ValidityFrom } equals new { Key1 = ndmax.NodeId, Key2 = ndmax.ValidityFrom }
                         where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                         select new DTO.StructureNodeDTO
                         {
                             NodeId = a.NodeIdFrom,
                             NodeName = ndn.Name,
                             NodeCode = a.NodeFrom.Code,
                             NodeActive = ndn.Active,
                             NodeLevelId = a.NodeFrom.LevelId,

                             NodeAttentionModeId = ndn.AttentionModeId,
                             NodeAttentionModeName = ndn.AttentionModeId.HasValue ? ndn.AttentionMode.Name : null,

                             NodeRoleId = ndn.RoleId,
                             NodeRoleName = ndn.RoleId.HasValue ? ndn.Role.Name : null,

                             NodeEmployeeId = ndn.EmployeeId,

                             NodeSaleChannelId = ndn.SaleChannelId,
                             NodeSaleChannelName = ndn.SaleChannelId.HasValue ? ndn.SaleChannel.Name : null,

                             NodeValidityFrom = ndn.ValidityFrom,
                             NodeValidityTo = ndn.ValidityTo,
                             NodeMotiveStateId = ndn.MotiveStateId,

                             ContainsNodeId = a.NodeIdTo

                         }).ToList();                         

            var queryTo = (from a in aristas
                                join ndn in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).AsNoTracking()
                                .Include(n => n.Role).AsNoTracking().Include(n => n.SaleChannel).AsNoTracking() on a.NodeIdTo equals ndn.NodeId
                                join ndmax in nodesMax.AsQueryable() on new { Key1 = ndn.NodeId, Key2 = ndn.ValidityFrom } equals new { Key1 = ndmax.NodeId, Key2 = ndmax.ValidityFrom }
                                where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                                select new DTO.StructureNodeDTO
                                {
                                    NodeId = a.NodeIdTo,
                                    NodeName = ndn.Name,
                                    NodeCode = a.NodeFrom.Code,
                                    NodeActive = ndn.Active,
                                    NodeLevelId = a.NodeTo.LevelId,

                                    NodeAttentionModeId = ndn.AttentionModeId,
                                    NodeAttentionModeName = ndn.AttentionModeId.HasValue ? ndn.AttentionMode.Name : null,

                                    NodeRoleId = ndn.RoleId,
                                    NodeRoleName = ndn.RoleId.HasValue ? ndn.Role.Name : null,

                                    NodeEmployeeId = ndn.EmployeeId,

                                    NodeSaleChannelId = ndn.SaleChannelId,
                                    NodeSaleChannelName = ndn.SaleChannelId.HasValue ? ndn.SaleChannel.Name : null,

                                    NodeValidityFrom = ndn.ValidityFrom,
                                    NodeValidityTo = ndn.ValidityTo,
                                    NodeMotiveStateId = ndn.MotiveStateId
                                }).ToList();


            queryFrom.AddRange(queryTo);

            var nodeTerritoryIds = queryFrom.Where(l => l.NodeLevelId == 8).Select(s => s.NodeId).ToList();

            var clients = await _mediator.Send(new GetAllNodeClientQuery {NodeIds = nodeTerritoryIds, ValidityFrom = request.ValidityFrom });

            IStructureBranchDTO node = null;

            node = queryFrom.ToChildBranchNodesDTO(request.NodeId, null, null, clients);

            return await Task.Run(() => node);
        }
    }
}
