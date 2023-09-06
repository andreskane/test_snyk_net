using ABI.API.Structure.ACL.Truck.Domain.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface IPeriodicityDaysRepository
    {
        Task<int> GetDaysCount(Periodicity periodicity);
        Task<IDictionary<Periodicity, int>> GetPeriodicityToDaysDictionary(CancellationToken cancellationToken);
    }
}
