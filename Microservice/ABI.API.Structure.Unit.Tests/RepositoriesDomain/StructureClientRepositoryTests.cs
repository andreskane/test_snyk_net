using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

using FluentAssertions;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.RepositoriesDomain
{
    [TestClass()]
    public class StructureClientRepositoryTests
    {
        private static IStructureClientRepository _repo;
        private static ICacheStore _cacheStore;
        private static StructureContext _context;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            AddDataContext.PrepareFactoryData();
            _context = AddDataContext._context;
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _repo = new StructureClientRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var entity = new StructureClientNode(1, "TEST", "1", "1", DateTime.UtcNow);
            await _repo.Create(entity);
            var result = await _repo.GetById(entity.Id);
            result.Name.Should().Be("TEST");
        }
        [TestMethod]
        public async Task DeleteTest()
        {
            var entity = new StructureClientNode(1, "TEST", "1", "1", DateTime.UtcNow);
            await _repo.Create(entity);
            var delete = await _repo.GetById(entity.Id);
            await _repo.Delete(delete);
            var result = await _repo.GetById(entity.Id);
            result.Should().BeNull();
        }
        [TestMethod]
        public async Task GetAllByStructureIdTest()
        {
            var result = await _repo.GetAllByStructureId(13, Convert.ToDateTime("2021-05-29"), default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task GetAllByStructureIdCacheTest()
        {
            var result = await _repo.GetAllByStructureId(13, Convert.ToDateTime("2021-05-29"), default);
            result = await _repo.GetAllByStructureId(13, Convert.ToDateTime("2021-05-29"), default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task GetAllCurrentByStructureIdTest()
        {
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task GetAllCurrentByStructureIdCacheTest()
        {
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task CreateRangeTest()
        {
            var listCreate = new List<StructureClientNode>
            {
                new StructureClientNode(100043, "TEST2", "1", "1", DateTime.UtcNow),
                new StructureClientNode(100043, "TEST2", "1", "1", DateTime.UtcNow)
            };
            await _repo
                .Invoking(async (i) => await i.CreateRange(listCreate, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod]
        public async Task CreateRangeNullTest()
        {
            await _repo.CreateRange(null, default);
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task CreateRangeEmptyTest()
        {
            var listCreate = new List<StructureClientNode>();
            await _repo.CreateRange(listCreate, default);
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task UpdateRangeTest()
        {
            var existing = await _repo.GetAllCurrentByStructureId(13, default);
            var listToUpdate = new List<StructureClientNode>();
            foreach (var item in existing)
            {
                item.EditValidityFrom(DateTime.UtcNow);
                listToUpdate.Add(item);
            }
            await _repo
                .Invoking(async (i) => await i.UpdateRange(listToUpdate, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod]
        public async Task UpdateRangeNullTest()
        {
            await _repo
                .Invoking(async (i) => await i.UpdateRange(null, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod]
        public async Task UpdateRangeEmptyTest()
        {
            var listToUpdate = new List<StructureClientNode>();
            await _repo
                .Invoking(async (i) => await i.UpdateRange(listToUpdate, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod]
        public async Task GetActivesClientsByNodeIdTest()
        {
            var result = await _repo.GetActivesClientsByNodeId(12, Convert.ToDateTime("2021-05-29"));
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task GetActivesClientsByNodeIdInactiveTest()
        {
            var result = await _repo.GetActivesClientsByNodeId(100, Convert.ToDateTime("2021-05-29"));
            result.Should().BeEmpty();
        }
        [TestMethod]
        public async Task GetClientsByNodesIdsTest()
        {
            var list = new List<int>
            {
                321
            };
            var result = await _repo.GetClientsByNodesIds(list, Convert.ToDateTime("2021-05-29"));
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task BulkInsertAsyncEmptyTest()
        {
            var listCreate = new List<StructureClientNode>();
            await _repo.BulkInsertAsync(listCreate, default);
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
        [TestMethod]
        public async Task BulkUpdateAsyncEmptyTest()
        {
            var listCreate = new List<StructureClientNode>();
            await _repo.BulkUpdateAsync(listCreate, default);
            var result = await _repo.GetAllCurrentByStructureId(13, default);
            result.Should().NotBeEmpty();
        }
    }
}
