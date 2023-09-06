using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.Framework.MS.Caching;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedProcessImpactCommandTest
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
        public void VersionedProcessImpactCommand()
        {
            var result = new VersionedProcessImpactCommand
            {
                Payload = null,
                VersionedPendingPortal = null
            };

            result.Should().NotBeNull();
            result.Payload.Should().BeNull();
            result.VersionedPendingPortal.Should().BeNull();
        }

        [TestMethod()]
        public async Task VersionedProcessImpactCommandHandlerTestAsync()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(s => s.Send(It.IsAny<VersionsToUnifyCommand>(), default))
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

            mockMediator.Setup(s => s.Send(It.IsAny<GetByIdQuery>(), default))
                .ReturnsAsync(new StructureDTO
                {
                     Id = 1,
                    StructureModelId= 1,
                    Erasable = true,
                    Validity = DateTimeOffset.Parse("2021-05-08T00:00:00.0000000-03:00"),
                    Name = "ARGENTINA - TRUCK DEMO",
                    FirstNodeName = "ARGENTINA",
                    ThereAreChangesWithoutSaving = false,
                    ThereAreScheduledChanges = false

                });

            mockMediator.Setup(s => s.Send(It.IsAny<VersionedGenerateVersionCommand>(), default))
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


            mockMediator.Setup(s => s.Send(It.IsAny<GetAllNodesSentTruckQuery>(), default))
              .ReturnsAsync(new List<NodePortalTruckDTO>());


            mockMediator.Setup(s => s.Send(It.IsAny<GetAllAristasSentTruckQuery>(), default))
              .ReturnsAsync(new List<PortalAristalDTO>());

            mockMediator.Setup(s => s.Send(It.IsAny < StructureVersionTruckStatusCommand>(), default))
                .ReturnsAsync(new PendingVersionTruckDTO());

            var command = new VersionedProcessImpactCommand
            {
                VersionedPendingPortal = new Domain.Entities.Versioned(),
                Payload = new Truck.Application.TruckStep.TruckWritingPayload()
            };


            var handler = new VersionedProcessImpactCommandHandler(
                mockMediator.Object,
                _truckService
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(true);
        }
    }
}
