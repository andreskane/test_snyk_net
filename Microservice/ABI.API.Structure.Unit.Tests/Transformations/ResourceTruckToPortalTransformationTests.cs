using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class ResourceTruckToPortalTransformationTest
    { 
        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("Resource.json"));

            var nameTransformation = new ResourceTruckToPortalTransformation();

            nameTransformation.Items = resource;

            dynamic dyItem = new ExpandoObject();

            dyItem.LevelPortalId = 8;
            dyItem.TypeEmployeeTruck = "V,C";
            dyItem.EmployeeId = "200629";

            var resourceEmp = (ResourceDTO)await nameTransformation.DoTransform(dyItem);

            Assert.IsNotNull(resourceEmp);
        }


        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformNullTestAsync()
        {
            var nameTransformation = new ResourceTruckToPortalTransformation();

            var resource = (ResourceDTO)await nameTransformation.DoTransform(null);

            Assert.IsNull(resource);
        }

    }
}