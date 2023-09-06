using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
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
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.RequestsTray.Tests
{
    [TestClass()]
    public class GetFiltersOptionsQueryTests
    {
        private static IChangeTrackingRepository _changeTrackingRepo;
        private static IStructureNodeRepository _structureNodeDefinitionRepository;
        private static ILogger<GetFiltersOptionsQueryHandler> _logger;
        private static ICacheStore _cacheStore;
        private static IVersionedRepository _externalTray;

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
            _changeTrackingRepo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _structureNodeDefinitionRepository = new StructureNodeRepository(AddDataContext._context);

            _externalTray = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);


            _logger = null;
        }


        [TestMethod()]
        public void GetFiltersOptionsQueryTest()
        {
            var result = new GetFiltersOptionsQuery();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetFiltersOptionsQueryHandlerTest()
        {
            var command = new GetFiltersOptionsQuery();
            var handler = new GetFiltersOptionsQueryHandler(_changeTrackingRepo, _structureNodeDefinitionRepository, _externalTray, _logger);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}