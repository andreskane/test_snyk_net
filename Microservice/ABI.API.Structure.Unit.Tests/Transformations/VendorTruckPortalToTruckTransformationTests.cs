using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class VendorTruckPortalToTruckTransformationTests
    {

        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            AddDataTruckACLContext.PrepareFactoryData();
            var contextDB = AddDataTruckACLContext._context;
            var mapeoTable = new MapeoTableTruckPortal(contextDB);

            var node = FactoryMock.GetMockJson<NodePortalTruckDTO>(FactoryMock.GetMockPath("JsonNodePortalTruck-NodoTerritorio.json"));

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.VendorTruckPortalToTruckTransformation);
            nameTransformation.Items = mapeoTable.GetAllTypeVendorTruckPortal().GetAwaiter().GetResult();

            var catVender = (int)await nameTransformation.DoTransform(node);

            Assert.AreEqual(7, catVender);
        }
    }
}