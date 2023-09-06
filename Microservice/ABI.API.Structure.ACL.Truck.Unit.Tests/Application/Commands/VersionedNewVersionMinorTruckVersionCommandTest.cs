using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.Framework.MS.Caching;
using Coravel.Events.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedNewVersionMinorTruckVersionCommandTest
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

        [TestMethod()]
        public void VersionedNewVersionMinorTruckVersionCommand()
        {
            var result = new VersionedNewVersionMinorTruckVersionCommand
            {
                VersionPortal = null,
                PendingTruck = null,
                CompanyTruck = "TEST",
                PlayLoad = null
            };

            result.Should().NotBeNull();
            result.VersionPortal.Should().BeNull();
            result.PendingTruck.Should().BeNull();
            result.CompanyTruck.Should().Be("TEST");
            result.PlayLoad.Should().BeNull();
        }

        [TestMethod()]
        public async Task VersionedNewVersionMinorTruckVersionCommandHandlerTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            var mockDispatcher = new Mock<IDispatcher>();

            var command = new VersionedNewVersionMinorTruckVersionCommand
            {
                CompanyTruck = "TEST",
                PendingTruck = new Truck.Application.DTO.PendingVersionTruckDTO(),
                PlayLoad = new Truck.Application.TruckStep.TruckWritingPayload(),
                VersionPortal = new Domain.Entities.Versioned {
                                                                Id = 1, 
                                                                Date = DateTimeOffset.Parse("2021-05-07T17:17:48.1381185"),
                                                                Version = "001525",
                                                                Validity = DateTimeOffset.Parse("2021-05-10T00:00:00.0000000-03:00"),
                                                                StatusId = 4,
                                                                User = "USER QUILMES (LAS-V)",
                                                                GenerateVersionDate = DateTimeOffset.Parse("2021-05-07T17:35:20.3635118"),
                }
            };


            var handler = new VersionedNewVersionMinorTruckVersionCommandHandler(
                mockMediator.Object,
                _versionedRepository,
                mockDispatcher.Object,
                _truckService
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(0);
        }


        [TestMethod()]
        public async Task VersionedNewVersionMinorTruckVersionCommandHandlerPendingTestAsync()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator
             .Setup(s => s.Send(It.IsAny<TruckOpeUPDCommand>(), default))
             .ReturnsAsync(true);

            mockMediator
            .Setup(s => s.Send(It.IsAny<TruckOpeRCHCommand> (), default))
             .ReturnsAsync(true);

            mockMediator
            .Setup(s => s.Send(It.IsAny<VersionedUpdateStateVersionCommand>(), default))
             .ReturnsAsync(true);

            mockMediator
            .Setup(s => s.Send(It.IsAny<TruckSendVersionCommand>(), default))
             .ReturnsAsync(true);

            var mockDispatcher = new Mock<IDispatcher>();

            var command = new VersionedNewVersionMinorTruckVersionCommand
            {
                CompanyTruck = "TEST",
                PendingTruck = new Truck.Application.DTO.PendingVersionTruckDTO { 
                    LastVersionDate = DateTimeOffset.Parse("2021-05-11"),
                    VersionTruck = "001556"

                },
                PlayLoad = new Truck.Application.TruckStep.TruckWritingPayload(),
                VersionPortal = new Domain.Entities.Versioned
                {
                    Id = 3,
                    Date = DateTimeOffset.Parse("2021-05-07T17:17:48.1381185"),
                    Version = null,
                    Validity = DateTimeOffset.Parse("2021-05-10T00:00:00.0000000-03:00"),
                    StatusId = 3,
                    User = "USER QUILMES (LAS-V)",
                    GenerateVersionDate = DateTimeOffset.Parse("2021-05-07T17:35:20.3635118"),
                }
            };


            var handler = new VersionedNewVersionMinorTruckVersionCommandHandler(
                mockMediator.Object,
                _versionedRepository,
                mockDispatcher.Object,
                _truckService
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(0);
        }

    }

}
