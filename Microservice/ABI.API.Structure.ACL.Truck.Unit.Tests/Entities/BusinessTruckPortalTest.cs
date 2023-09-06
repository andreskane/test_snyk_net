using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class BusinessTruckPortalTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new BusinessTruckPortal
            {
                BusinessCode = "TEST",
                StructureModelId = 1
            };

            result.Should().NotBeNull();
            result.BusinessCode.Should().Be("TEST");
            result.StructureModelId.Should().Be(1);
        }
    }
}