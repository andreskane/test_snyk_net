using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeNewQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeNewQueryTest()
        {
            var result = new GetOneNodeNewQuery();
            result.StructureId = 1;
            result.NodeId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetOneNodeNewQueryHandlerTest()
        {
            var command = new GetOneNodeNewQuery { StructureId = 10, NodeId = 100016 };
            var handler = new GetOneNodeNewQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}