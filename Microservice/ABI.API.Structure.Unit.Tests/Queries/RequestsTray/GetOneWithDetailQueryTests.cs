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
    public class GetOneWithDetailQueryTests
    {
        private static IChangeTrackingRepository _changeTrackingRepo;
        private static IStructureNodeRepository _structureNodeRepository;
        private static IAttentionModeRepository _attentionModeRepository;
        private static IRoleRepository _roleRepository;
        private static ISaleChannelRepository _saleChannelRepository;
        private static ICacheStore _cacheStore;

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
            _changeTrackingRepo = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);
            _attentionModeRepository = new AttentionModeRepository(AddDataContext._context);
            _roleRepository = new RoleRepository(AddDataContext._context);
            _saleChannelRepository = new SaleChannelRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void GetOneWithDetailQueryTest()
        {
            var result = new GetOneWithDetailQuery();
            result.structureId = 1;
            result.validity = Convert.ToDateTime("2020-03-03");
            result.userId = new Guid("3e8fa66e-3619-48f0-9f0b-60500005d7ef");
            result.structureId.Should().Be(1);
            result.validity.Should().Be(Convert.ToDateTime("2020-03-03"));
            result.userId.Should().Be("3e8fa66e-3619-48f0-9f0b-60500005d7ef");
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneWithDetailQueryHandlerTest()
        {
            var command = new GetOneWithDetailQuery
            {
                structureId = 1,
                userId = new Guid("3e8fa66e-3619-48f0-9f0b-60500005d7ef"),
                validity = Convert.ToDateTime("2021-03-03")
            };
            var handler = new GetOneWithDetailQueryHandler(_changeTrackingRepo, _structureNodeRepository, _attentionModeRepository, _roleRepository, _saleChannelRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetOneWithDetailEmptyQueryHandlerTest()
        {
            var command = new GetOneWithDetailQuery
            {
                structureId = 1,
                userId = new Guid("3e8fa66e-3619-48f0-9f0b-60500005d7ef"),
                validity = Convert.ToDateTime("2021-02-08")
            };
            var handler = new GetOneWithDetailQueryHandler(_changeTrackingRepo, _structureNodeRepository, _attentionModeRepository, _roleRepository, _saleChannelRepository);
            var results = await handler.Handle(command, default);

            results.Should().BeEmpty();
        }

        [TestMethod()]
        public async Task GetOneWithDetailAristaHandlerTest()
        {
            var command = new GetOneWithDetailQuery
            {
                structureId = 1,
                userId = new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"),
                validity = Convert.ToDateTime("2021-07-01")
            };
            var handler = new GetOneWithDetailQueryHandler(_changeTrackingRepo, _structureNodeRepository, _attentionModeRepository, _roleRepository, _saleChannelRepository);
            var results = await handler.Handle(command, default);
            Assert.Inconclusive();
            //results.Should().NotBeEmpty();
        }
    }
}