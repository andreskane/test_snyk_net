using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Repository.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface ISaleChannelRepository : IGenericRepository<int, SaleChannel, StructureContext>
    {
        Task<SaleChannel> GetByName(string name);

        Task<IList<SaleChannel>> GetAllActive(bool active);

    }
}
