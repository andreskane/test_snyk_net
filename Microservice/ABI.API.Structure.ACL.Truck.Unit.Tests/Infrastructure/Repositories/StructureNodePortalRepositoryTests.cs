using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories.Tests
{
    [TestClass()]
    public class StructureNodePortalRepositoryTests
    {
        private static StructureContext _context;
        private static IStructureNodePortalRepository _structureNodeRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            _structureNodeRepo = new StructureNodePortalRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void StructureNodePortalRepositoryTest()
        {
            var result = new StructureNodePortalRepository(_context);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllGradeChangesForTruckTest()
        {
            var results = await _structureNodeRepo.GetAllGradeChangesForTruck(10, DateTimeOffset.MinValue);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristasGradeChangesForTruckTest()
        {
            var results = await _structureNodeRepo.GetAllAristasGradeChangesForTruck(10, DateTimeOffset.MinValue);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllChildNodeForTruckTest()
        {
            var results = await _structureNodeRepo.GetAllChildNodeForTruck(10, 100016, DateTimeOffset.MinValue);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoParentTest()
        {
            var results = await _structureNodeRepo.GetNodoParent(10, 100016);
            results.Should().NotBeNull();
        }
    }
}