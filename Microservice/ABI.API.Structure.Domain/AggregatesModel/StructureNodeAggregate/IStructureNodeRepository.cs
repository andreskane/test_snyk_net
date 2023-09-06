using ABI.Framework.MS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public interface IStructureNodeRepository : IRepositoryDomain<StructureNode>
    {

        Task<StructureNode> GetNodoOneByCodeLevelAsync(int structureId, string code, int level);
        Task<StructureNode> GetOneNodoByCodeLevelAsync(int structureId, string code, int level);
        Task<StructureNode> GetNodoOneByNodeCodeAsync(int structureId, string code, int level);
        Task<List<StructureNode>> GetNodoChildAllByNodeIdAsync(int structureId, int id);
        Task<List<StructureNode>> GetNodoChildAllConfirmByNodeIdAsync(int structureId, int id, DateTimeOffset validityFrom);

        Task<IList<StructureNodeDefinition>> GetAllNodoDefinitionByIdAsync(int id);
        StructureNodeDefinition AddNodoDefinition(StructureNodeDefinition entity);
        Task<StructureNodeDefinition> GetNodoDefinitionAsync(int nodeId, DateTimeOffset? ValidityFrom, DateTimeOffset ValidityTo);
        Task<StructureNodeDefinition> GetNodoDefinitionByIdAsync(int id, bool tracking = true);

        Task<IList<StructureNodeDefinition>> GetAllNodoDefinitionByNodeIdAsync(int nodeId);
        Task<IList<StructureNodeDefinition>> GetNodosDefinitionByIdsNodoAsync(List<Int32> ids, DateTimeOffset ValidityFrom, DateTimeOffset ValidityTo);
        Task<IList<StructureNodeDefinition>> GetNodosDefinitionByIdsNodoAsync(List<Int32> ids);

        Task<StructureNodeDefinition> GetNodoDefinitionLastCurrentAsync(int nodeId, DateTimeOffset ValidityTo);
        Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdAsync(int nodeId);
        void UpdateNodoDefinition(StructureNodeDefinition entity);
        void DeleteNodoDefinitions(StructureNodeDefinition entity);


        StructureArista AddArista(StructureArista entity);
        void DeleteArista(StructureArista entity);
        Task<StructureArista> GetAristaPrevious(int structureId, int nodeid, DateTimeOffset validity);
        void UpdateArista(StructureArista entity);
        Task<StructureArista> GetArista(int structureId, int nodeIdFrom, int nodeIdTo, DateTimeOffset validityFrom, DateTimeOffset validityTo);
        Task<StructureArista> GetAristaPendient(int structureId, int nodeIdTo, DateTimeOffset validityFrom);

        Task<bool> ContainsChildNodes(int structureId, int id);
        Task<bool> ExistsInStructures(int nodeId);

        Task<StructureNodeDefinition> GetNodoDefinitionPendingAsync(int nodeId);
        Task<IList<StructureArista>> GetAllArista();
        Task<IList<StructureNodeDefinition>> GetAllNodeDefinition();

        Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdAsync(int nodeId, DateTimeOffset validity);
        Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdNoTrackingAsync(int nodeId, DateTimeOffset validity);

        Task<StructureNodeDefinition> GetNodoDefinitionValidityByNodeIdDroppedAsync(int nodeId, DateTimeOffset validity);

        Task<List<StructureNode>> GetNodesAsync(int structureId, int levelId);
        Task<List<StructureNode>> GetNodesTerritoryAsync(int structureId);

        Task<IList<StructureArista>> GetAllAristaByStructure(int structureId);

        Task<StructureArista> GetAristaByNodeTo(int structureId, int nodeIdTo, DateTimeOffset validity, bool tracking = true);
        Task<List<StructureNodeDefinition>> GetTerritoriesByZonesEmployeeId(int structureId, int employeeId, DateTimeOffset validity);
        Task<List<StructureNodeDefinition>> GetTerritoriesByEmployeeId(int structureId, int employeeId, DateTimeOffset validity);
        Task<bool> ExistsActiveTerritoryClientByNode(int nodeId, DateTimeOffset validityNode);

        IQueryable<StructureNode> GetAllWithLevelAsyncIQueryable();
        IQueryable<StructureNodeDefinition> GetAllNodeDefinitionWithIncludesIQueryable();

        IQueryable<StructureArista> GetAllAristaByStructureIQueryable(int structureId);
        Task<IList<StructureArista>> GetAllAristaByNodeTo(int nodeIdTo);
        Task<StructureNodeDefinition> GetNodoDefinitionPreviousByNodeIdAsync(int nodeId, DateTimeOffset validityFrom);
        IQueryable<StructureArista> GetAristaByNodeFromIQueryable(int structureId, int nodeIdFrom, DateTimeOffset validity);
        Task<StructureArista> GetAristaById(int aristaId);
        Task<StructureNode> GetNodeParentByNodeId(int structureId, int nodeToId, DateTimeOffset validity);
    }
}
