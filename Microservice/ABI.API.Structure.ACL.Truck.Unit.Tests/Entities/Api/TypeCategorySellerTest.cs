using ABI.API.Structure.APIClient.Truck.Entities.TipoCategoriaVendedor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities.Api
{
    [TestClass()]
    public class TypeCategorySellerTest
    {
        [TestMethod()]
        public void CreateTruckTypeCategorySellerTest()
        {
            var result = new TruckTypeCategorySeller
            {
                TypeCategorySeller = null
            };

            result.Should().NotBeNull();
            result.TypeCategorySeller.Should().BeNull();
        }


        [TestMethod()]
        public void CreateTypeCategorySellerTest()
        {
            var result = new TypeCategorySeller
            {
                Level1 = null
            };

            result.Should().NotBeNull();
            result.Level1.Should().BeNull();
        }

        [TestMethod()]
        public void CreateTypeCategorySellerLevel1Test()
        {
            var result = new TypeCategorySellerLevel1
            {
                CatResID = "TEST",
                CatResNom = "TEST",
                CatResAbv = "TEST",
                CatResPtoE = "TEST",
                CatResUsrI = "TEST",
                CatResCom = "TEST",
                CatResComD = "TEST",
                CatResLeg = "TEST",
                CatResAuto = "TEST",
                CatResVac = "TEST",
                CatResEstF = "TEST",
                CatResNroD = "TEST"
            };

            result.Should().NotBeNull();
            result.CatResID.Should().Be("TEST");
            result.CatResNom.Should().Be("TEST");
            result.CatResAbv.Should().Be("TEST");
            result.CatResPtoE.Should().Be("TEST");
            result.CatResUsrI.Should().Be("TEST");
            result.CatResCom.Should().Be("TEST");
            result.CatResComD.Should().Be("TEST");
            result.CatResLeg.Should().Be("TEST");
            result.CatResAuto.Should().Be("TEST");
            result.CatResVac.Should().Be("TEST");
            result.CatResEstF.Should().Be("TEST");
            result.CatResNroD.Should().Be("TEST");
        }


    }
}
