using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface IEstructuraClienteTerritorioIORepository
    {
      //  Task<IList<T>> GetByProcessId<T>(int processId, CancellationToken cancellationToken) where T : class, IIOEntity;
        Task<IList<EstructuraClienteTerritorioIO>> GetByProcessId(int processId, CancellationToken cancellationToken);
        Task BulkDelete(IList<EstructuraClienteTerritorioIO> EstructuraClienteTerritorioIOs, CancellationToken cancellationToken);
        Task BulkDeleteRestProcess(int IdProcess, CancellationToken cancellationToken);
        Task<IList<EstructuraClienteTerritorioIO>> GetLastDataByCountry(string country, CancellationToken cancellationToken= default);
        Task DeleteRange(IEnumerable<EstructuraClienteTerritorioIO> entities, CancellationToken cancellationToken = default);
        Task<IList<EstructuraClienteTerritorioIO>> GetLastDataByCountryNoTracking(string country, CancellationToken cancellationToken = default);
    }
}
