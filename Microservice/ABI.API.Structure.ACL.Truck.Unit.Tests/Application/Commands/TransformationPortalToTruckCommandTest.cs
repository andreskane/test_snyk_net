using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TransformationPortalToTruckCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TransformationPortalToTruckCommand()
        {
            var result = new TransformationPortalToTruckCommand
            {
                VersionedId = 1,
                StructureId = 1,
                OpecpiniOut = null,
                Nodes = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.StructureId.Should().Be(1);
            result.OpecpiniOut.Should().BeNull();
            result.Nodes.Should().BeNull();

        }
    }
}
