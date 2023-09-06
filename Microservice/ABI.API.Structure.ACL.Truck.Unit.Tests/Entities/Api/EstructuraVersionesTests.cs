using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class EstructuraVersionesTests
    {
        [TestMethod()]
        public void CreateEstructuraVersionesTest()
        {
            var result = new EstructuraVersiones
            {
                Level1 = new List<EstructuraVersionesLevel1>()
            };

            result.Should().NotBeNull();
            result.Level1.Should().NotBeNull();

        }

        [TestMethod()]
        public void CreateEstructuraVersionesLevel1Test()
        {
            var result = new EstructuraVersionesLevel1
            {
                ECFecAlt = "2020-12-30",
                ECFecApr = "2020-12-30",
                ECFecDes = "2020-12-30",
                ECFecHas = "2031-12-31",
                ECFecMod = "2020-12-30",
                ECFecTra = "2020-12-30",
                ECHorAlt = "14:53:45",
                ECHorApr = "16:09:28",
                ECHorMod = "16:08:37",
                ECHorTra = "16:09:37",
                ECIncMsg = "",
                ECIndTra = "S",
                ECStsCod = "APR",
                ECTipCre = "",
                ECUsuAlt = "TESTCMQ1",
                ECUsuApr = "AR3ROBOT",
                ECUsuMod = "AR3ROBOT",
                ECUsuTra = "AR3ROBOT",
                ECVerNro = 1396,
                EmpId = 1
            };

            result.Should().NotBeNull();



            result.ECFecAlt.Should().Be("2020-12-30");
            result.ECFecApr.Should().Be("2020-12-30");
            result.ECFecDes.Should().Be("2020-12-30");
            result.ECFecHas.Should().Be("2031-12-31");
            result.ECFecMod.Should().Be("2020-12-30");
            result.ECFecTra.Should().Be("2020-12-30");
            result.ECHorAlt.Should().Be("14:53:45");
            result.ECHorApr.Should().Be("16:09:28");
            result.ECHorMod.Should().Be("16:08:37");
            result.ECHorTra.Should().Be("16:09:37");
            result.ECIncMsg.Should().Be("");
            result.ECIndTra.Should().Be("S");
            result.ECStsCod.Should().Be("APR");
            result.ECTipCre.Should().Be("");
            result.ECUsuAlt.Should().Be("TESTCMQ1");
            result.ECUsuApr.Should().Be("AR3ROBOT");
            result.ECUsuMod.Should().Be("AR3ROBOT");
            result.ECUsuTra.Should().Be("AR3ROBOT");
            result.ECVerNro.Should().Be(1396);
            result.EmpId.Should().Be(1);

        }

        [TestMethod()]
        public void CreateEstructuraVersionInputTest()
        {
            var result = new EstructuraVersionInput
            {
                EmpId = 1,
                ECVerNro = "000000"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().Be(1);
            result.ECVerNro.Should().Be("000000");

        }

        [TestMethod()]
        public void CreateEstructuraVersionOutputTest()
        {
            var result = new EstructuraVersionOutput
            {
                EstructuraVersiones = new EstructuraVersiones()
            };

            result.Should().NotBeNull();
            result.EstructuraVersiones.Should().NotBeNull();

        }


    }
}
