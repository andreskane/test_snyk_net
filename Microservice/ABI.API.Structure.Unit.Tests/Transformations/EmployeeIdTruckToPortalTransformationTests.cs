using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class EmployeeIdTruckToPortalTransformationTests
    {

        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.EmployeeIdTruckToPortalTransformation);

            nameTransformation.Items = resource;

            dynamic dyItem = new ExpandoObject();

            dyItem.LevelPortalId = 8;
            dyItem.TypeEmployeeTruck = "V,C";
            dyItem.EmployeeId = "200629";


            var employeeId = (int?)await nameTransformation.DoTransform(dyItem);

            Assert.IsNotNull(employeeId);
            Assert.AreEqual(6188, employeeId.Value);
        }


        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformNullTestAsync()
        {
            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.EmployeeIdTruckToPortalTransformation);

            var employeeId = (int?)await  nameTransformation.DoTransform(null);

            Assert.IsNull(employeeId);
        }
    }

}