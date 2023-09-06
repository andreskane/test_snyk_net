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
    public class GetAllNodePendingChangesWithoutSavingQueryTests
    {
        private static IMediator _mediator;
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeRootPendingQuery>(), default))
                .ReturnsAsync(new DTO.NodePendingDTO());

            _mediator = mediatorMock.Object;
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodePendingChangesWithoutSavingQueryTest()
        {
            var result = new GetAllNodePendingChangesWithoutSavingQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetAllNodePendingChangesWithoutSavingQueryHandlerTest()
        {
            var command = new GetAllNodePendingChangesWithoutSavingQuery { StructureId = 9, ValidityFrom = new DateTime(2020, 1, 1) };
            var handler = new GetAllNodePendingChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllNodePendingChangesWithoutSavingQueryHandlerRootTest()
        {
            var command = new GetAllNodePendingChangesWithoutSavingQuery { StructureId = 1, ValidityFrom = new DateTime(2021, 4, 8) };
            var handler = new GetAllNodePendingChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}