using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.Unit.Tests.Inits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class AttentionModeTruckToPortalTransformationTests
    {


        [TestMethod()]
        public async Task DoTransformTestAsync()
        {
            AddDataTruckACLContext.PrepareFactoryData();
            var contextDB = AddDataTruckACLContext._context;
            var mapeoTable = new MapeoTableTruckPortal(contextDB);

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.AttentionModeTruckToPortalTransformation);
            nameTransformation.Items = mapeoTable.GetAllTypeVendorTruckPortal().GetAwaiter().GetResult();

            var attentionModeId = (int) await nameTransformation.DoTransform(32);

            Assert.AreEqual(4, attentionModeId);
        }
    }
}