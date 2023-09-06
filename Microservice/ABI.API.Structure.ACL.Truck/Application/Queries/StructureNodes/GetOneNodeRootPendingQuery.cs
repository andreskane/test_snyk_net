﻿using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetOneNodeRootPendingQuery : IRequest<PortalNodePendingDTO>
    {
        public int StructureId { get; set; }
    }

    public class GetOneNodeRootPendingQueryHandler : IRequestHandler<GetOneNodeRootPendingQuery, PortalNodePendingDTO>
    {
        private readonly StructureContext _context;

        public GetOneNodeRootPendingQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<PortalNodePendingDTO> Handle(GetOneNodeRootPendingQuery request, CancellationToken cancellationToken)
        {
            var structure = _context.Structures.AsNoTracking().Include(n => n.Node).ThenInclude(nd => nd.StructureNodoDefinitions).FirstOrDefault(s => s.Id == request.StructureId);

            if (structure != null && structure.Node != null)
            {
                var nd = structure.Node.StructureNodoDefinitions.FirstOrDefault();

                if (nd != null && nd.MotiveStateId == (int)MotiveStateNode.Draft)
                {
                    var nodeNew = new PortalNodePendingDTO
                    {
                        StructureId = request.StructureId,
                        NodeId = structure.Node.Id,
                        NodeName = nd.Name,
                        NodeCode = structure.Node.Code,
                        NodeLevelId = structure.Node.LevelId,
                        NodeDefinitionId = nd.Id,
                        TypeVersion = "N"
                    };

                    return await Task.Run(() => nodeNew);
                }
            }

            return null;
        }
    }
}
