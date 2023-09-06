using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using ABI.API.Structure.APIClient.Truck;
using ABI.Framework.MS.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class CategoryVendorTruckService : ICategoryVendorTruckService
    {
        private readonly IApiTruck _apiTruck;

        public CategoryVendorTruckService(IApiTruck apiTruck)
        {
            _apiTruck = apiTruck ?? throw new ArgumentNullException(nameof(apiTruck));
        }
        public async Task<IList<CategoryVendor>> GetAllCategoryVendorTruck()
        {
            try
            {
                var list = new List<CategoryVendor>();

                var categorySellers = await _apiTruck.GetAllSellerCategory();

                foreach (var item in categorySellers.SellerCategory.Level1)
                {
                    list.Add(new CategoryVendor(item.CatVdrId, item.CatVdrTxt, item.CatVdrAbv, item.CatVdrSts, item.CatVdrEstVta));
                }

                return await Task.Run(() => list);
            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }

        public async Task<IList<CategoryVendor>> GetCategoryVendorTruckById(string id)
        {
            try
            {
                var lista = await GetAllCategoryVendorTruck();

                lista = lista.Where(l => l.CategoryId == id).ToList();

                return await Task.Run(() => lista);
            }
            catch (Exception ex)
            {
                throw new SerciceException(ex.Message, ex);
            }
        }
    }
}
