using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodePendingScheduledChangesQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodePendingScheduledChangesQueryTest()
        {
            var result = new GetOneNodePendingScheduledChangesQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodePendingScheduledChangesQueryHandlerTest()
        {
            var command = new GetOneNodePendingScheduledChangesQuery { StructureId = 10, NodeId = 100017, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodePendingScheduledChangesQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodePendingScheduledChangesQueryHandlerRootTest()
        {
            var command = new GetOneNodePendingScheduledChangesQuery { StructureId = 3, NodeId = 100000, ValidityFrom = new DateTime(2021, 4, 1) };
            var handler = new GetOneNodePendingScheduledChangesQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}