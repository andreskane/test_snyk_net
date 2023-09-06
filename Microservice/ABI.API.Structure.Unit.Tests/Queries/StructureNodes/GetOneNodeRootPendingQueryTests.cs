using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeRootPendingQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeRootPendingQueryTest()
        {
            var result = new GetOneNodeRootPendingQuery();
            result.StructureId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetOneNodeRootPendingQueryHandlerTest()
        {
            var command = new GetOneNodeRootPendingQuery { StructureId = 11 };
            var handler = new GetOneNodeRootPendingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeRootPendingQueryHandlerNullTest()
        {
            var command = new GetOneNodeRootPendingQuery { StructureId = -1 };
            var handler = new GetOneNodeRootPendingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeNull();
        }
    }
}