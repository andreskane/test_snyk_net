using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Infrastructure;
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

namespace ABI.API.Structure.Application.Queries.RequestsTray.Tests
{
    [TestClass()]
    public class GetAllPaginatedSearchQueryTests
    {
        private static IMapper _mapper;
        private static IChangeTrackingRepository _changeTrackingRepo;
        private static IStructureNodeRepository _structureNodeRepo;
        private static IStructureRepository _structureRepo;

        private static  IVersionedNodeRepository _versionedNodeRepository;
        private static  IVersionedRepository _versionedRepository;
        private static  IVersionedLogRepository _versionedLogRepository;
        private static ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
            _changeTrackingRepo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _structureRepo = new StructureRepository(AddDataContext._context);
            _structureNodeRepo = new StructureNodeRepository(AddDataContext._context);

            _versionedNodeRepository = new VersionedNodeRepository(AddDataTruckACLContext._context);
            _versionedRepository = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);
            _versionedLogRepository = new VersionedLogRepository(AddDataTruckACLContext._context);
        }


        [TestMethod()]
        public void GetAllPaginatedSearchQueryTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.Search = "a";
            model.SortDirection = "ASC";
            model.SortOrder = "ASC";
            var result = new GetAllPaginatedSearchQuery { model = model };
            result.Should().NotBeNull();
            result.model.PageSize.Should().Be(5);
            result.model.PageIndex.Should().Be(1);
            result.model.Search.Should().Be("a");
            result.model.SortDirection.Should().Be("ASC");
            result.model.SortOrder.Should().Be("ASC");
        }
                     
        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerTest()
        {
            var model = new PaginatedSearchDTO();
            model.PageIndex = 1;
            model.PageSize = 5;
            var command = new GetAllPaginatedSearchQuery { model = model };
            var handler = new GetAllPaginatedSearchQueryHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
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
            var command = new GetAllPaginatedSearchQuery { model = model };
            var handler = new GetAllPaginatedSearchQueryHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
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
            var command = new GetAllPaginatedSearchQuery { model = model };
            var handler = new GetAllPaginatedSearchQueryHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
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
            var command = new GetAllPaginatedSearchQuery { model = model };
            var handler = new GetAllPaginatedSearchQueryHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
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
            var command = new GetAllPaginatedSearchQuery { model = model };
            var handler = new GetAllPaginatedSearchQueryHandler(_changeTrackingRepo, _structureNodeRepo, _structureRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}