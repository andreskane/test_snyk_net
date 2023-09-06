using ABI.API.Structure.ACL.Truck.Application.Transformations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class StructureTranformationTests
    {
        [TestMethod()]
        public void InstanciateTransformationTest()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTruckToPortalTransformation);

            Assert.IsNotNull(nameTransformation);
        }
    }
}