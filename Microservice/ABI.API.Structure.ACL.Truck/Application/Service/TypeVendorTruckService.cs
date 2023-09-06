using ABI.API.Structure.ACL.Truck.Application.Service.ACL.Models;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Extensions;
using ABI.Framework.MS.Service.Exceptions;
using ABI.Framework.MS.Service.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class TypeVendorTruckService : GenericService<int, TypeVendorTruck, TruckACLContext>, ITypeVendorTruckService
    {

        private readonly IAttentionModeRoleRepository _attentionModeRoleRepository;
        private readonly ITypeVendorTruckRepository _typeVendorTruckRepository;
        private readonly IApiTruck _apiTruck;

        public TypeVendorTruckService(IApiTruck apiTruck, ITypeVendorTruckRepository typeVendorTruckRepository, IAttentionModeRoleRepository attentionModeRoleRepository) : base(typeVendorTruckRepository)
        {
            _attentionModeRoleRepository = attentionModeRoleRepository ?? throw new ArgumentNullException(nameof(attentionModeRoleRepository));
            _typeVendorTruckRepository = typeVendorTruckRepository ?? throw new ArgumentNullException(nameof(typeVendorTruckRepository));
            _apiTruck = apiTruck ?? throw new ArgumentNullException(nameof(apiTruck));
        }

        #region Operation   

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TypeVendorTruck>> GetAllAsync()
        {
            return await GetAll();
        }

        /// <summary>
        /// Gets all configuration.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AttentionModelRolTypeVender>> GetAllConfiguration()
        {

            var typeVendors = await GetAllTypeVendorTruck();

            var rolTypeVendors = new List<AttentionModelRolTypeVender>();

            var list = await _attentionModeRoleRepository.GetAll();
            var listTypeVendors = await _typeVendorTruckRepository.GetAll();

            foreach (var item in list)
            {
                var vender = new AttentionModelRolTypeVender
                {
                    AttentionMode = item.AttentionMode,
                    Role = item.Role,
                    AttentionModeRoleId = item.Id
                };

                var typeVender = listTypeVendors.FirstOrDefault(t => t.AttentionModeRoleId == item.Id);

                if (typeVender != null)
                {
                    vender.TypeVendorTruckId = typeVender.Id;
                    vender.AttentionModeRoleId = typeVender.AttentionModeRoleId;
                    vender.VendorTruckId = typeVender.VendorTruckId;
                    var obVender = typeVendors.FirstOrDefault(t => t.Id == typeVender.VendorTruckId);

                    if (obVender != null)
                        vender.VendorTruckName = obVender.Name;

                }

                rolTypeVendors.Add(vender);
            }

            return rolTypeVendors;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="SerciceException"></exception>
        public async Task Delete(AttentionModelRolTypeVender entity)
        {
            try
            {
                var vender = new TypeVendorTruck
                {
                    Id = entity.AttentionModeRoleId,
                    // TypeVendorTruckId = entity.TypeVendorId.Value
                };

                await (_repository as ITypeVendorTruckRepository).DeleteAsync(vender);


            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TypeVendor>> GetAllTypeVendorTruck()
        {
            try
            {
                var list = new List<TypeVendor>();


                var sellers = await _apiTruck.GetAllTypeSeller();

                foreach (var item in sellers.TypeSeller.Level1.OrderBy(l => l.TpoVdrTxt))
                {
                    list.Add(new TypeVendor(item.TpoVdrId.ToInt(), item.TpoVdrTxt.ToUpper(), item.CatVdrId.ToUpper()));
                }

                return await Task.Run(() => list);

            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Get by id
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TypeVendor>> GetTypeVendorTruckById(int id)
        {
            try
            {
                var lista = await GetAllTypeVendorTruck();

                lista = lista.Where(l => l.Id == id).ToList();

                return await Task.Run(() => lista);

            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }

        #endregion


    }
}
