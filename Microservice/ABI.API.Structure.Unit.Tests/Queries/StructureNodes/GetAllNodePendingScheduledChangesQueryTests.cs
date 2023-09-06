using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodePendingScheduledChangesQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodePendingScheduledChangesQueryTest()
        {
            var result = new GetAllNodePendingScheduledChangesQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetAllNodePendingScheduledChangesQueryHandlerTest()
        {
            var command = new GetAllNodePendingScheduledChangesQuery { StructureId = 9, ValidityFrom = new DateTime(2019, 12, 31) };
            var handler = new GetAllNodePendingScheduledChangesQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }


        [TestMethod()]
        public async Task GetAllNodePendingScheduledChangesQueryHandlerRootTest()
        {
            var command = new GetAllNodePendingScheduledChangesQuery { StructureId = 3, ValidityFrom = new DateTime(2020, 4, 1) };
            var handler = new GetAllNodePendingScheduledChangesQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}