using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck
{
    public interface IStructureAdapter
    {
        Task<ProcessDTO> StructureTruckToStructurePortal(string empId);
        Task<ProcessDTO> StructureTruckToStructurePortalJson(string json);
        Task<ProcessDTO> StructurePortalToTruckChanges(int structureId, DateTimeOffset validityFrom);
        Task<ProcessDTO> StructureTruckToStructurePortalCompare(string code);
        Task<ProcessDTO> StructureTruckToStructurePortalCompareJson();
        Task<EstructuraVersionOutput> GetStructureVersionTruckStatus(int company, string version);
        Task<EstructuraVersionOutput> GetStructureVersionTruckStatusCurrent(int company);
        Task<PendingVersionTruckDTO> GetPendingVersionTruck(int company, EstructuraVersionOutput versions);
        Task<ProcessDTO> MigrationClientsTruckToPortal(string empId);
    }
}
