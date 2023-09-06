using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.ApiClient
{
    [TestClass()]
    public class ApiClientTruckTest
    {

        [TestMethod()]
        public void GetStructureTruckTest()
        {
            var mockApiTruck = new Mock<IApiTruck>();
           mockApiTruck
                .Setup(s => s.GetStructureTruck(It.IsAny<string>()))
                .ReturnsAsync(new TruckStructure());


            mockApiTruck.Object.Should().NotBeNull();
        }
    }
}
