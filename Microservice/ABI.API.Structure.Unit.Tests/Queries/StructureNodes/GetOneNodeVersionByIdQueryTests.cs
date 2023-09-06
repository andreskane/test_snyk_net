using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetOneNodeVersionByIdQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeVersionByIdQueryTest()
        {
            var result = new GetOneNodeVersionByIdQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodeVersionByIdQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeQuery>(w => w.NodeId.Equals(100016)), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    StructureId = 10,
                    NodeId = 100016
                });
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeVersionPendingByNodeIdQuery>(w => w.NodeId.Equals(100016)), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    StructureId = 10,
                    NodeId = 100016,
                    VersionType = "B"
                });

            var command = new GetOneNodeVersionByIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionByIdQueryHandler(mediatorMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }


        [TestMethod()]
        public async Task GetOneNodeNewVersionByIdQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeQuery>(w => w.NodeId.Equals(100016)), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    StructureId = 10,
                    NodeId = 100016
                });
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeVersionPendingByNodeIdQuery>(w => w.NodeId.Equals(100016)), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    StructureId = 10,
                    NodeId = 100016,
                    VersionType = "N"
                });

            var command = new GetOneNodeVersionByIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionByIdQueryHandler(mediatorMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeVersionByIdQueryHandlerNullTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var command = new GetOneNodeVersionByIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionByIdQueryHandler(mediatorMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeNull();
        }
    }
}