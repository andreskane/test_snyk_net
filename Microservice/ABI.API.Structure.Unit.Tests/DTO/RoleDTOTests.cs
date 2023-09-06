using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RoleDTOTests
    {
        [TestMethod()]
        public void RoleDTOTest()
        {
            var result = new RoleDTO
            {
                Id = null,
                Name = "TEST",
                ShortName = "TEST",
                Active = true,
                Erasable = null,
                AttentionMode = null
            };

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.Erasable.Should().BeNull();
            result.AttentionMode.Should().BeNull();
        }
    }
}