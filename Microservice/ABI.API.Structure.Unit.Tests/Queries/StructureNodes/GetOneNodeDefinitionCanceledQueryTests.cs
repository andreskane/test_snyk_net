using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeDefinitionCanceledQueryTests
    {
        [TestMethod()]
        public void GetOneNodeDefinitionCanceledQueryTest()
        {
            var result = new GetOneNodeDefinitionCanceledQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodeDefinitionCanceledQueryHandlerTest()
        {
            var command = new GetOneNodeDefinitionCanceledQuery { StructureId = 11, NodeId = 100021, ValidityFrom = new DateTime(2020, 1, 1) };
            var handler = new GetOneNodeCanceledQueryHandler(AddDataContext._context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}