using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Exceptions;
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
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.RequestsTray
{
    [TestClass()]
public    class GetPaginatedSearchByParametresQueryTest
    {
        private static IMapper _mapper;
        private static IChangeTrackingRepository _changeTrackingRepo;
        private static IStructureNodeRepository _structureNodeRepo;
        private static IStructureRepository _structureRepo;
        private static ICacheStore _cacheStore;

        private static IVersionedNodeRepository _versionedNodeRepository;
        private static IVersionedRepository _versionedRepository;
        private static IVersionedLogRepository _versionedLogRepository;
        private static readonly ILogger<GetPaginatedSearchByParametersQueryHandler> _logger;
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
            var model = new GetPaginatedSearchByParametersQuery();
        
            var result = new GetPaginatedSearchByParametersQuery
            {  
             externalSystems=default,
              filters = default,
               KindOfChanges = default,
                PageIndex = 1,
                 PageSize = 5,
                  portalStates = default,
                  sId = default,
                   SortDirection = "ASC",
                    SortOrder = "ASC",
                     Users = default,
                      validityFrom = default,
                       validityTo = default
            }; 
            result.Should().NotBeNull();
            result.PageSize.Should().Be(5);
            result.PageIndex.Should().Be(1);
            result.SortDirection.Should().Be("ASC");
            result.SortOrder.Should().Be("ASC");
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerTest()
        {
          
            var command = new GetPaginatedSearchByParametersQuery
            {
                
                PageIndex = 1,
                PageSize = 5,
                SortDirection = "ASC",
                SortOrder = "ASC",
                
                validityFrom = DateTime.Now.AddYears(-1),
                validityTo = DateTime.Now.AddYears(+1)
            };
            var handler = new GetPaginatedSearchByParametersQueryHandler(_changeTrackingRepo, _structureNodeRepo,  _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchQueryHandlerInvalidDateTest()
        {

            var command = new GetPaginatedSearchByParametersQuery
            {

                PageIndex = 1,
                PageSize = 5,
                SortDirection = "ASC",
                SortOrder = "ASC",

                validityFrom = DateTime.Now.AddYears(+1),
                validityTo = DateTime.Now.AddYears(-1)
            };
            var handler = new GetPaginatedSearchByParametersQueryHandler(_changeTrackingRepo, _structureNodeRepo, _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<GenericException>();
        }
    }
}
