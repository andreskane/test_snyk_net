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
    public class GetOneNodeVersionPendingByNodeIdQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeVersionPendingByNodeIdQueryTest()
        {
            var result = new GetOneNodeVersionPendingByNodeIdQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodeVersionPendingByNodeIdQueryHandlerDraftTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetOneNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new DTO.NodePendingDTO { NodeDefinitionId = 100016, TypeVersion = "T" });

            var commad = new GetOneNodeVersionPendingByNodeIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionPendingByNodeIdQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(commad, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeVersionPendingByNodeIdQueryHandlerConfirmedTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetOneNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new DTO.NodePendingDTO { NodeDefinitionId = 100016, TypeVersion = "T" });

            var commad = new GetOneNodeVersionPendingByNodeIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionPendingByNodeIdQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(commad, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeVersionPendingByNodeIdQueryHandlerNullTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var commad = new GetOneNodeVersionPendingByNodeIdQuery { StructureId = 10, NodeId = 100016, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetOneNodeVersionPendingByNodeIdQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(commad, default);

            results.Should().NotBeNull();
            results.VersionType.Should().BeEmpty();
        }
    }
}