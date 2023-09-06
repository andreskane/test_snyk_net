using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

using FluentAssertions;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Repositories
{
    [TestClass()]
    public class MostVisitedFilterRepositoryTests
    {
        private static IMostVisitedFilterRepository _mostVisitedFilterRepository;
        private static ICacheStore _cacheStore;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _mostVisitedFilterRepository = new MostVisitedFilterRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var newEntity = new MostVisitedFilter
            {
                Description = "TEST",
                FilterType = 1,
                IdValue = 1,
                Quantity = 1,
            };
            newEntity.EditStructureId(1);
            newEntity.EditUser(new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"));
            await _mostVisitedFilterRepository.Create(newEntity);
            newEntity.Id.Should().NotBe(0);
        }

        [TestMethod]
        public async Task GetByUserAndStructureOrderTest()
        {
            var result = await _mostVisitedFilterRepository.GetByUserAndStructureOrder(new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2);
            result.Should().NotBeEmpty();
            result.Should().HaveCountGreaterOrEqualTo(2);
        }

        [TestMethod]
        public async Task GetByUserAndStructureOrderWithMaxTest()
        {
            var result = await _mostVisitedFilterRepository.GetByUserAndStructureOrder(new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2, 1);
            result.Should().NotBeEmpty();
            result.Should().HaveCount(1);
        }
        [TestMethod]
        public async Task GetByUserStructureAndValueTest()
        {
            var result = await _mostVisitedFilterRepository.GetByUserStructureAndValue("TEST2", 2, new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2);
            result.Should().NotBeNull();
            result.Description.Should().Be("TEST2");
        }
        [TestMethod]
        public async Task GetByUserStructureAndValueNullTest()
        {
            var result = await _mostVisitedFilterRepository.GetByUserStructureAndValue("999999", 2, new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2);
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            var entity = await _mostVisitedFilterRepository.GetByUserStructureAndValue("TEST2", 2, new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2);
            entity.AddQuantity();
            await _mostVisitedFilterRepository.Update(entity);
            var result = await _mostVisitedFilterRepository.GetByUserStructureAndValue("TEST2", 2, new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"), 2);
            result.Quantity.Should().Be(entity.Quantity);
        }
    }
}