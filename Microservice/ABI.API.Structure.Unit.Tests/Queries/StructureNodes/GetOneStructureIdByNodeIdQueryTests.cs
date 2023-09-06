using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneStructureIdByNodeIdQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneStructureIdByNodeIdQueryTest()
        {
            var result = new GetOneStructureIdByNodeIdQuery();
            result.NodeId = 1;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetOneStructureIdByNodeIdQueryHandlerTestAsync()
        {

            var command = new GetOneStructureIdByNodeIdQuery { NodeId = 1 };
            var handler = new GetOneStructureIdByNodeIdQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}