using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.Structures;
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

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.Structures
{
    [TestClass()]
    public class GetMostVisitedFiltersQueryTests
    {
        private static ICacheStore _cacheStore;
        private static IMostVisitedFilterRepository _repository;
        private static IStructureRepository _structureRepository;
        private static IMapper _mapper;
        private static ICurrentUserService _currentUserService;

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
            
            var mockCurrentUser = new Mock<ICurrentUserService>();
            mockCurrentUser
                .Setup(s => s.UserId)
                .Returns(new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"));

            _currentUserService = mockCurrentUser.Object;
            _repository = new MostVisitedFilterRepository(AddDataContext._context, _cacheStore);
            _structureRepository = new StructureRepository(AddDataContext._context);
            _mapper = mappingConfig.CreateMapper();
        }

        [TestMethod()]
        public void GetMostVisitedFiltersQueryTest()
        {
            var result = new GetMostVisitedFiltersQuery { StructureCode= "ARG_VTA2"};
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetMostVisitedFiltersQueryHandlerTest()
        {
            var command = new GetMostVisitedFiltersQuery
            {
                StructureCode = "ARG_VTA2"
            };
            var handler = new GetMostVisitedFiltersQueryHandler(_repository, _currentUserService, _structureRepository, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetMostVisitedFiltersQueryHandlerNullTest()
        {
            var command = new GetMostVisitedFiltersQuery
            {
                StructureCode = "999999"
            };
            var handler = new GetMostVisitedFiltersQueryHandler(_repository, _currentUserService, _structureRepository, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().BeEmpty();
        }
    }
}
