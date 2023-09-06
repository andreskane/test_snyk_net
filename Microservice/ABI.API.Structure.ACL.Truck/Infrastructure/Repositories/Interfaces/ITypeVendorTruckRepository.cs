using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.Framework.MS.Repository.Generics;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Interfaces
{
    public interface ITypeVendorTruckRepository : IGenericRepository<int, TypeVendorTruck, TruckACLContext>
    {


        Task<TypeVendorTruck> GetByAttentionModeRoleIdAsync(int attentionModeRoleId);

        Task DeleteAsync(TypeVendorTruck entity);
    }
}
