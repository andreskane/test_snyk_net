using ABI.API.Structure.ACL.Truck.Application.Transformations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class ActiveNodePortalToTruckTransformationTests
    {
        [TestMethod()]
        public async Task DoTransformActiveTrueTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodePortalToTruckTransformation);

            var activate = (int) await nameTransformation.DoTransform(true);

            Assert.AreEqual(200, activate);
        }

        [TestMethod()]
        public async Task DoTransformActiveFalseTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodePortalToTruckTransformation);

            var activate = (int)await nameTransformation.DoTransform(false);

            Assert.AreEqual(201, activate);
        }
    }
}