
using ABI.API.Structure.ACL.Truck.Domain.Enums;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Net.RestClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.ACL.Truck.Domain.Entities;


namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories
{
    public class ImportProcessRepository : IImportProcessRepository
    {
        private readonly TruckACLContext _context;


        public ImportProcessRepository(TruckACLContext context) => _context = context;

        public async Task SetState(int id, ImportProcessState state, CancellationToken cancellationToken)
        {
            var entity = await _context.ImportProcessDBSet.FindAsync(id);
            entity.ProcessState = state;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<E.ImportProcess>> GetAllAsync(CancellationToken cancellationToken) =>
            await _context
            .ImportProcessDBSet
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.ProcessDate)
            .ToListAsync(cancellationToken);
        // todo: se cambio por tema de relacion de capas
        //public async Task<IList<E.ImportProcess>> FilterAsync(FilterQuery filter, CancellationToken cancellationToken)
        //{
        //    var query = _context.ImportProcessDBSet.AsNoTracking().AsQueryable().Where(x => !x.IsDeleted);

        //    query = filter.ProcessState.HasValue ? query.Where(x => x.ProcessState == filter.ProcessState) : query;
        //    query = filter.From.HasValue ? query.Where(x => filter.From.Value <= x.ProcessDate) : query;
        //    query = filter.To.HasValue ? query.Where(x => filter.To.Value.AddDays(1) >= x.ProcessDate) : query;


        //    return await query.OrderByDescending(x => x.ProcessDate).ToListAsync(cancellationToken);
        //}

        public async Task BulkInsertAsync(ICollection<E.ImportProcess> entities, CancellationToken cancellationToken)
        {
            _context.ImportProcessDBSet.AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int[] ids, CancellationToken cancellationToken)
        {
            var toDelete = await _context
                .ImportProcessDBSet
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (toDelete.Any(x => x.IsDeleted))
                throw new BadRequestException("Uno de los Id no es válido");

            foreach (var item in toDelete)
                item.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);
        }

        //public async Task EditAsync(EditCommand entity, CancellationToken cancellationToken)
        //{
        //    var dbEntity = await _context.ImportProcessDBSet.FindAsync(entity.Id);

        //    dbEntity.Condition = entity.Condition;
        //    dbEntity.ProcessDate = entity.ProcessDate;

        //    await _context.SaveChangesAsync(cancellationToken);
        //}

        // todo: se cambio por tema de relacion de capas
        public async Task EditAsync(Int32 Id, String Condition, DateTime ProcessDate, CancellationToken cancellationToken)
        {
            var dbEntity = await _context.ImportProcessDBSet.FindAsync(Id);

            dbEntity.Condition = Condition;
            dbEntity.ProcessDate = ProcessDate;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<E.ImportProcess> GetImportProcessById(int id, CancellationToken cancellationToken) =>
            await _context
            .ImportProcessDBSet
            .AsNoTracking()
            .SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id, cancellationToken);
    }
}
