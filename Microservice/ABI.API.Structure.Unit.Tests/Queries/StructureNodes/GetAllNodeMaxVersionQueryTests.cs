using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodeMaxVersionQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodeMaxVersionQueryTest()
        {
            var result = new GetAllNodeMaxVersionQuery();
            result.ValidityFrom = DateTimeOffset.MinValue;
            result.Nodes = null;

            result.Should().NotBeNull();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetAllNodeMaxVersionQueryHandlerTest()
        {
            //var command = new GetAllNodeMaxVersionQuery { Date = DateTimeOffset.MaxValue, Nodes = new List<int> { 100017 } };
            //var handler = new GetAllNodeMaxVersionQueryHandler(_context);
            //var results = await handler.Handle(command, default);

            //results.Should().NotBeNull();
            Assert.Inconclusive();
        }
    }
}