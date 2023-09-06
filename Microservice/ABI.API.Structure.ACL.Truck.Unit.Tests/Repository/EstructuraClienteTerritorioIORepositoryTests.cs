using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class EstructuraClienteTerritorioIORepositoryTests
    {
        private static IEstructuraClienteTerritorioIORepository _repo;
        private static ICacheStore _cacheStore;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            AddDataTruckACLContext.PrepareFactoryData();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

              
            _repo = new EstructuraClienteTerritorioIORepository(AddDataTruckACLContext._context,_cacheStore);
        }

        [TestMethod]
        public async Task GetByProcessIdRetornaElementos()
        {
            var result = await _repo.GetByProcessId(1, default);
            result.Should().BeEmpty();            
        }

        [TestMethod]
        public async Task GetLastDataByCountryTest()
        {
            var result = await _repo.GetLastDataByCountry("99TST", default);
            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task GetLastDataByCountryCacheTest()
        {
            var result = await _repo.GetLastDataByCountry("99TST", default);
            result = await _repo.GetLastDataByCountry("99TST", default);
            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task DeleteRangeTest()
        {
           

            var entities = await _repo.GetLastDataByCountry("01TST", default);
            await _repo.DeleteRange(entities, default);
        }

        [TestMethod]
        public async Task DeleteRangeEmptyTest()
        {
            var entities = await _repo.GetLastDataByCountry("NULL", default);
            await _repo.DeleteRange(entities, default);
        }

        [TestMethod]
        public async Task DeleteRangeNullTest()
        {
            await _repo.DeleteRange(null, default);
        }

        [TestMethod]
        public async Task GetLastDataByCountryNoTrackingCacheTest()
        {
            var result = await _repo.GetLastDataByCountryNoTracking("99TST", default);
            result = await _repo.GetLastDataByCountryNoTracking("99TST", default);
            result.Should().NotBeEmpty();
        }
    }
}