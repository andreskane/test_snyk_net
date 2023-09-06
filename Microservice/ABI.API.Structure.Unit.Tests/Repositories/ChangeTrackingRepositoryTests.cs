using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
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

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class ChangeTrackingRepositoryTests
    {
        private static IChangeTrackingRepository _repo;
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


            _repo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll(true);
            results.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task GetAllFalseTest()
        {
            var results = await _repo.GetAll(false);
            results.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task GetByStructureTest()
        {
            var results = await _repo.GetByStructureId(1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByDatesRangeTest()
        {
            var results = await _repo.GetByDatesRange(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var result = await _repo.GetById(1, false);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdTrackingTest()
        {
            var result = await _repo.GetById(1, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var newEntity = new ChangeTracking
            {
                User = new Domain.AggregatesModel.ChangeTrackingAggregate.User
                {
                    Id = Guid.NewGuid(),
                    Name = "JOHN DOE"
                },
                CreateDate = DateTime.UtcNow,
                IdStructure = 1,
                IdObjectType = (int)ChangeTrackingObjectType.Node,
                ChangedValueNode = new Domain.AggregatesModel.ChangeTrackingAggregate.ChangeTrackingNode 
                { 
                    Field ="Name",
                    FieldName = "Nombre",
                    NewValue="TEST B",
                    OldValue = "TEST A",
                    Node = new Domain.AggregatesModel.ChangeTrackingAggregate.ItemNode 
                    {
                        Name="TEST",
                        Id = 1,
                        Code ="TEST"
                    }
                },
                IdChangeType = (int)ChangeType.Structure,
                NodePath = new Domain.AggregatesModel.ChangeTrackingAggregate.NodesPath(),
                ValidityFrom = DateTime.UtcNow
            };

            await _repo.Create(newEntity);

            newEntity.Id.Should().NotBe(0);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var result = await _repo.GetById(1);
            //result.UserName = "ERASED";

            await _repo.Update(result);

            result = await _repo.GetById(1);
            //result.UserName.Should().Be("ERASED");
        }

        [TestMethod()]
        public async Task GetAllUsersTest()
        {
            var results = await _repo.GetAllUsers();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllObjectsIdByTypeTest()
        {
            var results = await _repo.GetAllObjectsIdByType(ChangeTrackingObjectType.Node);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByOriginAndDestinationIdAndValidityTest()
        {
            _repo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            var results = await _repo.GetByOriginAndDestinationIdAndValidity(9999, 9999, Convert.ToDateTime("2021-07-01 00:00:00.0000000 -03:00"), false);
            results.Should().BeEmpty();
        }
        [TestMethod()]
        public async Task GetByOriginAndDestinationIdAndValidityTrackingTest()
        {
            _repo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            var results = await _repo.GetByOriginAndDestinationIdAndValidity(9999, 9999, Convert.ToDateTime("2021-06-30 00:00:00.0000000 -03:00"), true);
            results.Should().BeEmpty();
        }
    }
}