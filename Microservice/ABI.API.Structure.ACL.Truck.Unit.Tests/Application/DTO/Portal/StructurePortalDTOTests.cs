using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Portal
{
    [TestClass()]
    public class StructurePortalDTOTests
    {
        [TestMethod()]
        public void StructurePortalDTOTest()
        {
            var result = new StructurePortalDTO
            {
                Id = 1,
                StructureModelId = 1,
                ValidityFrom = null,
                Nodes = null
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.StructureModelId.Should().Be(1);
            result.ValidityFrom.Should().BeNull();
            result.Nodes.Should().BeNull();
        }
    }
}