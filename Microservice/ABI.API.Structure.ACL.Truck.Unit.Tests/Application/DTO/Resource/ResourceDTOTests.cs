using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Resource
{
    [TestClass()]
    public class ResourceDTOTests
    {
        [TestMethod()]
        public void ResourceDTOTest()
        {
            var result = new ResourceDTO
            {
                Id = 1,
                Company = "TEST",
                Code = "TEST",
                Name = "TEST",
                Relations = null
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Company.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.Relations.Should().BeNull();
        }
    }
}