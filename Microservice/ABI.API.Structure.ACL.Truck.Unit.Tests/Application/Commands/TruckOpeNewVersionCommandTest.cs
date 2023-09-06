using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.APIClient.Truck;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using HttpMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckOpeNewVersionCommandTest
    {

        private static ITruckService _truckService;
        private static IApiTruck _apiTruck;
        private static IApiTruckUrls _apiTruckUrls;
        private static string _responseApi;

        private static IStructureNodePortalRepository _structureNodePortalRepository;
        private static IVersionedAristaRepository _versionedAristaRepository;
        private static IVersionedNodeRepository _versionedNodeRepository;
        private static IVersionedRepository _versionedRepository;
        private static IVersionedLogRepository _versionedLogRepository;

        private static readonly ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var contextTruck = AddDataTruckACLContext._context;
            var contextStructure = AddDataContext._context;


            _apiTruckUrls = new ApiTruckUrls("http://localhost:9191/", 300000);
            _apiTruck = new ApiTruck(_apiTruckUrls);

            _structureNodePortalRepository = new StructureNodePortalRepository(contextStructure);
            _versionedRepository = new VersionedRepository(contextTruck, _cacheStore);
            _versionedAristaRepository = new VersionedAristaRepository(contextTruck);
            _versionedNodeRepository = new VersionedNodeRepository(contextTruck);
            _versionedLogRepository = new VersionedLogRepository(contextTruck);

            _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository,
                                            _versionedNodeRepository, _versionedRepository, _versionedLogRepository);

        }

        [TestMethod]
        public void CreateTruckOpeNewVersionCommandTest()
        {
            var result = new TruckOpeNewVersionCommand
            {
                VersionedId = 1,
                ValidityFrom = DateTime.MinValue
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void CreateTruckOpeNewVersionCommandHandlerTest()
        {

            var result = new TruckOpeNewVersionCommandHandler(_truckService, _apiTruck);

            result.Should().NotBeNull();
        }



        [TestMethod()]
        public async Task TruckOpeNewVersionCommandHandlerTest()
        {
            var _responseApi = "{'epeciniin':{'Empresa':'001','TipoProceso':'NEW','NroVer':'','FechaDesde':'21/07/12','FechaHasta':'99/12/31','LogSts':''}}";

            var apiTruck = HttpMockRepository.At("http://localhost:9191");
            apiTruck.Stub(x => x.Post("/opecpini")).Return(_responseApi).OK();


            var command = new TruckOpeNewVersionCommand { VersionedId = 1, ValidityFrom = DateTime.UtcNow.AddDays(2) };
            var handler = new TruckOpeNewVersionCommandHandler(_truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }

    }
}
