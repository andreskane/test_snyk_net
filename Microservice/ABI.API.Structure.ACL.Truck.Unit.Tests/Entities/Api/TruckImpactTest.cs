using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class TruckImpactTest
    {
        [TestMethod()]
        public void CreateEpeciniTest()
        {
            var result = new Epecini
            {
                Empresa = "TEST",
                TipoProceso = "TEST",
                NroVer = "TEST",
                FechaDesde = "TEST",
                FechaHasta = "TEST",
                LogSts = "TEST"
            };

            result.Should().NotBeNull();
            result.Empresa.Should().Be("TEST");
            result.TipoProceso.Should().Be("TEST");
            result.NroVer.Should().Be("TEST");
            result.FechaDesde.Should().Be("TEST");
            result.FechaHasta.Should().Be("TEST");
            result.LogSts.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEtecareaTest()
        {
            var result = new Etecarea
            {
                EmpId = null,
                ECVerNro = null,
                ECAveId = null,
                ECAveTxt = "TEST",
                ECAveAbv = "TEST",
                ECDveId = null,
                ECAveIdGea = null,
                ECAveIdFvt = null,
                ECAveUsuMo = "TEST",
                ECAveFecMo = null,
                ECAveHorMo = "TEST",
                ECAveTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECAveId.Should().BeNull();
            result.ECAveTxt.Should().Be("TEST");
            result.ECAveAbv.Should().Be("TEST");
            result.ECDveId.Should().BeNull();
            result.ECAveIdGea.Should().BeNull();
            result.ECAveIdFvt.Should().BeNull();
            result.ECAveUsuMo.Should().Be("TEST");
            result.ECAveFecMo.Should().BeNull();
            result.ECAveHorMo.Should().Be("TEST");
            result.ECAveTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEtecdireTest()
        {
            var result = new Etecdire
            {
                EmpId = null,
                ECVerNro = null,
                ECDveId = null,
                ECDveTxt = "TEST",
                ECDveAbv = "TEST",
                ECDveIdDve = null,
                ECDveIdFvt = null,
                ECDveUsuMo = "TEST",
                ECDveFecMo = null,
                ECDveHorMo = "TEST",
                ECDveTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECDveId.Should().BeNull();
            result.ECDveTxt.Should().Be("TEST");
            result.ECDveAbv.Should().Be("TEST");
            result.ECDveIdDve.Should().BeNull();
            result.ECDveIdFvt.Should().BeNull();
            result.ECDveUsuMo.Should().Be("TEST");
            result.ECDveFecMo.Should().BeNull();
            result.ECDveHorMo.Should().Be("TEST");
            result.ECDveTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEtecgereTest()
        {
            var result = new Etecgere
            {
                EmpId = null,
                ECVerNro = null,
                ECGrcId = null,
                ECGrcTxt = "TEST",
                ECGrcAbv = "TEST",
                ECAveId = null,
                ECGrcIdGte = null,
                ECGrcIdFvt = null,
                ECGrcUsuMo = "TEST",
                ECGrcFecMo = null,
                ECGrcHorMo = "TEST",
                ECGrcTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECGrcId.Should().BeNull();
            result.ECGrcTxt.Should().Be("TEST");
            result.ECGrcAbv.Should().Be("TEST");
            result.ECAveId.Should().BeNull();
            result.ECGrcIdGte.Should().BeNull();
            result.ECGrcIdFvt.Should().BeNull();
            result.ECGrcUsuMo.Should().Be("TEST");
            result.ECGrcFecMo.Should().BeNull();
            result.ECGrcHorMo.Should().Be("TEST");
            result.ECGrcTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEtecregiTest()
        {
            var result = new Etecregi
            {
                EmpId = null,
                ECVerNro = null,
                ECRegId = null,
                ECRegAbv = "TEST",
                ECRegTxt = "TEST",
                ECGrcId = null,
                ECRegIdJfe = null,
                ECRegIdFvt = null,
                ECRegUsuMo = "TEST",
                ECRegFecMo = null,
                ECRegHorMo = "TEST",
                ECRegTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECRegId.Should().BeNull();
            result.ECRegAbv.Should().Be("TEST");
            result.ECRegTxt.Should().Be("TEST");
            result.ECGrcId.Should().BeNull();
            result.ECRegIdJfe.Should().BeNull();
            result.ECRegIdFvt.Should().BeNull();
            result.ECRegUsuMo.Should().Be("TEST");
            result.ECRegFecMo.Should().BeNull();
            result.ECRegHorMo.Should().Be("TEST");
            result.ECRegTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEtecterrTest()
        {
            var result = new Etecterr
            {
                EmpId = null,
                ECVerNro = null,
                ECTrrId = null,
                ECTrrTxt = "TEST",
                ECZonId = "TEST",
                VdrCod = null,
                ECTrrAbv = "TEST",
                ECTrrFvtId = null,
                ECTrrUsuMo = "TEST",
                ECTrrFecMo = null,
                ECTrrHorMo = "TEST",
                ECTrrTipCr = "TEST",
                TpoVdrId = null,
                ECTrrIdMer = null
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECTrrId.Should().BeNull();
            result.ECTrrTxt.Should().Be("TEST");
            result.ECZonId.Should().Be("TEST");
            result.VdrCod.Should().BeNull();
            result.ECTrrAbv.Should().Be("TEST");
            result.ECTrrFvtId.Should().BeNull();
            result.ECTrrUsuMo.Should().Be("TEST");
            result.ECTrrFecMo.Should().BeNull();
            result.ECTrrHorMo.Should().Be("TEST");
            result.ECTrrTipCr.Should().Be("TEST");
            result.TpoVdrId.Should().BeNull();
            result.ECTrrIdMer.Should().BeNull();
        }

        [TestMethod()]
        public void CreateEteczocoTest()
        {
            var result = new Eteczoco
            {
                EmpId = null,
                ECVerNro = null,
                ECZonId = "TEST",
                ECZonIdCoo = null,
                ECZoCoUsMo = "TEST",
                ECZoCoFeMo = null,
                ECZoCoHoMo = "TEST",
                ECZoCoTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECZonId.Should().Be("TEST");
            result.ECZonIdCoo.Should().BeNull();
            result.ECZoCoUsMo.Should().Be("TEST");
            result.ECZoCoFeMo.Should().BeNull();
            result.ECZoCoHoMo.Should().Be("TEST");
            result.ECZoCoTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateEteczonaTest()
        {
            var result = new Eteczona
            {
                EmpId = null,
                ECVerNro = null,
                ECZonId = "TEST",
                ECZonTxt = "TEST",
                ECZonAbv = "TEST",
                ECRegId = null,
                ECZonIdSup = null,
                ECZonIdFvt = null,
                ECZonUsuMo = "TEST",
                ECZonFecMo = null,
                ECZonHorMo = "TEST",
                ECZonTipCr = "TEST"
            };

            result.Should().NotBeNull();
            result.EmpId.Should().BeNull();
            result.ECVerNro.Should().BeNull();
            result.ECZonId.Should().Be("TEST");
            result.ECZonTxt.Should().Be("TEST");
            result.ECZonAbv.Should().Be("TEST");
            result.ECRegId.Should().BeNull();
            result.ECZonIdSup.Should().BeNull();
            result.ECZonIdFvt.Should().BeNull();
            result.ECZonUsuMo.Should().Be("TEST");
            result.ECZonFecMo.Should().BeNull();
            result.ECZonHorMo.Should().Be("TEST");
            result.ECZonTipCr.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateMsgLogTest()
        {
            var result = new MsgLog
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateMsgLogLevel1Test()
        {
            var result = new MsgLogLevel1();
            result.ECLogSts = "TEST";
            result.ECLogTxt = "TEST";
            result.ECLogFch = null;
            result.Eclogsec = null;

            result.Should().NotBeNull();
            result.ECLogSts.Should().Be("TEST");
            result.ECLogTxt.Should().Be("TEST");
            result.ECLogFch.Should().BeNull();
            result.Eclogsec.Should().BeNull();
        }

        [TestMethod()]
        public void CreateOpecpiniInputTest()
        {
            var result = new OpecpiniInput
            {
                Epeciniin = null
            };

            result.Should().NotBeNull();
            result.Epeciniin.Should().BeNull();
        }

        [TestMethod()]
        public void CreateOpecpiniOutTest()
        {
            var result = new OpecpiniOut
            {
                Epeciniin = null,
                Msglog = null
            };

            result.Should().NotBeNull();
            result.Epeciniin.Should().BeNull();
            result.Msglog.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePtecareaInputTest()
        {
            var result = new PtecareaInput
            {
                Tecarea = null
            };

            result.Should().NotBeNull();
            result.Tecarea.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePtecdireInputTest()
        {
            var result = new PtecdireInput();
            result.Tecdire = null;

            result.Should().NotBeNull();
            result.Tecdire.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePtecgereInputTest()
        {
            var result = new PtecgereInput
            {
                Tecgere = null
            };

            result.Should().NotBeNull();
            result.Tecgere.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePtecregiInputTest()
        {
            var result = new PtecregiInput
            {
                Tecregi = null
            };

            result.Should().NotBeNull();
            result.Tecregi.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePtecterrInputTest()
        {
            var result = new PtecterrInput
            {
                Tecterr = null
            };

            result.Should().NotBeNull();
            result.Tecterr.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePteczocoInputTest()
        {
            var result = new PteczocoInput
            {
                Teczoco = null
            };

            result.Should().NotBeNull();
            result.Teczoco.Should().BeNull();
        }

        [TestMethod()]
        public void CreatePteczonaInputTest()
        {
            var result = new PteczonaInput
            {
                Teczona = null
            };

            result.Should().NotBeNull();
            result.Teczona.Should().BeNull();
        }

        [TestMethod()]
        public void CreateStructureTruckTest()
        {
            var result = new StructureTruck
            {
                OpecpiniOut = null,
                Ptecdire = null,
                Ptecarea = null,
                Ptecgere = null,
                Ptecregi = null,
                Pteczoco = null,
                Pteczona = null,
                Ptecterr = null
            };

            result.Should().NotBeNull();
            result.OpecpiniOut.Should().BeNull();
            result.Ptecdire.Should().BeNull();
            result.Ptecarea.Should().BeNull();
            result.Ptecgere.Should().BeNull();
            result.Ptecregi.Should().BeNull();
            result.Pteczoco.Should().BeNull();
            result.Pteczona.Should().BeNull();
            result.Ptecterr.Should().BeNull();
        }



    }
}
