using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetStructureNodesPendingQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetStructureNodesPendingQueryTest()
        {
            var result = new GetStructureNodesPendingQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetStructureNodesPendingQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO> { new NodePendingDTO { StructureId = 11, NodeId = 100018, NodeDefinitionId = 100018 } });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO> { new NodePendingDTO { StructureId = 11, NodeId = 100018, NodeDefinitionId = 100019 } });

            var command = new GetStructureNodesPendingQuery { StructureId = 11, ValidityFrom = new DateTime(2020, 1, 1) };
            var handler = new GetStructureNodesPendingQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().HaveCount(2);
        }
    }
}