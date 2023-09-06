using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
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
            result.Active = true;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.Active.Should().BeTrue();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetAllNodeByStructureIdQueryHandlerTest()
        {
            var command = new GetAllNodeByStructureIdQuery { StructureId = 10, ValidityFrom = DateTimeOffset.MaxValue };
            var handler = new GetAllNodeByStructureIdQueryHandler(null, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}