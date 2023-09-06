using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class AttentionModeRoleTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new AttentionModeRole
            {
                AttentionModeId = null,
                RoleId = null,
                EsResponsable = true,
                AttentionMode = null,
                Role = null
            };

            result.Should().NotBeNull();
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.EsResponsable.Should().BeTrue();
            result.AttentionMode.Should().BeNull();
            result.Role.Should().BeNull();
        }
    }
}