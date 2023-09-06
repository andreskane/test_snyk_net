using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodePendingScheduledChangesQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodePendingScheduledChangesQueryTest()
        {
            var result = new GetAllNodePendingScheduledChangesQuery();
            result.StructureId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetAllNodePendingScheduledChangesQueryHandlerTest()
        {
            var command = new GetAllNodePendingScheduledChangesQuery { StructureId = 10 };
            var handler = new GetAllNodePendingScheduledChangesQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}