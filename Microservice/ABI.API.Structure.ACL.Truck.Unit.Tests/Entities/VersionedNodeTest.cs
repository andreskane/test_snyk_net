using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class VersionedNodeTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new VersionedNode
            {
                VersionedId = 1,
                NodeId = 1,
                NodeDefinitionId = 1,
                Versioned = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.NodeDefinitionId.Should().Be(1);
            result.Versioned.Should().BeNull();
        }
    }
}