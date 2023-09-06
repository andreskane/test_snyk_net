using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class ItemNodeDTOTests
    {
        [TestMethod()]
        public void ItemNodeDTOTest()
        {
            var result = new ItemNodeDTO();
            result.Id = null;
            result.Name = "TEST";
            result.Code = "TEST";

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
        }
    }
}