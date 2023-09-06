using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedProcessPendingCommandTest
    {

        private static ITruckService _truckService;
        private static IStructureNodePortalRepository _structureNodePortalRepository;
        private static IVersionedAristaRepository _versionedAristaRepository;
        private static IVersionedNodeRepository _versionedNodeRepository;
        private static IVersionedRepository _versionedRepository;
        private static IVersionedLogRepository _versionedLogRepository;

        private static readonly ICacheStore _cacheStore;



        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var contextTruck = AddDataTruckACLContext._context;
            var contextStructure = AddDataContext._context;

            _structureNodePortalRepository = new StructureNodePortalRepository(contextStructure);
            _versionedRepository = new VersionedRepository(contextTruck, _cacheStore);
            _versionedAristaRepository = new VersionedAristaRepository(contextTruck);
            _versionedNodeRepository = new VersionedNodeRepository(contextTruck);
            _versionedLogRepository = new VersionedLogRepository(contextTruck);

            _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository,
                                            _versionedNodeRepository, _versionedRepository, _versionedLogRepository);
        }

        [TestMethod]
        public void VersionedProcessPendingCommand()
        {
            var command = new VersionedProcessPendingCommand();

            command.Should().NotBeNull();

        }



        [TestMethod()]
        public async Task VersionedProcessPendingCommandHandlerTestNullAsync()
        {
            var mockMediator = new Mock<IMediator>();

            Versioned version = null;

            mockMediator.Setup(s => s.Send(It.IsAny<GetOneFirstVersionPending>(), default))
                .ReturnsAsync(version);

            var command = new VersionedProcessPendingCommand();

            var handler = new VersionedProcessPendingCommandHandler(
                mockMediator.Object,
                _truckService
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(true);
        }



        [TestMethod()]
        public async Task VersionedProcessPendingCommandHandlerTestAsync()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(s => s.Send(It.IsAny<GetOneFirstVersionPending>(), default))
                .ReturnsAsync(new Domain.Entities.Versioned
                {
                    Id = 2,
                    StructureId = 1,
                    Date = DateTimeOffset.Parse("2021-05-07T21:17:48.1381185"),
                    Version = null,
                    Validity = DateTimeOffset.Parse("2021-05-25T00:00:00.0000000-03:00"),
                    StatusId = 1,
                    User = "USER QUILMES (LAS-V)",
                    GenerateVersionDate = DateTimeOffset.Parse("2021-05-21T17:35:20.3635118"),
                });

            var command = new VersionedProcessPendingCommand();

            var handler = new VersionedProcessPendingCommandHandler(
                mockMediator.Object,
                _truckService
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(true);
        }
    }
}
