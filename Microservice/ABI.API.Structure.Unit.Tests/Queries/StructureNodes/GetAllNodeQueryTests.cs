using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodeQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodeQueryTest()
        {
            var result = new GetAllNodeQuery();
            result.StructureId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetAllNodeQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var command = new GetAllNodeQuery { StructureId = 10 };
            var handler = new GetAllNodeQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllNodeQueryHandlerRootNodeTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodeRootQuery>(), default))
                .ReturnsAsync(new DTO.StructureNodeDTO());

            var command = new GetAllNodeQuery { StructureId = -1 };
            var handler = new GetAllNodeQueryHandler(_context, mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}