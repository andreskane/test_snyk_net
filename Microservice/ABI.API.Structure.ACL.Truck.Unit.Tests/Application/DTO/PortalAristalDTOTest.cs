using ABI.API.Structure.ACL.Truck.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.DTO
{
    [TestClass()]
    public class PortalAristalDTOTest
    {
        [TestMethod()]
        public void PortalAristalDTOtest()
        {
            var result = new PortalAristalDTO
            {
                AristaId = null,
                StructureId = 1,
                NodeId = 1,
                NodeName = "TEST",
                NodeCode = "TEST",
                NodeActive = true,
                NodeLevelId = 1,
                NodeIdTo = null
            };

            result.Should().NotBeNull();
            result.AristaId.Should().BeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.NodeName.Should().Be("TEST");
            result.NodeCode.Should().Be("TEST");
            result.NodeActive.Should().BeTrue();
            result.NodeLevelId.Should().Be(1);
            result.NodeIdTo.Should().BeNull();
        }
    }
}
