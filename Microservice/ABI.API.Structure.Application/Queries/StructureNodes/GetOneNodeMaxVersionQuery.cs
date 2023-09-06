using ABI.API.Structure.ACL.Truck.Application.Extensions;
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
    public class GetOneNodeMaxVersionByIdQuery : IRequest<DTO.NodeMaxVersionDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset? Validity { get; set; }
    }

    public class GetOneNodeMaxVersionByIdHandler : IRequestHandler<GetOneNodeMaxVersionByIdQuery, DTO.NodeMaxVersionDTO>
    {
        private readonly StructureContext _context;

        public GetOneNodeMaxVersionByIdHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DTO.NodeMaxVersionDTO> Handle(GetOneNodeMaxVersionByIdQuery request, CancellationToken cancellationToken)
        {
            request.Validity ??= DateTimeOffset.UtcNow.Today(-3); //HACER: Ojo multipais

            var node = await (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                        where ndn.MotiveStateId == (int)MotiveStateNode.Confirmed
                            && ndn.ValidityFrom <= request.Validity
                        group ndn by ndn.NodeId into g
                        select new DTO.NodeMaxVersionDTO
                        {
                            NodeId = g.Key,
                            ValidityFrom = g.Max(e => e.ValidityFrom)
                        })
                        .Where(n => n.NodeId == request.NodeId)
                        .FirstOrDefaultAsync();

            if (node != null)
                return node;

            var nodeNew = await (from ndn in _context.StructureNodeDefinitions.AsNoTracking()
                           from a in _context.StructureAristas.AsNoTracking().Where(a => ndn.NodeId == a.NodeIdTo).DefaultIfEmpty()
                           where (ndn.MotiveStateId == (int)MotiveStateNode.Draft || ndn.MotiveStateId == (int)MotiveStateNode.Confirmed)
                            && ndn.ValidityFrom == request.Validity
                            && (a.MotiveStateId == (int)MotiveStateNode.Draft || a.MotiveStateId == (int)MotiveStateNode.Confirmed)
                                 group ndn by ndn.NodeId into g
                           select new DTO.NodeMaxVersionDTO
                           {
                               NodeId = g.Key,
                               ValidityFrom = g.Max(e => e.ValidityFrom)
                           })
                           .Where(n => n.NodeId == request.NodeId)
                           .FirstOrDefaultAsync();


            if (nodeNew != null)
                return nodeNew;

            return null;
        }
    }
}
