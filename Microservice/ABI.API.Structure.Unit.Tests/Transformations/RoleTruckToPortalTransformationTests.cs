﻿using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.Unit.Tests.Inits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class RoleTruckToPortalTransformationTests
    {

        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            AddDataTruckACLContext.PrepareFactoryData();
            var contextDB = AddDataTruckACLContext._context;
            var mapeoTable = new MapeoTableTruckPortal(contextDB);

            var nameTransformation = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTruckToPortalTransformation);
            nameTransformation.Items = mapeoTable.GetAllLevelTruckPortal().GetAwaiter().GetResult();

            var rolId = (int)await nameTransformation.DoTransform("DIRECCION");

            Assert.AreEqual(24, rolId);
        }
    }
}