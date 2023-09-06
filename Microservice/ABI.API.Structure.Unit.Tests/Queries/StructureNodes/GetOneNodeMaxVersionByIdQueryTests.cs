using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeMaxVersionByIdQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeMaxVersionByIdQueryTest()
        {
            var result = new GetOneNodeMaxVersionByIdQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.Validity = null;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.Validity.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeMaxVersionByIdQueryHandlerTest()
        {
            var command = new GetOneNodeMaxVersionByIdQuery { StructureId = 10, NodeId = 100017, Validity = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)) };
            var handler = new GetOneNodeMaxVersionByIdHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeMaxVersionByIdQueryHandlerDraftTest()
        {
            var command = new GetOneNodeMaxVersionByIdQuery { StructureId = 10, NodeId = 100016, Validity = new DateTime(2020, 1, 1) };
            var handler = new GetOneNodeMaxVersionByIdHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeMaxVersionByIdQueryHandlerNullTest()
        {
            var command = new GetOneNodeMaxVersionByIdQuery { StructureId = 10, NodeId = 100016, Validity = new DateTime(2020, 1, 2) };
            var handler = new GetOneNodeMaxVersionByIdHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeNull();
        }
    }
}