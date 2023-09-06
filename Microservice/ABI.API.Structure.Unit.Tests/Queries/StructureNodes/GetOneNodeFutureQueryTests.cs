using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeFutureQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeFutureQueryTest()
        {
            var result = new GetOneNodeFutureQuery();
            result.StructureId = 1;
            result.NodeId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetOneNodeFutureQueryHandlerTest()
        {
            var command = new GetOneNodeFutureQuery { StructureId = 10, NodeId = 100017 };
            var handler = new GetOneNodeFutureQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}