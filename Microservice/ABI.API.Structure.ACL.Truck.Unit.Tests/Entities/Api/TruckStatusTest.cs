using ABI.API.Structure.APIClient.Truck.Entities.EstadoApi;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class TruckStatusTest
    {
        [TestMethod()]
        public void CreateTruckStatusTest()
        {
            var result = new TruckStatus
            {
                DatoOut = "TEST"
            };

            result.Should().NotBeNull();
            result.DatoOut.Should().Be("TEST");
        }


    }
}
