using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.RequestsTray;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.RequestsTray
{
    [TestClass()]
    public class GetPaginatedSearchByStructureQueryTests
    {
        private static IChangeTrackingRepository _changeTrackingRepo;
        private static IStructureNodeRepository _structureNodeRepo;
        private static IStructureRepository _structureRepo;
        private static IVersionedNodeRepository _versionedNodeRepository;
        private static IVersionedRepository _versionedRepository;
        private static IVersionedLogRepository _versionedLogRepository;
        private static ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            AddDataContext.PrepareFactoryData();
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);
            _changeTrackingRepo = new ChangeTrackingRepository(AddDataContext._context, default);
            _structureRepo = new StructureRepository(AddDataContext._context);
            _structureNodeRepo = new StructureNodeRepository(AddDataContext._context);
            _versionedNodeRepository = new VersionedNodeRepository(AddDataTruckACLContext._context);
            _versionedRepository = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);
            _versionedLogRepository = new VersionedLogRepository(AddDataTruckACLContext._context);
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            var command = new GetPaginatedSearchByStructureQuery
            {
                structureId = 1,
                validityFrom = Convert.ToDateTime("2020-01-01"),
                validityTo = Convert.ToDateTime("2022-01-01"),
                model = model
            };
            var handler = new GetPaginatedSearchByStructureHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerOrderTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.SortOrder = "Structure";
            var command = new GetPaginatedSearchByStructureQuery
            {
                structureId = 1,
                validityFrom = Convert.ToDateTime("2020-01-01"),
                validityTo = Convert.ToDateTime("2022-01-01"),
                model = model
            };
            var handler = new GetPaginatedSearchByStructureHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerSortTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.SortOrder = "Structure";
            model.SortDirection = "ASC";
            var command = new GetPaginatedSearchByStructureQuery
            {
                structureId = 1,
                validityFrom = Convert.ToDateTime("2020-01-01"),
                validityTo = Convert.ToDateTime("2022-01-01"),
                model = model
            };
            var handler = new GetPaginatedSearchByStructureHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerSortDescTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.SortOrder = "Structure";
            model.SortDirection = "DESC";
            var command = new GetPaginatedSearchByStructureQuery
            {
                structureId = 1,
                validityFrom = Convert.ToDateTime("2020-01-01"),
                validityTo = Convert.ToDateTime("2022-01-01"),
                model = model
            };
            var handler = new GetPaginatedSearchByStructureHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerSearchTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.SortOrder = "Structure";
            model.SortDirection = "ASC";
            model.Search = "A";
            var command = new GetPaginatedSearchByStructureQuery
            {
                structureId = 1,
                validityFrom = Convert.ToDateTime("2020-01-01"),
                validityTo = Convert.ToDateTime("2022-01-01"),
                model = model
            };
            var handler = new GetPaginatedSearchByStructureHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}
