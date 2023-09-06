using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.TruckTests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.LogImpactTruck.Tests
{
    [TestClass()]
    public class GetAllStructureStatesQueryTests
    {
        private static TruckACLContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataTruckACLContext._context;
        }


        [TestMethod()]
        public void GetAllStructureStatesQueryTest()
        {
            var result = new GetAllStructureStatesQuery();

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureStatesQueryHandlerTest()
        {
            var command = new GetAllStructureStatesQuery();
            var handler = new GetAllStructureStatesQueryHandler(_context);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}