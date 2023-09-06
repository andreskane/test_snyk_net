using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckLoadTraysCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TruckLoadTraysCommand()
        {
            var result = new TruckLoadTraysCommand
            {
                VersionedId = 1,
                StructureTruck = null,
                OpecpiniOut = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.StructureTruck.Should().BeNull();
            result.OpecpiniOut.Should().BeNull();
        }
    }
}
