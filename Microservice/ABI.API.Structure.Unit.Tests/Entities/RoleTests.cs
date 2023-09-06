using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class RoleTests
    {
        [TestMethod()]
        public void RoleTest()
        {
            var result = new Role();
            result.Should().NotBeNull();
            result.Active.Should().BeTrue();
            result.StructureNodoDefinitions.Should().BeEmpty();
            result.AttentionModeRoles.Should().BeEmpty();
        }
    }
}