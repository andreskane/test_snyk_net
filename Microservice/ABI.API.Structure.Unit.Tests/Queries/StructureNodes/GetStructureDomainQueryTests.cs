using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetStructureDomainQueryTests
    {

        private static StructureContext _context;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetByIdQueryTest()
        {
            var result = new GetStructureDomainQuery { StructureId = 1 };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdQueryHandlerTest()
        {
            var command = new GetStructureDomainQuery { StructureId = 1 };
            var handler = new GetStructureDomainQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

    }
}
