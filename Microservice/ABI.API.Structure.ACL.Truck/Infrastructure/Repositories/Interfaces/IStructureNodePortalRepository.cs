using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface IStructureNodePortalRepository 
    {
        Task<IList<NodePortalTruckDTO>> GetAllGradeChangesForTruck(int structureId, DateTimeOffset validityFrom);
        Task<IList<PortalAristalDTO>> GetAllAristasGradeChangesForTruck(int structureId, DateTimeOffset validityFrom);

        Task<IList<NodePortalTruckDTO>> GetAllChildNodeForTruck(int structureId, int nodeId, DateTimeOffset validityFrom);

        Task<NodePortalTruckDTO> GetNodoParent(int structureId, int nodeId);
        Task<StructureNodeDefinition> GetNodeDefinitionsByIdAsync(int id);
        void UpdateNodoDefinition(StructureNodeDefinition entity);
        Task SaveEntitiesAsync();
        Task<NodePortalTruckDTO> GetNodoById(int nodeId);
    }
}
