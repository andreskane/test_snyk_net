using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.ACLTruck.Structure
{
    [TestClass()]
    public class GetByIdQueryTests
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
            var result = new GetByIdQuery { StructureId = 1 };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdQueryHandlerTest()
        {
            var command = new GetByIdQuery { StructureId = 1 };
            var handler = new GetByIdQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

    }
}
