using ABI.API.Structure.Application.DTO.Extension;
using ABI.API.Structure.Application.DTO.Interfaces;
using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetBoundedStructureFromNodeIdQuery : IRequest<IStructureBranchDTO>
    {
        public int StructureId { get; set; }
        public string SocietyCode { get; set; }
        public int FromNodeId { get; set; }
        public int ToLevelId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetBoundedStructureFromNodeIdQueryHandler : IRequestHandler<GetBoundedStructureFromNodeIdQuery, IStructureBranchDTO>
    {
        private readonly IMediator _mediator;
        private readonly StructureContext _context;


        public GetBoundedStructureFromNodeIdQueryHandler(IMediator mediator, StructureContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        public async Task<IStructureBranchDTO> Handle(GetBoundedStructureFromNodeIdQuery request, CancellationToken cancellationToken)
        {
            var aristas = await _mediator.Send(
                new GetAllAristaQuery { 
                    StructureId = request.StructureId, NodeId = request.FromNodeId, LevelId = request.ToLevelId, ValidityFrom = request.ValidityFrom 
                });

            var nodesD = aristas.Select(s => s.NodeIdFrom);
            var nodesH = aristas.Select(s => s.NodeIdTo);
            var listNode = nodesD.Union(nodesH).Distinct().ToList();

            var nodesMax = (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                            where ndn.ValidityFrom <= request.ValidityFrom && 
                            (ndn.MotiveStateId == (int)MotiveStateNode.Confirmed && listNode.Contains(ndn.NodeId))
                            group ndn by ndn.NodeId into g
                            select new
                            {
                                NodeId = g.Key,
                                ValidityFrom = g.Max(e => e.ValidityFrom)
                            })
                            .Where(n => listNode.Contains(n.NodeId)).ToList();

            var queryFrom = (from a in aristas
                             join ndn in _context.StructureNodeDefinitions.AsNoTracking()
                                .Include(n => n.AttentionMode).AsNoTracking()
                                .Include(n => n.Role).AsNoTracking()
                                .Include(n => n.SaleChannel).AsNoTracking()
                             on a.NodeIdFrom equals ndn.NodeId
                             join ndmax in nodesMax.AsQueryable()
                                on new { Key1 = ndn.NodeId, Key2 = ndn.ValidityFrom }
                                equals new { Key1 = ndmax.NodeId, Key2 = ndmax.ValidityFrom }
                             where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                             && a.NodeFrom.LevelId <= request.ToLevelId
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

            var queryTo =   (from a in aristas
                            join ndn in _context.StructureNodeDefinitions.AsNoTracking()
                                .Include(n => n.AttentionMode).AsNoTracking()
                                .Include(n => n.Role).AsNoTracking()
                                .Include(n => n.SaleChannel).AsNoTracking() 
                            on a.NodeIdTo equals ndn.NodeId
                            join ndmax in nodesMax.AsQueryable() 
                                on new { Key1 = ndn.NodeId, Key2 = ndn.ValidityFrom } 
                                equals new { Key1 = ndmax.NodeId, Key2 = ndmax.ValidityFrom }
                            where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed 
                            && a.NodeTo.LevelId <= request.ToLevelId
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
            IStructureBranchDTO node = null;

            node = queryFrom.ToChildBranchNodesDTO(request.FromNodeId, null, null);

            return await Task.Run(() => node);
        }
    }
}
