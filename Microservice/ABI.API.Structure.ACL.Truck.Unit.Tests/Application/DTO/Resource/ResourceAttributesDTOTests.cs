using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Resource
{
    [TestClass()]
    public class ResourceAttributesDTOTests
    {
        [TestMethod()]
        public void ResourceAttributesDTOTest()
        {
            var result = new ResourceAttributesDTO
            {
                VdrCod = "TEST",
                VdrCodExt = "TEST",
                VdrCodLega = "TEST",
                VdrIdPtoEm = "TEST",
                VdrNom = "TEST",
                VdrTpoCat = "TEST",
                VdrTpoLpr = "TEST",
                Vacante = "TEST"
            };

            result.Should().NotBeNull();
            result.VdrCod.Should().Be("TEST");
            result.VdrCodExt.Should().Be("TEST");
            result.VdrCodLega.Should().Be("TEST");
            result.VdrIdPtoEm.Should().Be("TEST");
            result.VdrNom.Should().Be("TEST");
            result.VdrTpoCat.Should().Be("TEST");
            result.VdrTpoLpr.Should().Be("TEST");
            result.Vacante.Should().Be("TEST");
        }
    }
}