using ABI.API.Structure.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IDBUHResourceRepository
    {
        Task<bool> CheckVacantCategory(string companyId, string categoryId);
        Task<IList<Resource>> GetAllResource();
    }
}
