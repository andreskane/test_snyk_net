using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureNodeChangesDTOTests
    {
        [TestMethod()]
        public void StructureNodeChangesDTOTest()
        {
            var result = new StructureNodeChangesDTO();
            result.NodeId = 1;
            result.Description = "TEST";
            result.NewNode = true;
            result.Changes = null;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.Description.Should().Be("TEST");
            result.NewNode.Should().BeTrue();
            result.Changes.Should().BeNull();
        }
    }
}