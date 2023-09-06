using ABI.API.Structure.Application.DTO;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetStructureChangesWithoutSavingThereIsQueryTests
    {
        [TestMethod()]
        public void GetStructureChangesWithoutSavingThereIsQueryTest()
        {
            var result = new GetStructureChangesWithoutSavingThereIsQuery();
            result.StructureId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetStructureChangesWithoutSavingThereIsQueryHandlerAsync()
        {
            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO>());

            var command = new GetStructureChangesWithoutSavingThereIsQuery { StructureId = 1 };
            var handler = new GetStructureChangesWithoutSavingThereIsQueryHandler(mockMediatr.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }
    }
}