using ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Command
{
    [TestClass()]
    public class SyncClientsIOCommandTests
    {
        private static ILogger<SyncClientsIOCommand> _logger;
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private static IPortalService _portalService;
        private static ITruckToPortalService _truckToPortalService;
        private static ISyncLogRepository _syncLogRepo;
        private static IEstructuraClienteTerritorioIORepository _clienteRepository;
        private static IStructureClientRepository _structureClientRepository;
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IMediator _mediator;
        private static ICacheStore _cacheStore;
        private static IImportProcessRepository _importProcessRepository;

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

            var loggerMock = new Mock<ILogger<SyncClientsIOCommand>>();
            _logger = loggerMock.Object;
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            var mediator = new Mock<IMediator>();
            _mediator = mediator.Object;

            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _portalService = new PortalService(_structureRepo, _nodeRepo, _mediator);
            _truckToPortalService = new TruckToPortalService(_structureRepo, _nodeRepo, _mediator);
            _syncLogRepo = new SyncLogRepository(AddDataTruckACLContext._context);
            _clienteRepository = new EstructuraClienteTerritorioIORepository(AddDataTruckACLContext._context, _cacheStore);
            _structureClientRepository = new StructureClientRepository(AddDataContext._context, _cacheStore);
            _importProcessRepository = new ImportProcessRepository(AddDataTruckACLContext._context);
        }

        [TestMethod()]
        public void SyncClientsIOCommandInitTest()
        {
            var result = new SyncClientsIOCommand { ProccessId = 1};
            result.Should().NotBeNull();
            result.ProccessId.Should().Be(1);
        }

        [TestMethod()]
        public async Task SyncClientsIOCommandHandlerInitTest()
        {
            var command = new SyncClientsIOCommand { ProccessId = 262 };
            var handler = new SyncClientsIOCommandHandler(_logger, _mapeoTableTruckPortal, _portalService, _truckToPortalService, _syncLogRepo, _clienteRepository, _structureClientRepository, _importProcessRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task SyncClientsIOCommandHandlerBoliviaTest()
        {
            var command = new SyncClientsIOCommand { ProccessId = 264 };
            var handler = new SyncClientsIOCommandHandler(_logger, _mapeoTableTruckPortal, _portalService, _truckToPortalService, _syncLogRepo, _clienteRepository, _structureClientRepository, _importProcessRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}
