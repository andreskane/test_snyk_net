using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class AttentionModeDTOTests
    {
        [TestMethod()]
        public void AttentionModeDTOTest()
        {
            var result = new AttentionModeDTO();
            result.Id = 1;
            result.Name = "TEST";
            result.ShortName = "TST";
            result.Description = "TEST";
            result.Active = true;
            result.Erasable = false;
            result.Role = null;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.Erasable.Should().BeFalse();
            result.Role.Should().BeNull();
        }
    }
}