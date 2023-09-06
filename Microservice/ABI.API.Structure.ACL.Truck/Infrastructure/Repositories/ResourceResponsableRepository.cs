using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.EFBulkExtensions;
using ABI.Framework.MS.Repository.Generics;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class ResourceResponsableRepository: IResourceResponsableRepository
    {
        private readonly TruckACLContext _context;

        private IQueryable<ResourceResponsable> UntrackedSet() => _context.ResourcesResponsable.AsNoTracking();
        public ResourceResponsableRepository(TruckACLContext context) => _context = context;

        public async Task<IList<ResourceResponsable>> GetAll(CancellationToken cancellationToken = default) => 
            await UntrackedSet().ToListAsync(cancellationToken);

        public async Task<ResourceResponsable> GetById(int resourceId, CancellationToken cancellationToken = default) =>
            await _context.ResourcesResponsable.Where(x =>x.ResourceId == resourceId).FirstOrDefaultAsync(cancellationToken);

        public async Task Create(ResourceResponsable entity, CancellationToken cancellationToken = default)
        {
            _context.ResourcesResponsable.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(ResourceResponsable entity, CancellationToken cancellationToken = default)
        {
            _context.ResourcesResponsable.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ResourceResponsable entity, CancellationToken cancellationToken = default)
        {
            _context.ResourcesResponsable.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BulkInsertOrUpdateAsync(List<ResourceResponsable> newitems, CancellationToken cancellationToken = default)
        {
            await _context.BulkInsertAsync(newitems, cancellationToken: cancellationToken);
        }

        public async Task BulkDeleteteAsync(List<ResourceResponsable> newItems, CancellationToken cancellationToken = default)
        {
            await _context.BulkDeleteAsync(newItems, cancellationToken: cancellationToken);
        }
    }
}
