using ABI.API.Structure.ACL.Truck.Application.Transformations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class ShortNameTransformationTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.ShortNameTransformation);

            var name = (string)await nameTransformation.DoTransform("Argentina");

            Assert.AreEqual("ARGENTINA", name);
        }
    }
}