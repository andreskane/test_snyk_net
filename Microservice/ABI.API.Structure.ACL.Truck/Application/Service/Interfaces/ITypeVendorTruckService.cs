using ABI.API.Structure.ACL.Truck.Application.Service.ACL.Models;
using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.Framework.MS.Service.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ITypeVendorTruckService : IGenericService<int, TypeVendorTruck, TruckACLContext>
    {
        Task<IList<TypeVendorTruck>> GetAllAsync();
        Task Delete(AttentionModelRolTypeVender entity);
        Task<IList<TypeVendor>> GetAllTypeVendorTruck();
        Task<IList<AttentionModelRolTypeVender>> GetAllConfiguration();
        Task<IList<TypeVendor>> GetTypeVendorTruckById(int id);
    }
}
