using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedPendingVersionIsLowerPortalCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedPendingVersionIsLowerPortalCommand()
        {
            var result = new VersionedPendingVersionIsLowerPortalCommand
            {
                VersionPortal = null,
                VersionPortalPending = null,
                PendingTruck = null,
                CompanyTruck = "TEST",
                PlayLoad = null
            };

            result.Should().NotBeNull();
            result.VersionPortal.Should().BeNull();
            result.VersionPortalPending.Should().BeNull();
            result.PendingTruck.Should().BeNull();
            result.CompanyTruck.Should().Be("TEST");
            result.PlayLoad.Should().BeNull();

        }
    }
}
