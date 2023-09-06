using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ITruckService
    {
        Task<OpecpiniInput> SetActionNewVersion(DateTimeOffset validityFrom, string companyTruckId);
        Task<OpecpiniInput> SetActionAPR(DateTimeOffset validityFrom, string companyTruckId, string version);
        Task<OpecpiniInput> SetActionOPE(DateTimeOffset validityFrom, string companyTruckId, string version);
        Task<OpecpiniInput> SetActionRCH(DateTimeOffset validityFrom, string companyTruckId, string version);
        Task<OpecpiniInput> SetActionUPD(DateTimeOffset validityFrom, string companyTruckId, string version);
        Task<OpecpiniInput> SetActionFCH(DateTimeOffset newDate, string companyTruckId, string version);

        Task<OpecpiniInput> GetTypeProcessTruck(TypeProcessTruck process, DateTimeOffset validityFrom, string companyTruckId, string version);
        Task<IList<NodePortalTruckDTO>> GetAllNodesSentTruckAsync(DateTimeOffset validityFrom, int structureId);
        Task<IList<PortalAristalDTO>> GetAllAristasPortalAsync(DateTimeOffset validityFrom, int structureId);
        Task<EstructuraVersionInput> GetStructureVersionTruckInput(int company, string version);

        Task<Versioned> SetVersionedNew(int strictureId, DateTimeOffset validityFrom, string user);
        Task<int> SetVersionedIniVersion(OpecpiniOut ini, int strictureId, DateTimeOffset validityFrom);
        Task SetVersionedLog(int versionedId, VersionedLogState state, object objectValue);
        Task SetVersionedLog(int versionedId, VersionedLogState state, string text);
        Task SetVersionedNode(int versionedId, int nodeId, int nodeDefinitionId);
        Task SetVersionedArista(int versionedId, int aristaId);
        
    }
}
