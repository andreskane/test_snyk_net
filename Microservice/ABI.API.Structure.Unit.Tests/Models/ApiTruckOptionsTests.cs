using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.APIClient.Truck.Models.Tests
{
    [TestClass()]
    public class ApiTruckOptionsTests
    {
        [TestMethod()]
        public void ApiTruckOptionsTest()
        {
            var result = new ApiTruckOptions
            {
                UrlApiTruck = "TEST"
            };

            result.Should().NotBeNull();
            result.UrlApiTruck.Should().Be("TEST");
        }
    }
}