using ABI.API.Structure.ACL.Truck.Domain.Entities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class ResourceResponsableTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new ResourceResponsable
            {
                Id = 1,
                ResourceId = 1,
                TruckId="1",
                IsVacant=true,
                CountryId=1,
                VendorCategory = "S"
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.CountryId.Should().Be(1);
            result.VendorCategory.Should().Be("S");
            result.TruckId.Should().Be("1");
            result.ResourceId.Should().Be(1);
            result.IsVacant.Should().Be(true);
        }
    }
}
