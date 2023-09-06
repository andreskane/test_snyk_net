using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllNodeQuery : IRequest<IList<DTO.StructureNodeDTO>>
    {
        public int StructureId { get; set; }

    }

    public class GetAllNodeQueryHandler : IRequestHandler<GetAllNodeQuery, IList<DTO.StructureNodeDTO>>
    {
        private readonly StructureContext _context;
        private readonly IMediator _mediator;


        public GetAllNodeQueryHandler(StructureContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<DTO.StructureNodeDTO>> Handle(GetAllNodeQuery request, CancellationToken cancellationToken)
        {


            var nodes = (
           from a in _context.StructureAristas.AsNoTracking()
           join n in _context.StructureNodes.AsNoTracking() on a.NodeIdFrom equals n.Id
           join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
           where a.StructureIdFrom == request.StructureId
           select new DTO.StructureNodeDTO
           {
               NodeId = n.Id,
               NodeName = nd.Name,
               NodeCode = n.Code,
               NodeActive = nd.Active,
               NodeLevelName = nd.Node.Level.Name,
               NodeLevelId = n.LevelId,
               NodeDefinitionId = nd.Id,

               NodeAttentionModeId = nd.AttentionModeId,
               NodeAttentionModeName = nd.AttentionModeId.HasValue ? nd.AttentionMode.Name : null,

               NodeRoleId = nd.RoleId,
               NodeRoleName = nd.RoleId.HasValue ? nd.Role.Name : null,

               NodeEmployeeId = nd.VacantPerson.HasValue ? (nd.VacantPerson.Value ? null : nd.EmployeeId) : (int?)null,

               NodeSaleChannelId = nd.SaleChannelId,
               NodeSaleChannelName = nd.SaleChannelId.HasValue ? nd.SaleChannel.Name : null,

               NodeValidityFrom = nd.ValidityFrom,
               NodeValidityTo = nd.ValidityTo,

               NodeMotiveStateId = nd.MotiveStateId
           })
       .Union(
           from a in _context.StructureAristas.AsNoTracking()
           join n in _context.StructureNodes.AsNoTracking() on a.NodeIdTo equals n.Id
           join nd in _context.StructureNodeDefinitions.AsNoTracking() on n.Id equals nd.NodeId
           where a.StructureIdFrom == request.StructureId
           select new DTO.StructureNodeDTO
           {
               NodeId = n.Id,
               NodeName = nd.Name,
               NodeCode = n.Code,
               NodeActive = nd.Active,
               NodeLevelName = nd.Node.Level.Name,
               NodeLevelId = n.LevelId,
               NodeDefinitionId = nd.Id,

               NodeAttentionModeId = nd.AttentionModeId,
               NodeAttentionModeName = nd.AttentionModeId.HasValue ? nd.AttentionMode.Name : null,

               NodeRoleId = nd.RoleId,
               NodeRoleName = nd.RoleId.HasValue ? nd.Role.Name : null,

               NodeEmployeeId = nd.VacantPerson.HasValue ? (nd.VacantPerson.Value ? null : nd.EmployeeId) : (int?)null,

               NodeSaleChannelId = nd.SaleChannelId,
               NodeSaleChannelName = nd.SaleChannelId.HasValue ? nd.SaleChannel.Name : null,

               NodeValidityFrom = nd.ValidityFrom,
               NodeValidityTo = nd.ValidityTo,

               NodeMotiveStateId = nd.MotiveStateId
           }
       );


            var list = await nodes.ToListAsync();

            if (list.Count == 0)
            {
                var nodoroot = await _mediator.Send(new GetStructureNodeRootQuery { StructureId = request.StructureId });

                if (nodoroot != null)
                    list.Add(nodoroot);
            }

            return list;
        }
    }
}
