using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class LevelTruckPortalTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new LevelTruckPortal
            {
                LevelTruck = 1,
                LevelTruckName = "TEST",
                LevelPortalId = 1,
                LevelPortalName = "TEST",
                TypeEmployeeTruck = "TEST",
                RolPortalId = null
            };

            result.Should().NotBeNull();
            result.LevelTruck.Should().Be(1);
            result.LevelTruckName.Should().Be("TEST");
            result.LevelPortalId.Should().Be(1);
            result.LevelPortalName.Should().Be("TEST");
            result.TypeEmployeeTruck.Should().Be("TEST");
            result.RolPortalId.Should().BeNull();
        }
    }
}