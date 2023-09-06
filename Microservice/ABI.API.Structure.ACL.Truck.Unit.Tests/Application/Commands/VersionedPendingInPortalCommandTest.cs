using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedPendingInPortalCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod]
        public void VersionedPendingInPortalCommand()
        {
            var result = new VersionedPendingInPortalCommand
            {
                VersionedId = 1,
                PendingTruck = null,
                CompanyTruck = "TEST",
                PlayLoad = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.PendingTruck.Should().BeNull();
            result.CompanyTruck.Should().Be("TEST");
            result.PlayLoad.Should().BeNull();
        }


        [TestMethod]
        public async Task VersionedPendingInPortalCommandHandlerAsync()
        {
            var mockMediator = new Mock<IMediator>();

            var command = new VersionedPendingInPortalCommand
            {
                VersionedId = 1,
                PendingTruck = new Truck.Application.DTO.PendingVersionTruckDTO(),
                CompanyTruck = "TEST",
                PlayLoad = new Truck.Application.TruckStep.TruckWritingPayload()
            };

            var handler = new VersionedPendingInPortalCommandHandler(
                mockMediator.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(true);
        }
    }
}
