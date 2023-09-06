using ABI.API.Structure.ACL.Truck.Application.Queries.StructureModel;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.ACLTruck.Structure
{
    [TestClass()]
    public class GetStructureModelByIdQueryTestsTests
    {

        private static StructureContext _context;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetStructureModelByIdQueryTestsTest()
        {
            var result = new GetStructureModelByIdQuery { StructureModelId = 1 };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void GetStructureModelByIdQueryConstructorTestsTest()
        {
            var result = new GetStructureModelByIdQueryHandler();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdQueryHandlerTest()
        {
            var command = new GetStructureModelByIdQuery { StructureModelId = 1 };
            var handler = new GetStructureModelByIdQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

    }
}
