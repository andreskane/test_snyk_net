using ABI.API.Structure.APIClient.Truck.Entities.EstructuraRutas;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class RoutingStructureTest
    {
        [TestMethod()]
        public void CreateRoutingStructureTest()
        {
            var result = new RoutingStructure
            {
                EmpId = "TEST",
                Level1 = null
            };

            result.Should().NotBeNull();
            result.EmpId.Should().Be("TEST");
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateRoutingStructureLevel1Test()
        {
            var result = new RoutingStructureLevel1
            {
                CliTrrId = "TEST",
                CliId = "TEST",
                CliNom = "TEST",
                CliAbv = "TEST",
                CliDom = "TEST",
                CliNroDom = "TEST",
                CliSts = "TEST",
                CliTrrValF = "TEST",
                CliTrrNroD = "TEST",
                CliTrrPrjC = "TEST",
                CliTrrSdoC = "TEST",
                CliTrrIdCd = "TEST",
                CdpAbv = "TEST",
                CliTrrOrdV = "TEST",
                CnlId = "TEST",
                CnlAbv = "TEST",
                Level2 = null,
                ZonEntId = "TEST"
            };

            result.Should().NotBeNull();
            result.CliTrrId.Should().Be("TEST");
            result.CliId.Should().Be("TEST");
            result.CliNom.Should().Be("TEST");
            result.CliAbv.Should().Be("TEST");
            result.CliDom.Should().Be("TEST");
            result.CliNroDom.Should().Be("TEST");
            result.CliSts.Should().Be("TEST");
            result.CliTrrValF.Should().Be("TEST");
            result.CliTrrNroD.Should().Be("TEST");
            result.CliTrrPrjC.Should().Be("TEST");
            result.CliTrrSdoC.Should().Be("TEST");
            result.CliTrrIdCd.Should().Be("TEST");
            result.CdpAbv.Should().Be("TEST");
            result.CliTrrOrdV.Should().Be("TEST");
            result.CnlId.Should().Be("TEST");
            result.CnlAbv.Should().Be("TEST");
            result.Level2.Should().BeNull();
            result.ZonEntId.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateRoutingStructureLevel2Test()
        {
            var result = new RoutingStructureLevel2
            {
                AtnCod = "TEST",
                RutId = "TEST",
                CliIdLoc = "TEST",
                RutVtaValO = "TEST",
                RutTpo = "TEST",
                RutNroDia = "TEST",
                RutDiaTxt = "TEST"
            };

            result.Should().NotBeNull();
            result.AtnCod.Should().Be("TEST");
            result.RutId.Should().Be("TEST");
            result.CliIdLoc.Should().Be("TEST");
            result.RutVtaValO.Should().Be("TEST");
            result.RutTpo.Should().Be("TEST");
            result.RutNroDia.Should().Be("TEST");
            result.RutDiaTxt.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateTruckTerritoryTest()
        {
            var result = new TruckTerritory
            {
                ConsultationDate = DateTime.MinValue,
                RoutingStructure = null
            };

            result.Should().NotBeNull();
            result.ConsultationDate.Should().Be(DateTime.MinValue);
            result.RoutingStructure.Should().BeNull();
        }

    }
}
