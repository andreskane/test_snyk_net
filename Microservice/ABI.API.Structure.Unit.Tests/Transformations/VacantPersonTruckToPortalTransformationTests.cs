using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class VacantPersonTruckToPortalTransformationTests
    { 
        [TestMethod()]
        public async Task DoTransformTestAsync()
        {
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("Resource.json"));

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.VacantPersonTruckToPortalTransformation);

            nameTransformation.Items = resource;

            dynamic dyItem = new ExpandoObject();

            dyItem.LevelPortalId = 8;
            dyItem.TypeEmployeeTruck = "V,C";
            dyItem.EmployeeId = "200629";


            var vacante = (bool) await nameTransformation.DoTransform(dyItem);

            Assert.IsNotNull(vacante);
            Assert.AreEqual(false, vacante);
        }


        [TestMethod()]
        public async Task DoTransformNullTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.VacantPersonTruckToPortalTransformation);

            var vacante = (bool)await nameTransformation.DoTransform(null);

            Assert.AreEqual(false, vacante);
        }

    }
}