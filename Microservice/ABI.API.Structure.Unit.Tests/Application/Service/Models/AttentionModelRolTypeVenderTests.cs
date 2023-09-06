using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Models.Tests
{
    [TestClass()]
    public class AttentionModelRolTypeVenderTests
    {
        [TestMethod()]
        public void AttentionModelRolTypeVenderTest()
        {
            var result = new AttentionModelRolTypeVender();
            result.AttentionMode = null;
            result.Role = null;
            result.AttentionModeRoleId = 1;
            result.TypeVendorTruckId = 1;
            result.VendorTruckId = null;
            result.VendorTruckName = "TEST";

            result.Should().NotBeNull();
            result.AttentionMode.Should().BeNull();
            result.Role.Should().BeNull();
            result.AttentionModeRoleId.Should().Be(1);
            result.TypeVendorTruckId.Should().Be(1);
            result.VendorTruckId.Should().BeNull();
            result.VendorTruckName.Should().Be("TEST");
        }

    }
}