using ABI.API.Structure.Application.DTO;
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
    public class GetThereAreScheduledChangesQueryTests
    {
        [TestMethod()]
        public void GetThereAreScheduledChangesQueryTest()
        {
            var result = new GetThereAreScheduledChangesQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
        }

        [TestMethod()]
        public async Task GetThereAreScheduledChangesQueryHandlerAsync()
        {
            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>());

            var model = new GetThereAreScheduledChangesQuery { StructureId = 1, ValidityFrom = DateTimeOffset.UtcNow.Date };
            var handler = new GetThereAreScheduledChangesQueryHandler(mockMediatr.Object);
            var results = await handler.Handle(model, default);

            results.Should().BeFalse();
        }
    }
}