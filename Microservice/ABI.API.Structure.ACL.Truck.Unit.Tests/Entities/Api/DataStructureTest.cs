using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class DataStructureTest
    {
        [TestMethod()]
        public void CreateTruckStructureTest()
        {
            var result = new TruckStructure
            {
                ConsultationDate = DateTime.MinValue,
                DataStructure = null
            };

            result.Should().NotBeNull();
            result.ConsultationDate.Should().Be(DateTime.MinValue);
            result.DataStructure.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureTest()
        {
            var result = new DataStructure
            {
                EmpId = "TEST",
                Level1 = null
            };

            result.Should().NotBeNull();
            result.EmpId.Should().Be("TEST");
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel1Test()
        {
            var result = new DataStructureLevel1
            {
                DveId = "TEST",
                DveTxt = "TEST",
                DveAbv = "TEST",
                DveIdFvt = "TEST",
                DveFvtAbv = "TEST",
                DveIdDve = "TEST",
                DveNom = "TEST",
                Level2 = null
            };

            result.Should().NotBeNull();
            result.DveId.Should().Be("TEST");
            result.DveTxt.Should().Be("TEST");
            result.DveAbv.Should().Be("TEST");
            result.DveIdFvt.Should().Be("TEST");
            result.DveFvtAbv.Should().Be("TEST");
            result.DveIdDve.Should().Be("TEST");
            result.DveNom.Should().Be("TEST");
            result.Level2.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel2Test()
        {
            var result = new DataStructureLevel2
            {
                AveId = "TEST",
                AveTxt = "TEST",
                AveAbv = "TEST",
                AveIdGea = "TEST",
                AveNom = "TEST",
                AveIdFvt = "TEST",
                AveFvtAbv = "TEST",
                Level3 = null
            };

            result.Should().NotBeNull();
            result.AveId.Should().Be("TEST");
            result.AveTxt.Should().Be("TEST");
            result.AveAbv.Should().Be("TEST");
            result.AveIdGea.Should().Be("TEST");
            result.AveNom.Should().Be("TEST");
            result.AveIdFvt.Should().Be("TEST");
            result.AveFvtAbv.Should().Be("TEST");
            result.Level3.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel3Test()
        {
            var result = new DataStructureLevel3
            {
                GrcId = "TEST",
                GrcTxt = "TEST",
                GrcAbv = "TEST",
                GrcIdFvt = "TEST",
                GrcFvtAbv = "TEST",
                GrcIdGte = "TEST",
                GrcNom = "TEST",
                Level4 = null
            };

            result.Should().NotBeNull();
            result.GrcId.Should().Be("TEST");
            result.GrcTxt.Should().Be("TEST");
            result.GrcAbv.Should().Be("TEST");
            result.GrcIdFvt.Should().Be("TEST");
            result.GrcFvtAbv.Should().Be("TEST");
            result.GrcIdGte.Should().Be("TEST");
            result.GrcNom.Should().Be("TEST");
            result.Level4.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel4Test()
        {
            var result = new DataStructureLevel4
            {
                RegId = "TEST",
                RegTxt = "TEST",
                RegAbv = "TEST",
                RegIdJfe = "TEST",
                RegNom = "TEST",
                RegIdFvt = "TEST",
                RegFvtAbv = "TEST",
                RegIdAsist = "TEST",
                AsiNom = "TEST",
                Level5 = null
            };

            result.Should().NotBeNull();
            result.RegId.Should().Be("TEST");
            result.RegTxt.Should().Be("TEST");
            result.RegAbv.Should().Be("TEST");
            result.RegIdJfe.Should().Be("TEST");
            result.RegNom.Should().Be("TEST");
            result.RegIdFvt.Should().Be("TEST");
            result.RegFvtAbv.Should().Be("TEST");
            result.RegIdAsist.Should().Be("TEST");
            result.AsiNom.Should().Be("TEST");
            result.Level5.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel5Test()
        {
            var result = new DataStructureLevel5
            {
                ZonId = "TEST",
                ZonTxt = "TEST",
                ZonAbv = "TEST",
                ZonIdSup = "TEST",
                ZonNom = "TEST",
                ZonIdFvt = "TEST",
                ZonFvtAbv = "TEST",
                ZonIdCoor = "TEST",
                CooNom = "TEST",
                ZonIdPrm = "TEST",
                PrmNom = "TEST",
                Level6 = null
            };

            result.Should().NotBeNull();
            result.ZonId.Should().Be("TEST");
            result.ZonTxt.Should().Be("TEST");
            result.ZonAbv.Should().Be("TEST");
            result.ZonIdSup.Should().Be("TEST");
            result.ZonNom.Should().Be("TEST");
            result.ZonIdFvt.Should().Be("TEST");
            result.ZonFvtAbv.Should().Be("TEST");
            result.ZonIdCoor.Should().Be("TEST");
            result.CooNom.Should().Be("TEST");
            result.ZonIdPrm.Should().Be("TEST");
            result.PrmNom.Should().Be("TEST");
            result.Level6.Should().BeNull();
        }

        [TestMethod()]
        public void CreateDataStructureLevel6Test()
        {
            var result = new DataStructureLevel6();
            result.TrrId = "TEST";
            result.TrrTxt = "TEST";
            result.TrrAbv = "TEST";
            result.VdrCod = "TEST";
            result.TrrNom = "TEST";
            result.TrrIdFvt = "TEST";
            result.TrrFvtAbv = "TEST";
            result.TrrIdDps = "TEST";
            result.DpsAbv = "TEST";
            result.VdrTpoCat = "TEST";
            result.TpoVdrId = "TEST";
            result.TpoVdrAbv = "TEST";
            result.TrrIdMer = "TEST";
            result.MerNom = "TEST";
            result.CatVdrId = "TEST";
            result.CatVdrAbv = "TEST";

            result.Should().NotBeNull();
            result.TrrId.Should().Be("TEST");
            result.TrrTxt.Should().Be("TEST");
            result.TrrAbv.Should().Be("TEST");
            result.VdrCod.Should().Be("TEST");
            result.TrrNom.Should().Be("TEST");
            result.TrrIdFvt.Should().Be("TEST");
            result.TrrFvtAbv.Should().Be("TEST");
            result.TrrIdDps.Should().Be("TEST");
            result.DpsAbv.Should().Be("TEST");
            result.VdrTpoCat.Should().Be("TEST");
            result.TpoVdrId.Should().Be("TEST");
            result.TpoVdrAbv.Should().Be("TEST");
            result.TrrIdMer.Should().Be("TEST");
            result.MerNom.Should().Be("TEST");
            result.CatVdrId.Should().Be("TEST");
            result.CatVdrAbv.Should().Be("TEST");
        }
    }
}
