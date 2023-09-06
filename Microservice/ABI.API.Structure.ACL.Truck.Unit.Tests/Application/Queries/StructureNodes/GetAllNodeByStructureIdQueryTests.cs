using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodeByStructureIdQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodeByStructureIdQueryTest()
        {
            var result = new GetAllNodeByStructureIdQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetAllNodeByStructureIdQueryHandlerTest()
        {
            //var command = new GetAllNodeByStructureIdQuery { StructureId = 10, Date = DateTimeOffset.MaxValue };
            //var handler = new GetAllNodeByStructureIdQueryHandler(null, _context);
            //var results = await handler.Handle(command, default);

            //results.Should().NotBeNull();
            Assert.Inconclusive(); // Revisar el casteo de NodeAristaDTO a PortalAristaDTO dentro del handler
        }
    }
}