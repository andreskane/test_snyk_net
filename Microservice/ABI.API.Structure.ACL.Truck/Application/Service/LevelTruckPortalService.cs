using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Service.Exceptions;
using ABI.Framework.MS.Service.Generics;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class LevelTruckPortalService : GenericService<int, LevelTruckPortal, TruckACLContext>, ILevelTruckPortalService
    {

        public LevelTruckPortalService(ILevelTruckPortalRepository repository) : base(repository) { }

        #region Operation   

        /// <summary>
        /// Adds the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        /// <exception cref="SerciceException"></exception>
        public async Task<int> Add(LevelTruckPortal entity)
        {

            try
            {
                return await (_repository as ILevelTruckPortalRepository).Create(entity);

            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }

            throw new NameExistsException();
        }

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NameExistsException"></exception>
        /// <exception cref="ABI.StructuresPortal.Service.Exceptions.SerciceException"></exception>
        public async Task Edit(LevelTruckPortal entity)
        {
            try
            {
                await base.Update(entity);
            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }

        #endregion


    }
}
