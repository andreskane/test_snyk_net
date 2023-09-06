using ABI.API.Structure.ACL.Truck.Application.Queries.CategoryVendor;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Queries.CategoryVendor
{
    [TestClass()]
    public class GetByVendorTruckIdQueryTests
    {

        private static ITypeVendorTruckService _service;
        private static ICategoryVendorTruckService _categoryVendorService;
        private static ICategoryVendorTruckService _categoryVendorServiceWithOutItems;
        private static ITypeVendorTruckRepository _typeVendorRepo;
        private static IAttentionModeRoleRepository _repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var apiTruckMock = new Mock<IApiTruck>();
            var apiTruckMockWithoutItems = new Mock<IApiTruck>();
            apiTruckMock
                .Setup(s => s.GetAllTypeSeller())
                .ReturnsAsync(new APIClient.Truck.Entities.TipoVendedores.TruckTypeSeller
                {
                    TypeSeller = new APIClient.Truck.Entities.TipoVendedores.TypeSeller
                    {
                        Level1 = new System.Collections.Generic.List<APIClient.Truck.Entities.TipoVendedores.TypeSellerLevel1>
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
                        Level1 = new System.Collections.Generic.List<APIClient.Truck.Entities.CategoriaVendedor.SellerCategoryLevel1>
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
                }) ;
            apiTruckMockWithoutItems
                .Setup(s => s.GetAllSellerCategory())
                .ReturnsAsync(new APIClient.Truck.Entities.CategoriaVendedor.TruckSellerCategory
                {
                    SellerCategory = new APIClient.Truck.Entities.CategoriaVendedor.SellerCategory
                    {
                        Level1 = new System.Collections.Generic.List<APIClient.Truck.Entities.CategoriaVendedor.SellerCategoryLevel1>
                        {
                            new APIClient.Truck.Entities.CategoriaVendedor.SellerCategoryLevel1
                            {
                                CatVdrEstVta = "S",
                                CatVdrTxt = "TEST",
                                CatVdrId = "E",
                                CatVdrAbv="",
                                CatVdrSts =""
                            }
                        }
                    }
                });
            _repo = new AttentionModeRoleRepository(AddDataContext._context);
            _typeVendorRepo = new TypeVendorTruckRepository(AddDataTruckACLContext._context);
            _service = new TypeVendorTruckService(apiTruckMock.Object, _typeVendorRepo, _repo);
            _categoryVendorService = new CategoryVendorTruckService(apiTruckMock.Object);
            _categoryVendorServiceWithOutItems = new CategoryVendorTruckService(apiTruckMockWithoutItems.Object);
        }

        [TestMethod()]
        public async Task GetByVendorTruckIdQueryTest()
        {
            var command = new GetByVendorTruckIdQuery { VendorTruckId = 28 };
            var handler = new GetByVendorTruckIdHandler(_service, _categoryVendorService);
            var results = await handler.Handle(command, default);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByVendorTruckIdQueryWithoutItemsTest()
        {
            var command = new GetByVendorTruckIdQuery { VendorTruckId = 1 };
            var handler = new GetByVendorTruckIdHandler(_service, _categoryVendorService);
            var results = await handler.Handle(command, default);
            results.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task GetByVendorTruckIdQueryWithoutItemsCategoryTest()
        {
            var command = new GetByVendorTruckIdQuery { VendorTruckId = 28 };
            var handler = new GetByVendorTruckIdHandler(_service, _categoryVendorServiceWithOutItems);
            var results = await handler.Handle(command, default);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByVendorTruckIdQueryWithoutServiceTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new GetByVendorTruckIdHandler(null, _categoryVendorService))
            );
        }

        [TestMethod()]
        public async Task GetByVendorTruckIdQueryWithoutCategoryServiceTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new GetByVendorTruckIdHandler(_service, null))
            );
        }
    }
}
