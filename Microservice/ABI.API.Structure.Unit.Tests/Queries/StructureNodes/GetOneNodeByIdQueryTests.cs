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
    public class GetOneNodeByIdQueryTests
    {
        [TestMethod()]
        public void GetOneNodeByIdQueryTest()
        {
            var result = new GetOneNodeByIdQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodeByIdQueryHandlerPendingTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO { NodeId = 100017, NodeValidityFrom = DateTime.UtcNow.Date, VersionType = "B" }
                });

            var command = new GetOneNodeByIdQuery { StructureId = 10, NodeId = 100017, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetOneNodeByIdQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeByIdQueryHandlerFutureTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO { NodeId = 100017, NodeValidityFrom = DateTime.UtcNow.Date, VersionType = "F" }
                });

            var command = new GetOneNodeByIdQuery { StructureId = 10, NodeId = 100017, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetOneNodeByIdQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeByIdQueryHandlerOneNodeTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO { NodeId = 100017, NodeValidityFrom = DateTime.UtcNow.Date }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeQuery>(), default))
                .ReturnsAsync(new DTO.StructureNodeDTO { NodeId = 1, NodeValidityFrom = DateTime.UtcNow.Date });

            var command = new GetOneNodeByIdQuery { StructureId = 10, NodeId = 100017, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetOneNodeByIdQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}