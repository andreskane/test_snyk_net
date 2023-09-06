using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Infrastructure;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodePendingChangesWithoutSavingQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllNodePendingChangesWithoutSavingQueryTest()
        {
            var result = new GetAllNodePendingChangesWithoutSavingQuery();
            result.StructureId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetAllNodePendingChangesWithoutSavingQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();

            var command = new GetAllNodePendingChangesWithoutSavingQuery { StructureId = 10 };
            var handler = new GetAllNodePendingChangesWithoutSavingQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}