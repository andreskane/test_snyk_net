using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class ItemNodeDTOTests
    {
        [TestMethod()]
        public void ItemNodeDTOTest()
        {
            var result = new ItemNodeDTO
            {
                Id = null,
                Name = "TEST"
            };

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
        }
    }
}