using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class MapeoTableTruckPortal : IMapeoTableTruckPortal
    {
        private readonly TruckACLContext _contextDB;


        public MapeoTableTruckPortal(TruckACLContext contextDB)
        {
            _contextDB = contextDB;
        }


        /// <summary>
        /// Gets all level truck portal.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LevelTruckPortal>> GetAllLevelTruckPortal()
        {
            try
            {
                return await _contextDB.LevelTruckPortals.AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla mapeo Nivel_Truck_Portal", ex);
            }
        }

        /// <summary>
        /// Gets the one level truck portal by level identifier.
        /// </summary>
        /// <param name="levelId">The level identifier.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error en obtener datos tabla mapeo Nivel_Truck_Portal, ex</exception>
        public async Task<LevelTruckPortal> GetOneLevelTruckPortalByLevelId(int levelId)
        {
            try
            {
                return await _contextDB.LevelTruckPortals.AsNoTracking().FirstOrDefaultAsync(l => l.LevelPortalId == levelId);

            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla mapeo Nivel_Truck_Portal", ex);
            }
        }

        /// <summary>
        /// Gets all business truck portal.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error en obtener datos tabla mapeo Nivel_Truck_Portal</exception>
        public async Task<List<BusinessTruckPortal>> GetAllBusinessTruckPortal()
        {
            try
            {

                return await _contextDB.BusinessTruckPortals.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla mapeo Nivel_Truck_Portal", ex);
            }
        }

        /// <summary>
        /// Gets the one business truck portal.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error en obtener datos tabla mapeo Nivel_Truck_Portal</exception>
        public async Task<BusinessTruckPortal> GetOneBusinessTruckPortal(string code)
        {
            try
            {
                var id = code.ToInt();

                return await _contextDB.BusinessTruckPortals.AsNoTracking().FirstOrDefaultAsync(b => b.BusinessCode == id.ToString());
            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla mapeo Nivel_Truck_Portal", ex);
            }
        }

        /// <summary>
        /// Gets the name of the one business truck portal by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="GenericException">Error en obtener datos tabla</exception>
        public async Task<BusinessTruckPortal> GetOneBusinessTruckPortalByName(string name)
        {
            try
            {

                return await _contextDB.BusinessTruckPortals.AsNoTracking().FirstOrDefaultAsync(b => b.Name == name.ToString());
            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla", ex);
            }
        }

        /// <summary>
        /// Gets all type vendor truck portal asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GenericException">Error en obtener datos tabla mapeo Nivel_Truck_Portal_Portal</exception>
        public async Task<IList<TypeVendorTruckPortal>> GetAllTypeVendorTruckPortal()
        {
            try
            {
                return await _contextDB.TypeVendorsTruckPortal.AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw new GenericException("Error en obtener datos tabla mapeo Nivel_Truck_Portal_Portal", ex);
            }
        }
    }
}