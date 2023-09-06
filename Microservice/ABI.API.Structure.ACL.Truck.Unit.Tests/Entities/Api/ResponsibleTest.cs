using ABI.API.Structure.APIClient.Truck.Entities.Responsables;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class ResponsibleTest
    {

        [TestMethod()]
        public void CreateTruckResponsibleTest()
        {
            var result = new TruckResponsible
            {
                Responsible = null
            };

            result.Should().NotBeNull();
            result.Responsible.Should().BeNull();
        }

        [TestMethod()]
        public void CreateResponsibleTest()
        {
            var result = new Responsible
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateResponsibleLevel1Test()
        {
            var result = new ResponsibleLevel1
            {
                VdrCod = "TEST",
                VdrNom = "TEST",
                VdrTpoCat = "TEST",
                VdrTpoLpr = "TEST",
                VdrCodExt = "TEST",
                VdrIdPtoEm = "TEST",
                VdrCodLega = "TEST"
            };

            result.Should().NotBeNull();
            result.VdrCod.Should().Be("TEST");
            result.VdrNom.Should().Be("TEST");
            result.VdrTpoCat.Should().Be("TEST");
            result.VdrTpoLpr.Should().Be("TEST");
            result.VdrCodExt.Should().Be("TEST");
            result.VdrIdPtoEm.Should().Be("TEST");
            result.VdrCodLega.Should().Be("TEST");
        }
    }
}
