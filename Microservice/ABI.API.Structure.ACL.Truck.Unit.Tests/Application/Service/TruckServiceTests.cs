using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Application.Infrastructure;
using ABI.Framework.MS.Caching;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Service
{
    [TestClass()]
    public class TruckServiceTests
    {
        private static IStructureNodePortalRepository _structureNodePortalRepository;
        private static IVersionedAristaRepository _versionedAristaRepository;
        private static IVersionedNodeRepository _VersionedNodeRepository;
        private static IVersionedRepository _VersionedRepository;
        private static IVersionedLogRepository _VersionedLogRepository;
        private static ICacheStore _cacheStore;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _structureNodePortalRepository = new StructureNodePortalRepository(AddDataContext._context);
            _versionedAristaRepository = new VersionedAristaRepository(AddDataTruckACLContext._context);
            _VersionedNodeRepository = new VersionedNodeRepository(AddDataTruckACLContext._context);
            _VersionedRepository = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);
            _VersionedLogRepository = new VersionedLogRepository(AddDataTruckACLContext._context);
        }

        [TestMethod()]
        public async Task SetActionNewVersionTestAsync()
        {

            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(5);
            var opini = await _truckService.SetActionNewVersion(date, "001");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "NEW");
        }

        [TestMethod()]
        public async Task SetActionAPRTestAsync()
        {
            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(5);
            var opini = await _truckService.SetActionAPR(date, "001", "001309");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "APR");
        }

        [TestMethod()]
        public async Task SetActionOPETestAsync()
        {
            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(5);
            var opini = await _truckService.SetActionOPE(date, "001", "001309");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "OPE");
        }

        [TestMethod()]
        public async Task SetActionRCHTestAsync()
        {
            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(5);
            var opini = await _truckService.SetActionRCH(date, "001", "001309");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "RCH");
        }

        [TestMethod()]
        public async Task SetActionUPDTestAsync()
        {
            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(5);
            var opini = await _truckService.SetActionUPD(date, "001", "001309");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "UPD");
        }

        [TestMethod()]
        public async Task SetActionFCHTestAsync()
        {
            var _truckService = new TruckService();

            var date = DateTime.UtcNow.AddDays(10);
            var opini = await _truckService.SetActionFCH(date, "001", "001309");

            Assert.IsNotNull(opini.Epeciniin.TipoProceso == "FCH");
        }


        [TestMethod()]
        public async Task GetStructureVersionTruckInputVersionNull()
        {
            var _truckService = new TruckService();

             var opini = await _truckService.GetStructureVersionTruckInput(1, null);

            Assert.IsNotNull(opini.ECVerNro == "000000");
        }


        [TestMethod()]
        public async Task GetAllNodesSentTruckAsync()
        {
       

            var _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository, _VersionedNodeRepository, _VersionedRepository, _VersionedLogRepository);

            var date = ToOffsetTest(DateTimeOffset.UtcNow, -3);
            var result = await _truckService.GetAllNodesSentTruckAsync(date, 1);

            result.Should().NotBeNull();

        }


        [TestMethod()]
        public async Task GetAllAristasPortalAsync()
        {

            var _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository, _VersionedNodeRepository, _VersionedRepository, _VersionedLogRepository);

            var date = ToOffsetTest(DateTimeOffset.UtcNow, -3);
            var result = await _truckService.GetAllAristasPortalAsync(date, 1);

            result.Should().NotBeNull();

        }

        [TestMethod()]
        public async Task SetVersionedLogText()
        {
            var _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository, _VersionedNodeRepository, _VersionedRepository, _VersionedLogRepository);

            await _truckService.SetVersionedLog(1,VersionedLogState.IGP,"Proceso");

            var result = await _VersionedLogRepository.GetLastStateByVersionedId(1);

            result.LogStatusId.Should().Be(11);

        }

        [TestMethod()]
        public async Task SetVersionedLogObjeto()
        {
            var _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository, _VersionedNodeRepository, _VersionedRepository, _VersionedLogRepository);

            dynamic product = new JObject();
            product.ProductName = "Nuevo";
            product.Enabled = true;

            await _truckService.SetVersionedLog(1, VersionedLogState.IGP, product);

            var result = await _VersionedLogRepository.GetLastStateByVersionedId(1);

            result.LogStatusId.Should().Be(11);

        }


        //[TestMethod()]
        //public async Task SetVersionedNew()
        //{
        //    var _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository, _VersionedNodeRepository, _VersionedRepository, _VersionedLogRepository);

        //    var date = ToOffsetTest(DateTimeOffset.UtcNow, -3);
        //    var impactVersioned = await _truckService.SetVersionedNew(1, date, "USERTEST");

        //    Assert.IsNotNull(impactVersioned.User == "USERTEST");
        //}

        public DateTimeOffset ToOffsetTest(DateTimeOffset input, int offset = -3)
        {
            var zone = TimeSpan.FromHours(offset);

            return input.ToOffset(zone);
        }
    }
}
