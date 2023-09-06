using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedSameDateCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedSameDateCommand()
        {
            var result = new VersionedSameDateCommand
            {
                PlayLoad = null,
                VersionedId = 1,
                PendingTruck = null,
                CompanyTruck = "TEST"
            };

            result.Should().NotBeNull();
            result.PlayLoad.Should().BeNull();
            result.VersionedId.Should().Be(1);
            result.PendingTruck.Should().BeNull();
            result.CompanyTruck.Should().Be("TEST");

        }
    }
}
