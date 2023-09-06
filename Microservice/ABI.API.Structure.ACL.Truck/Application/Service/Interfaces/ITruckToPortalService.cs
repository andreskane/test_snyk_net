using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ITruckToPortalService
    {
        Task<List<StructureNode>> GetNodesByLevelIdAsync(int structureId, int levelId);
        Task<List<StructureNode>> GetNodesTerritoryAsync(int structureId);
        Task<int> MigrateEstructureAsync(StructurePortalDTO portal, string name);
        Task MigrateEstructureClientsAsync(IList<DataIODto> clients, DateTimeOffset date, List<StructureNode> territorys);
        Task<int> SaveCompare(int structureId, List<NodePortalCompareDTO> nodes);
    }
}
