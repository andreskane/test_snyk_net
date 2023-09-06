using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ICategoryVendorTruckService
    {
        Task<IList<CategoryVendor>> GetAllCategoryVendorTruck();
        Task<IList<CategoryVendor>> GetCategoryVendorTruckById(string id);
    }
}
