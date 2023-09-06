using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetNodesToCompareQuery : IRequest<IList<ChangeNodeDTO>>
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetNodesToCompareQueryHandler : IRequestHandler<GetNodesToCompareQuery, IList<ChangeNodeDTO>>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;


        public GetNodesToCompareQueryHandler(IStructureNodeRepository structureNodeRepository)
        {
            _structureNodeRepository = structureNodeRepository;
        }


        public async Task<IList<ChangeNodeDTO>> Handle(GetNodesToCompareQuery request, CancellationToken cancellationToken)
        {
            var definitions = await _structureNodeRepository.GetAllNodoDefinitionByNodeIdAsync(request.NodeId);
            var nodeMaxVersion = definitions
                .Where(w =>
                    w.MotiveStateId == (int)MotiveStateNode.Confirmed &&
                    w.ValidityFrom <= request.ValidityFrom &&
                    w.ValidityTo >= request.ValidityFrom
                )
                .Select(s => new NodeMaxVersionDTO
                {
                    NodeId = s.NodeId,
                    ValidityFrom = s.ValidityFrom
                })
                .FirstOrDefault();

            var results = await GetNodesChange(request);

            if (nodeMaxVersion != null)
                return results.Where(nd => (nd.NodeMotiveStateId == (int)MotiveStateNode.Confirmed && nd.ValidityFrom == nodeMaxVersion.ValidityFrom) ||
                            nd.NodeMotiveStateId == (int)MotiveStateNode.Draft).ToList();
            else
                return results.Where(nd => nd.NodeMotiveStateId == (int)MotiveStateNode.Draft).ToList();
        }

        private async Task<List<ChangeNodeDTO>> GetNodesChange(GetNodesToCompareQuery request)
        {
            var nodes = _structureNodeRepository.GetAllWithLevelAsyncIQueryable();
            var nodesDefinitions = _structureNodeRepository.GetAllNodeDefinitionWithIncludesIQueryable();
            var aristas = _structureNodeRepository.GetAllAristaByStructureIQueryable(request.StructureId);

            return await(
                            from n in nodes
                            join nd in nodesDefinitions
                                 on n.Id equals nd.NodeId
                            from ap in aristas
                                .Where(w =>
                                    w.NodeIdTo == n.Id
                                )
                                .DefaultIfEmpty()
                            from np in nodes
                                .Where(w => w.Id == ap.NodeIdFrom)
                                .DefaultIfEmpty()
                            from ndp in nodesDefinitions
                                .Where(w => w.NodeId == np.Id)
                                .DefaultIfEmpty()
                            where
                                n.Id == request.NodeId
                            select new ChangeNodeDTO
                            {
                                NodeId = n.Id,
                                Code = n.Code,
                                Name = nd.Name,
                                Active = nd.Active,
                                NodeDefinitionId = nd.Id,
                                LevelId = n.LevelId,
                                Level = n.Level.Name,
                                AttentionModeId = nd.AttentionModeId.HasValue ? nd.AttentionModeId.Value : null,
                                AttentionModeName = nd.AttentionModeId.HasValue ? nd.AttentionMode.Name : null,
                                RoleId = nd.RoleId.HasValue ? nd.RoleId.Value : null,
                                Role = nd.RoleId.HasValue ? nd.Role.Name : null,
                                SaleChannelId = nd.SaleChannelId.HasValue ? nd.SaleChannelId : null,
                                SaleChannel = nd.SaleChannelId.HasValue ? nd.SaleChannel.Name : null,
                                EmployeeId = nd.EmployeeId.HasValue ? nd.EmployeeId.Value : null,
                                ValidityFrom = nd.ValidityFrom,
                                ValidityTo = nd.ValidityTo,
                                NodeParentId = (np != null ? np.Id : default(int?)),
                                NodeParentCode = (np != null ? np.Code : default(string)),
                                NodeParentName = (ndp != null ? ndp.Name : default(string)),
                                NodeMotiveStateId = nd.MotiveStateId,
                                AristaMotiveStateId = (ap != null ? ap.MotiveStateId : default(int))
                            }
                        )
                        .ToListAsync();
        }
    }
}
