using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Structure;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Infrastructure.Repositories
{
    public class StructureNodeDefinitionsRespositoryTests
    {
        private static StructureContext _context;



        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }

        [TestMethod()]
        public void StructureNodePortalRepositoryTest()
        {
            var result = new StructureNodeDefinitionsRespository(_context);
            result.Should().NotBeNull();
        }

         [TestMethod()]
        public void StructureNodePortalRepositoryGetByIdAsyncTest()
        {
            var reposiroty = new StructureNodeDefinitionsRespository(_context);

            var result = reposiroty.GetByIdAsync(1);
            result.Should().NotBeNull();
        }
    }
}
