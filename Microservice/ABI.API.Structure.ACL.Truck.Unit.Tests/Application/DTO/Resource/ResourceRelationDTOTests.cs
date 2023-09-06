using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Resource
{
    [TestClass()]
    public class ResourceRelationDTOTests
    {
        [TestMethod()]
        public void ResourceRelationDTOTest()
        {
            var result = new ResourceRelationDTO
            {
                Code = "TEST",
                Name = "TEST",
                Type = 1,
                Attributes = null
            };

            result.Should().NotBeNull();
            result.Code.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.Type.Should().Be(1);
            result.Attributes.Should().BeNull();
        }
    }
}