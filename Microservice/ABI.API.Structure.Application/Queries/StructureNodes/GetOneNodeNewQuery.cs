using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetOneNodeNewQuery : IRequest<DTO.StructureNodeDTO>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
    }

    public class GetOneNodeNewQueryHandler : IRequestHandler<GetOneNodeNewQuery, DTO.StructureNodeDTO>
    {
        private readonly StructureContext _context;


        public GetOneNodeNewQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<DTO.StructureNodeDTO> Handle(GetOneNodeNewQuery request, CancellationToken cancellationToken)
        {
            var node = (from n in _context.StructureNodes.AsNoTracking().Include(n => n.Level)
                        join nd in _context.StructureNodeDefinitions.AsNoTracking().Include(n => n.AttentionMode).Include(n => n.Role).Include(n => n.SaleChannel)
                            on n.Id equals nd.NodeId
                        join a in _context.StructureAristas.AsNoTracking() on n.Id equals a.NodeIdTo
                        where (n.Id == request.NodeId && a.MotiveStateId == (int)MotiveStateNode.Draft && nd.MotiveStateId == (int)MotiveStateNode.Draft)
                        select new DTO.StructureNodeDTO
                        {
                            StructureId = request.StructureId,
                            NodeId = n.Id,
                            NodeCode = n.Code,
                            NodeName = nd.Name.ToUpper(),
                            NodeLevelId = n.LevelId,
                            NodeLevelName = n.Level.Name,
                            NodeValidityFrom = nd.ValidityFrom,
                            VersionType = "N"

                        }).FirstOrDefault();

            return await Task.Run(() => node);
        }
    }
}
