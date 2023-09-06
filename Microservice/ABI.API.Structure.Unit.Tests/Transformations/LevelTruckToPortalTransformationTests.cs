using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.Unit.Tests.Inits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class LevelTruckToPortalTransformationTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            AddDataTruckACLContext.PrepareFactoryData();
            var contextDB = AddDataTruckACLContext._context;
            var mapeoTable = new MapeoTableTruckPortal(contextDB);

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.LevelTruckToPortalTransformation);
            nameTransformation.Items = mapeoTable.GetAllLevelTruckPortal().GetAwaiter().GetResult();

            var levelId = (int)await nameTransformation.DoTransform("DIRECCION");

            Assert.AreEqual(1, levelId);
        }
    }
}