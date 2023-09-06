using ABI.API.Structure.Application.Commands.StructureClientNodes;
using ABI.API.Structure.Application.Infrastructure;
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

namespace ABI.API.Structure.Unit.Tests.Commands.StructureClient
{
    [TestClass()]
    public class AddClientNodeCommandTests
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
        public async Task AddClientNodeCommandHandlerTestAsync()
        {
            var command = new AddClientNodeCommand
            {
                ClientId = "999",
                NodeId = 1,
                Name = "TEST",
                State = "1",
                ValidityFrom = DateTimeOffset.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date
            };
            var handler = new AddClientNodeCommandHandler(_repo, _mapper);

            var result = await handler.Handle(command, default);

            var newValue = await _repo.GetById(result);
            newValue.Name.Should().Be("TEST");
            newValue.ClientId.Should().Be("999");
            newValue.NodeId.Should().Be(1);
            newValue.ClientState.Should().Be("1");
            newValue.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
            newValue.ValidityTo.Should().Be(DateTimeOffset.MaxValue.Date);
        }
    }
}
