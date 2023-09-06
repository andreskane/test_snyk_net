using ABI.API.Structure.Application.Commands.StructureClientNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

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
    public class UpdateClientNodeCommandTests
    {
        private static IStructureClientRepository _repo;
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

            _repo = new StructureClientRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod()]
        public async Task UpdateClientNodeCommandTestAsync()
        {
            var entityToUpdate = await _repo.GetById(17961);
            var command = new UpdateClientNodeCommand
            {
                ClientId = entityToUpdate.ClientId,
                NodeId = entityToUpdate.NodeId,
                Name = "TEST",
                Id = entityToUpdate.Id,
                State = entityToUpdate.ClientState,
                ValidityFrom = entityToUpdate.ValidityFrom,
                ValidityTo = entityToUpdate.ValidityTo
            };
            var handler = new UpdateClientNodeCommandHandler(_repo);

            var result = await handler.Handle(command, default);

            var newValue = await _repo.GetById(result);
            newValue.Name.Should().Be("TEST");
            newValue.ClientId.Should().Be("111813");
            newValue.NodeId.Should().Be(100036);
            newValue.ClientState.Should().Be("0");
        }

        [TestMethod()]
        public async Task UpdateClientNodeMaxLengthNameCommandTestAsync()
        {
            var entityToUpdate = await _repo.GetById(17961);
            var command = new UpdateClientNodeCommand
            {
                ClientId = entityToUpdate.ClientId,
                NodeId = entityToUpdate.NodeId,
                Name = "TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTEST",
                Id = entityToUpdate.Id,
                State = entityToUpdate.ClientState,
                ValidityFrom = entityToUpdate.ValidityFrom,
                ValidityTo = entityToUpdate.ValidityTo
            };
            var handler = new UpdateClientNodeCommandHandler(_repo);

            var result = await handler.Handle(command, default);

            var newValue = await _repo.GetById(result);
            newValue.Name.Should().Be("TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTE");
            newValue.ClientId.Should().Be("111813");
            newValue.NodeId.Should().Be(100036);
            newValue.ClientState.Should().Be("0");
        }

        [TestMethod()]
        public async Task UpdateClientNodeMoveCommandTestAsync()
        {
            var entityToUpdate = await _repo.GetById(17961);
            var command = new UpdateClientNodeCommand
            {
                ClientId = entityToUpdate.ClientId,
                NodeId = 100035,
                Name = "TEST",
                Id = entityToUpdate.Id,
                State = entityToUpdate.ClientState,
                ValidityFrom = entityToUpdate.ValidityFrom,
                ValidityTo = entityToUpdate.ValidityTo
            };
            var handler = new UpdateClientNodeCommandHandler(_repo);

            var result = await handler.Handle(command, default);

            var newValue = await _repo.GetById(result);
            newValue.Name.Should().Be("TEST");
            newValue.ClientId.Should().Be("111813");
            newValue.NodeId.Should().Be(100035);
            newValue.ClientState.Should().Be("0");
        }

        [TestMethod()]
        public async Task UpdateNoPreviousCommandTestAsync()
        {
            var entityToUpdate = await _repo.GetById(17961);
            var command = new UpdateClientNodeCommand
            {
                ClientId = entityToUpdate.ClientId,
                NodeId = 100035,
                Name = "TEST",
                Id = entityToUpdate.Id,
                State = entityToUpdate.ClientState,
                ValidityFrom = entityToUpdate.ValidityFrom,
                ValidityTo = entityToUpdate.ValidityTo
            };

            command.Id = 99999;
            var handler = new UpdateClientNodeCommandHandler(_repo);

            var result = await handler.Handle(command, default);

            var newValue = await _repo.GetById(result);
            newValue.Name.Should().Be("TEST");
            newValue.ClientId.Should().Be("111813");
            newValue.NodeId.Should().Be(100035);
            newValue.ClientState.Should().Be("0");
        }
    }
}
