using ABI.API.Structure.APIClient.Truck.Entities.CategoriaVendedor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class SellerCategoryTest
    {
        [TestMethod()]
        public void CreateSellerCategoryTest()
        {
            var result = new SellerCategory
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }


        [TestMethod()]
        public void CreateSellerCategoryLevel1Test()
        {
            var result = new SellerCategoryLevel1
            {
                CatVdrId = "TEST",
                CatVdrTxt = "TEST",
                CatVdrAbv = "TEST",
                CatVdrSts = "TEST",
                CatVdrIngPed = "TEST",
                CatVdrRutEnt = "TEST",
                CatVdrRutVta = "TEST"
            };

            result.Should().NotBeNull();
            result.CatVdrId.Should().Be("TEST");
            result.CatVdrTxt.Should().Be("TEST");
            result.CatVdrAbv.Should().Be("TEST");
            result.CatVdrSts.Should().Be("TEST");
            result.CatVdrIngPed.Should().Be("TEST");
            result.CatVdrRutEnt.Should().Be("TEST");
            result.CatVdrRutVta.Should().Be("TEST");
        }

        [TestMethod()]
        public void CreateTruckSellerCategoryest()
        {
            var result = new TruckSellerCategory
            {
                SellerCategory = null
            };

            result.Should().NotBeNull();
            result.SellerCategory.Should().BeNull();
        }

    }
}
