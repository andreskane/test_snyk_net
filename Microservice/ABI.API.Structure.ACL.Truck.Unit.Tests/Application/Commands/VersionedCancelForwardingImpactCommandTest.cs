using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedCancelForwardingImpactCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedCancelForwardingImpactCommand()
        {
            var result = new VersionedCancelForwardingImpactCommand
            {
                PlayLoad = null,
                Versioned = null,
                VersionTruck = "TEST",
                CompanyTruck = "TEST"
            };

            result.Should().NotBeNull();
            result.PlayLoad.Should().BeNull();
            result.Versioned.Should().BeNull();
            result.VersionTruck.Should().Be("TEST");
            result.CompanyTruck.Should().Be("TEST");

        }
    }
}
