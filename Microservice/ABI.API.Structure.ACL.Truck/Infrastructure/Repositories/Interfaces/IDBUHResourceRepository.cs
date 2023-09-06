using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface IDBUHResourceRepository
    {
        Task<ResourceDTO> AddVacantResource(int companyId, string categoryId);
        Task<IList<ResourceDTO>> GetAllResource();
    }
}
