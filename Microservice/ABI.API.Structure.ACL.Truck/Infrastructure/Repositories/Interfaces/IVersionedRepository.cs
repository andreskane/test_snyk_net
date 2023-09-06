using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.Framework.MS.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface IVersionedRepository : IGenericRepository<int, Versioned, TruckACLContext>
    {
        Task<IList<Versioned>> GetAllVersionsByValidity(DateTimeOffset validateFrom);
        Task<IList<Versioned>> GetAllVersionsPendingByValidity(DateTimeOffset validateFrom);
        Task<IList<Versioned>> GetByNroVerValidity(string versionTruck, DateTimeOffset validity);
        Task<IList<Versioned>> GetByStatuAsync(VersionedState statu);
        Task<Versioned> GetOneVersionByVerTruck(string verTruck);
        Task<IList<Versioned>> GetVersionByIdsStructureValidity(List<Int32> StructureIds, DateTimeOffset ValidateFrom, DateTimeOffset ValidateTo);
        Task<IList<Versioned>> GetVersionByIdsValidity(List<Int32>NodesIds, List<Int32> AristasIds, DateTimeOffset Validate);

    }
}
