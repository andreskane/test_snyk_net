using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class PortalNodeVersionDTOTests
    {
        [TestMethod()]
        public void PortalNodeVersionDTOTest()
        {
            var result = new PortalNodeVersionDTO();
            result.NodeDefinitionId = 1;
            result.Name = "TEST";
            result.Code = "TEST";
            result.LevelId = 1;
            result.Active = null;
            result.AttentionModeId = null;
            result.RoleId = null;
            result.SaleChannelId = null;
            result.EmployeeId = null;
            result.ValidityFrom = null;
            result.AttentionMode = null;
            result.Role = null;
            result.SaleChannel = null;

            result.Should().NotBeNull();
            result.NodeDefinitionId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.Active.Should().BeNull();
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.ValidityFrom.Should().BeNull();
            result.AttentionMode.Should().BeNull();
            result.Role.Should().BeNull();
            result.SaleChannel.Should().BeNull();
        }
    }
}