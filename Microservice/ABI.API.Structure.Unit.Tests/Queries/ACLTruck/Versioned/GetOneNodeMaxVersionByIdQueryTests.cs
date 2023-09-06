using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.ACLTruck.Versioned
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
            var result = new GetOneNodeMaxVersionByIdQuery
            {
                StructureId = 1,
                NodeId = 1,
                ValidityFrom = null
            };

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeMaxVersionByIdQueryHandlerTest()
        {
            var command = new GetOneNodeMaxVersionByIdQuery { StructureId = 10, NodeId = 100017, ValidityFrom = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)) };
            var handler = new GetOneNodeMaxVersionByIdHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }


        [TestMethod()]
        public async Task GetOneNodeMaxVersionByIdQueryHandlerNullTest()
        {
            var command = new GetOneNodeMaxVersionByIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = new DateTime(2020, 1, 2) };
            var handler = new GetOneNodeMaxVersionByIdHandler(_context);
            var results = await handler.Handle(command, default);

            //results.Should().BeNull();
            Assert.Inconclusive();
        }
    }
}