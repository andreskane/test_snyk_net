using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class ItemDTOTests
    {
        [TestMethod()]
        public void ItemDTOTest()
        {
            var result = new ItemDTO();
            result.Id = null;
            result.Name = "TEST";
            result.Description = "TEST";

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.Description.Should().Be("TEST");
        }
    }
}