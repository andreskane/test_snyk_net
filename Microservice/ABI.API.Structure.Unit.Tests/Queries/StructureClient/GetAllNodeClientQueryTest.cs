using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
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

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetAllNodeClientQueryTest
    {
        private static IMapper _mapper;
        private static IStructureClientRepository _repo;
        private static ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureClientRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod()]
        public void GetAllNodeClientQueryHandler()
        {
            var result = new GetAllNodeClientQueryHandler(_repo, _mapper);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetAllClientQueryTest()
        {
            var result = new GetAllNodeClientQuery { NodeIds = new List<int>(), ValidityFrom = DateTimeOffset.MinValue };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllOrderQueryHandlerTest()
        {
            var list = new List<int>
            {
                321
            };

            var command = new GetAllNodeClientQuery { NodeIds = list, ValidityFrom = DateTimeOffset.MinValue };

            var handler = new GetAllNodeClientQueryHandler(_repo, _mapper);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}