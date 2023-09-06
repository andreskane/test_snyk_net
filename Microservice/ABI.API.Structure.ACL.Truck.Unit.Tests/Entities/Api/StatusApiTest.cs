using ABI.API.Structure.APIClient.Truck.Entities.EstadoApi;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class StatusApiTest
    {
        [TestMethod()]
        public void CreateStatusApiTest()
        {
            var result = new StatusApi
            {
                Static = "TEST",
                Date = "TEST"
            };

            result.Should().NotBeNull();
            result.Static.Should().Be("TEST");
            result.Date.Should().Be("TEST");

        }

        [TestMethod()]
        public void GetStatusTest()
        {
            var result = new StatusApi
            {
                Static = "TEST",
                Date = "TEST"
            };

            var resultStatus = "-- Dato de Entrada: 'TEST' [ Dato de Extra: 'TEST' ] Tiempo: 07/12/2021 09:35:47 PM --";

            result.GetStatus(resultStatus);

            result.Should().NotBeNull();
            result.Static.Should().Be("TEST");
            result.Date.Should().Be("12/07/2021 09:35:47 PM");

        }



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
