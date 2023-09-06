using ABI.API.Structure.ACL.Truck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.ACL.Truck.Domain.Entities;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface IImportProcessRepository
    {
        Task SetState(int id, ImportProcessState state, CancellationToken cancellationToken);
        Task<IList<E.ImportProcess>> GetAllAsync(CancellationToken cancellationToken);
       // Task<IList<E.ImportProcess>> FilterAsync(FilterQuery filter, CancellationToken cancellationToken);
        Task BulkInsertAsync(ICollection<E.ImportProcess> entities, CancellationToken cancellationToken);
        Task DeleteAsync(int[] ids, CancellationToken cancellationToken);
        // Task EditAsync(EditCommand entity, CancellationToken cancellationToken);
        Task EditAsync(Int32 Id, String Condition, DateTime ProcessDate, CancellationToken cancellationToken);

        Task<E.ImportProcess> GetImportProcessById(int id, CancellationToken cancellationToken);
    }
}
