using ABI.API.Structure.Domain.Enums;
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
    public class GetOneNodePendingChangesWithoutSavingQueryTests
    {
        private static IMediator _mediator;
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            var mediatorMock = new Mock<IMediator>();
            _mediator = mediatorMock.Object;
        }



        [TestMethod()]
        public void GetOneNodePendingChangesWithoutSavingQueryTest()
        {
            var result = new GetOneNodePendingChangesWithoutSavingQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.Validity = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.Validity.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodePendingChangesWithoutSavingQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeMaxVersionByIdQuery>(w => w.StructureId.Equals(10) && w.NodeId.Equals(100016)), default))
                .ReturnsAsync(new DTO.NodeMaxVersionDTO());

            var command = new GetOneNodePendingChangesWithoutSavingQuery { StructureId = 10, NodeId = 100016 };
            var handler = new GetOneNodePendingChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodePendingChangesWithoutSavingQueryHandlerRootTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeRootPendingQuery>(w => w.StructureId.Equals(10)), default))
                .ReturnsAsync(new DTO.NodePendingDTO { NodeId = 100018, NodeMotiveStateId = (int)MotiveStateNode.Draft });

            var command = new GetOneNodePendingChangesWithoutSavingQuery { StructureId = 11, NodeId = 100018 };
            var handler = new GetOneNodePendingChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }



        [TestMethod()]
        public async Task GetAllNodePendingChangesWithoutSavingQueryHandlerRootTest()
        {
            var command = new GetOneNodePendingChangesWithoutSavingQuery { StructureId = 1, Validity = new DateTime(2021, 4, 10),NodeId = 1 };
            var handler = new GetOneNodePendingChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}