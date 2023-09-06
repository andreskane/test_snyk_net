using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.APIClient.Truck;
using ABI.Framework.MS.Service.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Service
{
    [TestClass()]
    public class CategoryVendorTruckServiceTests
    {
        private static IApiTruck _apiTruck;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var apiTruckMock = new Mock<IApiTruck>();
            apiTruckMock
                .Setup(s => s.GetAllSellerCategory())
                .ReturnsAsync(new APIClient.Truck.Entities.CategoriaVendedor.TruckSellerCategory
                {
                    SellerCategory = new APIClient.Truck.Entities.CategoriaVendedor.SellerCategory
                    {
                        Level1 = new List<APIClient.Truck.Entities.CategoriaVendedor.SellerCategoryLevel1>
                        {
                            new APIClient.Truck.Entities.CategoriaVendedor.SellerCategoryLevel1
                            {
                                CatVdrEstVta = "S",
                                CatVdrTxt = "TEST",
                                CatVdrId = "D",
                                CatVdrAbv="",
                                CatVdrSts =""
                            }
                        }
                    }
                });
            _apiTruck = apiTruckMock.Object;
        }

        [TestMethod()]
        public async Task GetAllCategoryVendorTruckTest()
        {
            var _categoryService = new CategoryVendorTruckService(_apiTruck);
            var results = await _categoryService.GetAllCategoryVendorTruck();
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GetCategoryVendorTruckByIdWithResultsTest()
        {
            var _categoryService = new CategoryVendorTruckService(_apiTruck);
            var results = await _categoryService.GetCategoryVendorTruckById("D");
            Assert.IsTrue(results.Count>0);
        }

        [TestMethod()]
        public async Task GetCategoryVendorTruckByIdWithoutResultsTest()
        {
            var _categoryService = new CategoryVendorTruckService(_apiTruck);
            var results = await _categoryService.GetCategoryVendorTruckById("A");
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod()]
        public async Task GetExcepcionTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new CategoryVendorTruckService(null))
            );
        }
        [TestMethod()]
        public async Task GetExcepcionSerciceTest()
        {
            var apiTruckMock = new Mock<IApiTruck>();
            apiTruckMock
                .Setup(s => s.GetAllSellerCategory())
                .ReturnsAsync(new APIClient.Truck.Entities.CategoriaVendedor.TruckSellerCategory
                {
                    SellerCategory = new APIClient.Truck.Entities.CategoriaVendedor.SellerCategory()
                }) ;
            _apiTruck = apiTruckMock.Object;
            var _categoryService = new CategoryVendorTruckService(_apiTruck);
            await Assert.ThrowsExceptionAsync<SerciceException>(
                async () => await Task.Run(() => _categoryService.GetCategoryVendorTruckById("A"))
            );
        }
    }
}
