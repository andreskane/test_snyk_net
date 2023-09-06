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
    public class GetThereAreChangesWithoutSavingQueryTests
    {
        private static StructureContext _context;
        private static IMediator _mediator;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediatorMock = new Mock<IMediator>();
            _mediator = mediatorMock.Object;
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodePendingScheduledChangesQueryTest()
        {
            var result = new GetThereAreChangesWithoutSavingQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetOneNodePendingScheduledChangesQueryHandlerTest()
        {
            var command = new GetThereAreChangesWithoutSavingQuery { StructureId = 10,  ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetThereAreChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeTrue();
        }

        [TestMethod()]
        public async Task GetOneNodePendingScheduledChangesQueryHandlerRootTest()
        {
            var command = new GetThereAreChangesWithoutSavingQuery { StructureId = 3, ValidityFrom = new DateTime(2021, 4, 1) };
            var handler = new GetThereAreChangesWithoutSavingQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeTrue();
        }
    }
}