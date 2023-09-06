using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class TypeVendorTruckTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new TypeVendorTruck
            {
                AttentionModeRoleId = 1,
                VendorTruckId = null
            };

            result.Should().NotBeNull();
            result.AttentionModeRoleId.Should().Be(1);
            result.VendorTruckId.Should().BeNull();
        }
    }
}