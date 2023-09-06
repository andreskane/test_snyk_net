using ABI.API.Structure.APIClient.Truck.Entities.CentroDeDespacho;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class OfficeCenterTest
    {
        [TestMethod()]
        public void CreateOfficeCenterTest()
        {
            var result = new OfficeCenter
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }


        [TestMethod()]
        public void CreateOfficeCenterLevel1Test()
        {
            var result = new OfficeCenterLevel1
            {
                EmpId = "TEST",
                CdpId = "TEST",
                CdpAbv = "TEST",
                CdpIdDpsIdDf = "TEST",
                CdpDpsTxt = "TEST",
                CdpFlgCodD = "TEST",
                CdpTpoCtlM = "TEST",
                CdpValMin = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().Be("TEST");
            result.CdpId.Should().Be("TEST");
            result.CdpAbv.Should().Be("TEST");
            result.CdpIdDpsIdDf.Should().Be("TEST");
            result.CdpDpsTxt.Should().Be("TEST");
            result.CdpFlgCodD.Should().Be("TEST");
            result.CdpTpoCtlM.Should().Be("TEST");
            result.CdpValMin.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateTruckOfficeCenterTest()
        {
            var result = new TruckOfficeCenter
            {
                OfficeCenter = null
            };

            result.Should().NotBeNull();
            result.OfficeCenter.Should().BeNull();
        }

    }
}
