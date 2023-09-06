using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetNodesToCompareQueryTests
    {
        private static IStructureNodeRepository _structureNodeRepository;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void GetNodesToCompareQueryTest()
        {
            var result = new GetNodesToCompareQuery();
            result.StructureId = 1;
            result.NodeId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetNodesToCompareQueryHandlerTest()
        {
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>
                {
                    new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, "TEST", true)
                });

            var command = new GetNodesToCompareQuery { StructureId = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, NodeId = 1 };
            var handler = new GetNodesToCompareQueryHandler(_structureNodeRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodesToCompareQueryHandlerDraftTest()
        {
            var command = new GetNodesToCompareQuery { StructureId = 10, NodeId = 100016 };
            var handler = new GetNodesToCompareQueryHandler(_structureNodeRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodesToCompareQueryHandlerDraft2Test()
        {
            var command = new GetNodesToCompareQuery { StructureId = 10, NodeId = 100037 };
            var handler = new GetNodesToCompareQueryHandler(_structureNodeRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodesToCompareQueryHandlerNewTest()
        {
            var command = new GetNodesToCompareQuery { StructureId = 9, NodeId = 100016 };
            var handler = new GetNodesToCompareQueryHandler(_structureNodeRepository);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}