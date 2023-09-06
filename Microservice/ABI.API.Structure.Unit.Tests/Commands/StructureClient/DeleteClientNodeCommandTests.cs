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
    public class DeleteClientNodeCommandTests
    {
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

            _repo = new StructureClientRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod()]
        public async Task DeleteClientNodeCommandTest()
        {
            var command = new DeleteClientNodeCommand(17960);

            var handler = new DeleteClientNodeCommandHandler(_repo);

            await handler.Handle(command, default);

            var newValue = await _repo.GetById(17960);
            newValue.Should().BeNull();
        }

        [TestMethod()]
        public async Task DeleteCommandTest()
        {
            var command = new DeleteClientNodeCommand(9999);

            var handler = new DeleteClientNodeCommandHandler(_repo);

            var result = await handler.Handle(command, default);

            MediatR.Unit unit = new MediatR.Unit();

            result.Should().Be(unit);
        }
    }
}
