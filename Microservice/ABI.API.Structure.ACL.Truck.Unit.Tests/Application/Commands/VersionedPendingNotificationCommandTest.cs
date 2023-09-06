using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedPendingNotificationCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedPendingNotificationCommand()
        {
            var result = new VersionedPendingNotificationCommand
            {
                PlayLoad = null,
                VersionedId = 1,
                PendingTruck = null
            };

            result.Should().NotBeNull();
            result.PlayLoad.Should().BeNull();
            result.VersionedId.Should().Be(1);
            result.PendingTruck.Should().BeNull();

        }
    }
}
