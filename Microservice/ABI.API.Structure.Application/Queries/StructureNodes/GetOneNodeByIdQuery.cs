using ABI.API.Structure.Application.DTO;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetOneNodeByIdQuery : IRequest<DTO.NodeDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetOneNodeByIdQueryHandler : IRequestHandler<GetOneNodeByIdQuery, DTO.NodeDTO>
    {
        private readonly IMediator _mediator;

        public GetOneNodeByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Recupero los pendientes, versiones B (borrador), N (nuevo) y F (futuro), sino recupera TypeVersion vacío (nodo vigente sin cambios)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DTO.NodeDTO> Handle(GetOneNodeByIdQuery request, CancellationToken cancellationToken)
        {
            var structureChanges = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });
            var node = structureChanges.FirstOrDefault(f => f.NodeId == request.NodeId);

            if (node == null)
            {
                node = await _mediator.Send(new GetOneNodeQuery { StructureId = request.StructureId, NodeId = request.NodeId, Validity = request.ValidityFrom });
            }

            var result = new NodeDTO
            {
                Id = node.NodeId,
                StructureId = node.StructureId,
                NodeIdParent = node.NodeParentId,
                Name = node.NodeName,
                Code = node.NodeCode,
                LevelId = node.NodeLevelId,
                Active = node.NodeActive,
                AttentionModeId = node.NodeAttentionModeId,
                RoleId = node.NodeRoleId,
                SaleChannelId = node.NodeSaleChannelId,
                EmployeeId = node.NodeEmployeeId,
                IsRootNode = !node.NodeParentId.HasValue,
                ValidityFrom = node.NodeValidityFrom,
                ValidityTo = node.NodeValidityTo,
                VersionType = node.VersionType
            };

            return result;
        }
    }
}
