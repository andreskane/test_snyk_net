using ABI.API.Structure.ACL.Truck;
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class IntegrationTruckControllerTests
    {
        private static IntegrationTruckController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<IntegrationTruckController>>();
            var structureMock = new Mock<IStructureAdapter>();
            var mediator = new Mock<IMediator>();

            _controller = new IntegrationTruckController(structureMock.Object, loggerMock.Object, mediator.Object);
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new IntegrationTruckController(null, null, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task IntegrateTruckToPortalTest()
        {
            var result = await _controller.IntegrateTruckToPortal("1");
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<ProcessDTO>));
        }

        [TestMethod()]
        public async Task IntegrateTruckToPortalCompareTest()
        {
            var result = await _controller.IntegrateTruckToPortalCompare("1");
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<ProcessDTO>));
        }

        [TestMethod()]
        public async Task IntegrateTruckToPortalJsonTest()
        {
            var result = await _controller.IntegrateTruckToPortalJson();
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<ProcessDTO>));
        }

        [TestMethod()]
        public async Task IntegrateStructurePortalToTruckChangesTest()
        {
            var result = await _controller.IntegrateStructurePortalToTruckChanges(new TruckChangesDTO { StructureId = 1, DateChanges = DateTime.UtcNow.Date });
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<ProcessDTO>));
        }

        [TestMethod()]
        public async Task IntegrateTruckToPortalCompareJsonTest()
        {
            var result = await _controller.IntegrateTruckToPortalCompareJson();
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<ProcessDTO>));
        }

        [TestMethod()]
        public async Task StructureVersionTruckStatusTest()
        {
            var result = await _controller.StructureVersionTruckStatus(1, null);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(GenericResponse<PendingVersionTruckDTO>));
        }
    }
}