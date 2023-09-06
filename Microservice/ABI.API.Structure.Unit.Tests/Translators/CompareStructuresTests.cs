using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class CompareStructuresTests
    {
        [TestMethod()]
        public async Task CompareTruckToPortalTestAsync()
        {
            var truck = FactoryMock.GetMockJson<TruckStructure>(FactoryMock.GetMockPath("JsonTruckStructureNewNodes.json"));
            var portal = FactoryMock.GetMockJson<StructureNodeDTO>(FactoryMock.GetMockPath("JsonPortalStructure.json"));
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("Resource.json"));

            AddDataTruckACLContext.PrepareFactoryData();

            var mapping = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            var translator = new CompareStructures( mapping);

            var structureCompare = await translator.CompareTruckToPortal("ARGENTINA", truck, portal, resource);

            Assert.AreEqual(2, structureCompare.UpdateNodes.Count);
        }

        [TestMethod()]
        public async Task CompareTruckToPortalJefaturaTest()
        {
            var truck = FactoryMock.GetMockJson<TruckStructure>(FactoryMock.GetMockPath("JsonTruckStructureNewNodesJefature.json"));
            var portal = FactoryMock.GetMockJson<StructureNodeDTO>(FactoryMock.GetMockPath("JsonPortalStructure.json"));
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("Resource.json"));

            AddDataTruckACLContext.PrepareFactoryData();

            var mapping = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            var translator = new CompareStructures( mapping);

            var structureCompare = await translator.CompareTruckToPortal("ARGENTINA", truck, portal, resource);

            Assert.AreEqual(3, structureCompare.UpdateNodes.Count);
        }
    }
}