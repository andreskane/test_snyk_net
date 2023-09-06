using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class ResourceAttributesTests
    {
        [TestMethod()]
        public void ResourceAttributesTest()
        {
            var result = new ResourceAttributes();
            result.VdrCod = "TEST";
            result.VdrCodExt = "TEST";
            result.VdrCodLega = "TEST";
            result.VdrIdPtoEm = "TEST";
            result.VdrNom = "TEST";
            result.VdrTpoCat = "TEST";
            result.VdrTpoLpr = "TEST";

            result.Should().NotBeNull();
            result.VdrCod.Should().Be("TEST");
            result.VdrCodExt.Should().Be("TEST");
            result.VdrCodLega.Should().Be("TEST");
            result.VdrIdPtoEm.Should().Be("TEST");
            result.VdrNom.Should().Be("TEST");
            result.VdrTpoCat.Should().Be("TEST");
            result.VdrTpoLpr.Should().Be("TEST");
        }
    }
}