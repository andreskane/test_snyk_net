using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class AttentionModeTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new AttentionMode
            {
                ShortName = "TEST",
                Description = "TEST",
                Active = null,
                AttentionModeRoles = null,
                StructureNodoDefinitions = null
            };

            result.Should().NotBeNull();
            result.ShortName.Should().Be("TEST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeNull();
            result.AttentionModeRoles.Should().BeNull();
            result.StructureNodoDefinitions.Should().BeNull();
        }
    }
}