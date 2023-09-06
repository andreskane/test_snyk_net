using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Service.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Service
{
    [TestClass()]
    public class TypeVendorTruckServiceTests
    {
        private static IAttentionModeRoleRepository _attentionModeRoleRepository;
        private static ITypeVendorTruckRepository _typeVendorTruckRepository;
        private static IApiTruck _apiTruck;
        private static IApiTruck _apiTruckException;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var apiTruckMock = new Mock<IApiTruck>();
            apiTruckMock
                .Setup(s => s.GetAllTypeSeller())
                .ReturnsAsync(new APIClient.Truck.Entities.TipoVendedores.TruckTypeSeller
                {
                    TypeSeller = new APIClient.Truck.Entities.TipoVendedores.TypeSeller
                    {
                        Level1 = new List<APIClient.Truck.Entities.TipoVendedores.TypeSellerLevel1>
                        {
                            new APIClient.Truck.Entities.TipoVendedores.TypeSellerLevel1
                            {
                                TpoVdrId = "28",
                                TpoVdrTxt = "TEST",
                                CatVdrId = "D"
                            }
                        }
                    }
                });
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
            _attentionModeRoleRepository = new AttentionModeRoleRepository(AddDataContext._context);
            _typeVendorTruckRepository = new TypeVendorTruckRepository(AddDataTruckACLContext._context);
            _apiTruck = apiTruckMock.Object;
        }

        [TestMethod()]
        public async Task GetTypeVendorTruckByIdWithResultTest()
        {
            var _typeVendorTruckService = new TypeVendorTruckService(_apiTruck, _typeVendorTruckRepository, _attentionModeRoleRepository);
            var results = await _typeVendorTruckService.GetTypeVendorTruckById(28);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GetTypeVendorTruckByIdWithoutResultTest()
        {
            var _typeVendorTruckService = new TypeVendorTruckService(_apiTruck, _typeVendorTruckRepository, _attentionModeRoleRepository);
            var results = await _typeVendorTruckService.GetTypeVendorTruckById(1);
            Assert.IsTrue(results.Count == 0);
        }
        [TestMethod()]
        public async Task GetExcepcionTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new TypeVendorTruckService(null, _typeVendorTruckRepository, _attentionModeRoleRepository))
            );
        }
        [TestMethod()]
        public async Task GetExcepcionTruckRepoTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new TypeVendorTruckService(_apiTruck, null, _attentionModeRoleRepository))
            );
        }
        [TestMethod()]
        public async Task GetExcepcionAttentionModeRoleRepoTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new TypeVendorTruckService(_apiTruck, _typeVendorTruckRepository, null))
            );
        }
        [TestMethod()]
        public async Task GetExcepcionSerciceTest()
        {
            var apiTruckMock = new Mock<IApiTruck>();
            apiTruckMock
                .Setup(s => s.GetAllTypeSeller())
                .ReturnsAsync(new APIClient.Truck.Entities.TipoVendedores.TruckTypeSeller
                {
                    TypeSeller = new APIClient.Truck.Entities.TipoVendedores.TypeSeller()
                });
            _apiTruckException = apiTruckMock.Object;
            var _typeVendorTruckService = new TypeVendorTruckService(_apiTruckException, _typeVendorTruckRepository, _attentionModeRoleRepository);
            await Assert.ThrowsExceptionAsync<SerciceException>(
                async () => await Task.Run(() => _typeVendorTruckService.GetTypeVendorTruckById(1))
            );
        }
    }
}
