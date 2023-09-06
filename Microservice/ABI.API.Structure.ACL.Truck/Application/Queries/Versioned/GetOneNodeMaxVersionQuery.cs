using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Versioned
{
    public class GetOneNodeMaxVersionByIdQuery : IRequest<PortalNodeMaxVersionDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
    }

    public class GetOneNodeMaxVersionByIdHandler : IRequestHandler<GetOneNodeMaxVersionByIdQuery, PortalNodeMaxVersionDTO>
    {
        private readonly StructureContext _context;

        public GetOneNodeMaxVersionByIdHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<PortalNodeMaxVersionDTO> Handle(GetOneNodeMaxVersionByIdQuery request, CancellationToken cancellationToken)
        {
            request.ValidityFrom ??= DateTimeOffset.UtcNow.Date;

            var node = await (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                        where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                            && ndn.ValidityFrom <= request.ValidityFrom
                        group ndn by ndn.NodeId into g
                        select new PortalNodeMaxVersionDTO
                        {
                            NodeId = g.Key,
                            ValidityFrom = g.Max(e => e.ValidityFrom)
                        })
                        .Where(n => n.NodeId == request.NodeId)
                        .FirstOrDefaultAsync();

            return node;

        }
    }
}
