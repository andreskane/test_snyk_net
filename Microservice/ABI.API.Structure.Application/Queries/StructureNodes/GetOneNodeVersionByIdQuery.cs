using ABI.API.Structure.Application.DTO.Extension;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetOneNodeVersionByIdQuery : IRequest<DTO.NodeDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetOneNodeVersionByIdQueryHandler : IRequestHandler<GetOneNodeVersionByIdQuery, DTO.NodeDTO>
    {
        private readonly IMediator _mediator;

        public GetOneNodeVersionByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DTO.NodeDTO> Handle(GetOneNodeVersionByIdQuery request, CancellationToken cancellationToken)
        {
            var resultNode = await _mediator.Send(new GetOneNodeQuery { StructureId = request.StructureId, NodeId = request.NodeId, Validity = request.ValidityFrom });

            if (resultNode == null)
                return null;

            var node = resultNode.ToNodeDTO(request.ValidityFrom);
            var version = await _mediator.Send(new GetOneNodeVersionPendingByNodeIdQuery { StructureId = request.StructureId, NodeId = request.NodeId, ValidityFrom = request.ValidityFrom });

            if (version != null && !string.IsNullOrEmpty(version.VersionType))
            {
                var nodeVersion = version.ToNodeVersionDTO();
                node.VersionType = version.VersionType;
                node.Version = nodeVersion;

                if (version.VersionType == "N")
                {
                    node.Name = null;
                    node.Code = null;
                    node.Active = null;
                    node.AttentionModeId = null;
                    node.RoleId = null;
                    node.SaleChannelId = null;
                    node.EmployeeId = null;
                    node.NodeIdParent = null;
                }
            }

            return node;
        }
    }
}
