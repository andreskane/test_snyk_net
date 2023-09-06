using ABI.API.Structure.APIClient.Truck.Entities.TipoVendedores;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class TypeSellerTest
    {

        [TestMethod()]
        public void CreateTruckTypeSellerTest()
        {
            var result = new TruckTypeSeller
            {
                TypeSeller = null
            };

            result.Should().NotBeNull();
            result.TypeSeller.Should().BeNull();
        }

        [TestMethod()]
        public void CreateTypeSellerTest()
        {
            var result = new TypeSeller
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateTypeSellerLevel1Test()
        {
            var result = new TypeSellerLevel1
            {
                TpoVdrId = "TEST",
                TpoVdrAbv = "TEST",
                TpoVdrTxt = "TEST",
                CatVdrId = "TEST"
            };

            result.Should().NotBeNull();
            result.TpoVdrId.Should().Be("TEST");
            result.TpoVdrAbv.Should().Be("TEST");
            result.TpoVdrTxt.Should().Be("TEST");
            result.CatVdrId.Should().Be("TEST");
        }
    }
}
