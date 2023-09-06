using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.StructureNodes
{
    [TestClass()]
    public class GetAllSameLevelNodesByNodeTests
    {
        private static StructureContext _context;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }

        [TestMethod()]
        public async Task GetAllSameLevelNodesByNodeTest()
        {
            var command = new GetAllSameLevelNodesByNode { NodeId = 15, ValidityFrom =Convert.ToDateTime("2020-10-08") };
            var handler = new GetAllSameLevelNodesByNodeHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetAllSameLevelNodesByNodeNullTest()
        {
            var command = new GetAllSameLevelNodesByNode { NodeId = 999, ValidityFrom = Convert.ToDateTime("2020-11-20") };
            var handler = new GetAllSameLevelNodesByNodeHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeEmpty();
        }
    }
}
