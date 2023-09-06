using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class LevelDTOTests
    {
        [TestMethod()]
        public void LevelDTOTest()
        {
            var result = new LevelDTO();
            result.Id = null;
            result.Name = "TEST";
            result.ShortName = "TEST";
            result.Description = "TEST";
            result.Active = true;
            result.Erasable = null;

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TEST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.Erasable.Should().BeNull();
        }
    }
}