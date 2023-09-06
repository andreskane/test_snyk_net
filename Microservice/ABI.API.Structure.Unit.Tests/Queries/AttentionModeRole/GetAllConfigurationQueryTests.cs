using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionModeRole.Tests
{
    [TestClass()]
    public class GetAllConfigurationQueryTests
    {
        private static IMapper _mapper;
        private static IAttentionModeRoleRepository _repo;
        private static ITypeVendorTruckService _typeVendorService;
        private static ITypeVendorTruckRepository _typeVendorRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            var apiTruckMock = new Mock<IApiTruck>();
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
                                TpoVdrId = "1",
                                TpoVdrTxt = "TEST",
                                CatVdrId = "D"
                            }
                        }
                    }
                });

            _mapper = mappingConfig.CreateMapper();
            _repo = new AttentionModeRoleRepository(AddDataContext._context);
            _typeVendorRepo = new TypeVendorTruckRepository(AddDataTruckACLContext._context);
            _typeVendorService = new TypeVendorTruckService(apiTruckMock.Object, _typeVendorRepo, _repo);
        }


        [TestMethod()]
        public void GetAllConfigurationQueryTest()
        {
            var result = new GetAllConfigurationQuery();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllConfigurationQueryHandlerTest()
        {
            var command = new GetAllConfigurationQuery();
            var handler = new GetAllConfigurationQueryHandler(_mapper, _typeVendorService);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}