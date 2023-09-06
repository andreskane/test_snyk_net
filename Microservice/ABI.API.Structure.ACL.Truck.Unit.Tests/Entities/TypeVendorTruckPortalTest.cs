using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class TypeVendorTruckPortalTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new TypeVendorTruckPortal
            {
                VendorTruckId = 1,
                VendorTruckName = "TEST",
                AttentionModeId = null,
                RoleId = null,
                MappingTruckReading = null,
                MappingTruckWriting = null
            };

            result.Should().NotBeNull();
            result.VendorTruckId.Should().Be(1);
            result.VendorTruckName.Should().Be("TEST");
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.MappingTruckReading.Should().BeNull();
            result.MappingTruckWriting.Should().BeNull();
        }
    }
}