using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class TypeVendorTruckRepository : GenericRepository<int, TypeVendorTruck, TruckACLContext>, ITypeVendorTruckRepository
    {

        #region Contructor
        public TypeVendorTruckRepository(TruckACLContext context) : base(context) { }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>   
        public override async Task<IList<TypeVendorTruck>> GetAll() =>
           await UntrackedSet().ToListAsync();

        /// <summary>
        /// Gets the exists asynchronous.
        /// </summary>
        /// <param name="attentionModeRoleId">The attention mode role identifier.</param>
        /// <param name="typeVendorId">The type vendor identifier.</param>
        /// <returns></returns>
        //public async Task<bool> GetExistsAsync(int attentionModeRoleId, int typeVendorId)
        //{

        //    var entity = await UntrackedSet().FirstOrDefaultAsync(t => t.Id == attentionModeRoleId && t.TypeVendorTruckId == typeVendorId);

        //    return entity != null;
        //}


        public async Task<TypeVendorTruck> GetByAttentionModeRoleIdAsync(int attentionModeRoleId)
        {
            return await UntrackedSet().FirstOrDefaultAsync(t => t.Id == attentionModeRoleId);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="attentionModeRoleId">The attention mode role identifier.</param>
        /// <param name="typeVendorId">The type vendor identifier.</param>
        public async Task DeleteAsync(TypeVendorTruck entity)
        {
            dbContext.TypeVendorsTruck.Remove(entity);
            await dbContext.SaveChangesAsync();

        }

        #endregion
    }
}
