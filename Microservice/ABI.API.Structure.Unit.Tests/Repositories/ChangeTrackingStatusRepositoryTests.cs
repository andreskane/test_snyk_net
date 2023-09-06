using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
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
    public class ChangeTrackingStatusRepositoryTests
    {
        private static IChangeTrackingStatusRepository _repo;

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
            _repo = new ChangeTrackingStatusRepository(AddDataContext._context, _cacheStore, default);
        }

        [TestMethod()]
        public void ChangeTrackingStatusTest()
        {
            var results = new ChangeTrackingStatus();
            results.Id = 1;
            results.IdStatus = 1;
            results.IdChangeTracking = 1;
            results.CreatedDate = Convert.ToDateTime("2020-03-29");

            results.Id.Should().Be(1);
            results.IdStatus.Should().Be(1);
            results.IdChangeTracking.Should().Be(1);
            results.CreatedDate.Should().Be(Convert.ToDateTime("2020-03-29"));

        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var newEntity = new ChangeTrackingStatus
            {
                IdStatus = (int)ChangeTrackingStatusCode.Confirmed,
                CreatedDate = DateTime.UtcNow,
                IdChangeTracking = 1
            };

            await _repo.Create(newEntity);

            newEntity.Id.Should().NotBe(0);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var entity = await _repo.GetByChangeId(10);
            entity.IdStatus = (int)ChangeTrackingStatusCode.Cancelled;
            _repo.Update(entity);
            entity = await _repo.GetByChangeId(10);
            entity.IdStatus.Should().Be(6);
        }
    }
}
