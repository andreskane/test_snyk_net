using ABI.API.Structure.ACL.Truck.Application.Transformations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class ActiveNodeTruckToPortalTransformationTests
    {
        [TestMethod()]
        public async Task DoTransformActiveTrueTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodeTruckToPortalTransformation);

            var activate = (bool) await nameTransformation.DoTransform("200");

            Assert.AreEqual(true, activate);
        }

        [TestMethod()]
        public async Task DoTransformActiveFalseTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodeTruckToPortalTransformation);

            var activate = (bool)await nameTransformation.DoTransform("201");

            Assert.AreEqual(false, activate);
        }
    }
}