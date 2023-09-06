using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.TruckTests.Infrastructure.Repositories
{
    [TestClass()]
    public class MapeoTableTruckPortalRepositoryTest
    {

        private static TruckACLContext _context;
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataTruckACLContext._context;
            _mapeoTableTruckPortal = new MapeoTableTruckPortal(_context);
        }

        [TestMethod()]
        public void GetAllLevelTruckPortalTest()
        {
            var result = _mapeoTableTruckPortal.GetAllLevelTruckPortal();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetAllBusinessTruckPortalTest()
        {
            var result = _mapeoTableTruckPortal.GetAllBusinessTruckPortal();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetOneBusinessTruckPortalTest()
        {
            var result = _mapeoTableTruckPortal.GetOneBusinessTruckPortal("001");
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetAllTypeVendorTruckPortalTest()
        {
            var result = _mapeoTableTruckPortal.GetAllTypeVendorTruckPortal();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetOneBusinessTruckPortalByNameTest()
        {
            var result = _mapeoTableTruckPortal.GetOneBusinessTruckPortalByName("ARGENTINA");
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetOneLevelTruckPortalByLevelIdTest()
        {
            var result = _mapeoTableTruckPortal.GetOneLevelTruckPortalByLevelId(2);
            result.Should().NotBeNull();
        }
    }
}
