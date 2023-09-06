using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class AttentionModeRoleConfigurationDTOTests
    {
        [TestMethod()]
        public void AttentionModeRoleConfigurationDTOTest()
        {
            var result = new AttentionModeRoleConfigurationDTO();
            result.AttentionModeRoleId = 1;
            result.AttentionModeId = 1;
            result.AttentionModeName = "TEST";
            result.RoleId = 1;
            result.RoleName = "TEST";
            result.TypeVendorTruckId = 1;
            result.VendorTruckId = 1;
            result.VendorTruckName = "TEST";

            result.Should().NotBeNull();
            result.AttentionModeRoleId.Should().Be(1);
            result.AttentionModeId.Should().Be(1);
            result.AttentionModeName.Should().Be("TEST");
            result.RoleId.Should().Be(1);
            result.RoleName.Should().Be("TEST");
            result.TypeVendorTruckId.Should().Be(1);
            result.VendorTruckId.Should().Be(1);
            result.VendorTruckName.Should().Be("TEST");
        }
    }
}