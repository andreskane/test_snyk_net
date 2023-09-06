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
    public class GetOneNodeQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetOneNodeQueryTest()
        {
            var result = new GetOneNodeQuery();
            result.StructureId = 1;
            result.NodeId = 1;
            result.Validity = null;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.Validity.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneNodeMaxVersionByIdQuery>(w => w.StructureId.Equals(10) && w.NodeId.Equals(100017)), default))
                .ReturnsAsync(new DTO.NodeMaxVersionDTO { NodeId = 100017, ValidityFrom = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)) });

            var command = new GetOneNodeQuery { StructureId = 10, NodeId = 100017, Validity = null };
            var handler = new GetOneNodeQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetOneNodeQueryHandlerNullTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var command = new GetOneNodeQuery();
            var handler = new GetOneNodeQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().BeNull();
        }
    }
}