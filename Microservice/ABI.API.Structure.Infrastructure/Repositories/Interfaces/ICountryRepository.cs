using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface ICountryRepository : IGenericRepository<int, Country, StructureContext>
    {
        Task<Country> GetByName(string name);
    }
}
