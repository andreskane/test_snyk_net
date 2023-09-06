using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces
{
    public interface IResourceResponsableRepository
    {
        Task<IList<ResourceResponsable>> GetAll(CancellationToken cancellationToken = default);
        Task<ResourceResponsable> GetById(int resourceId, CancellationToken cancellationToken = default);
        Task Create(ResourceResponsable entity, CancellationToken cancellationToken = default);
        Task Update(ResourceResponsable entity, CancellationToken cancellationToken = default);
        Task Delete(ResourceResponsable entity, CancellationToken cancellationToken = default);
        Task BulkInsertOrUpdateAsync(List<ResourceResponsable> newitems, CancellationToken cancellationToken = default);
        Task BulkDeleteteAsync(List<ResourceResponsable> newItems, CancellationToken cancellationToken = default);
    }
}
