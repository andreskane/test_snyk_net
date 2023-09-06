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
    public class GetStructurePendingChangesDetailQueryTests
    {
        [TestMethod()]
        public void GetStructurePendingChangesDetailQueryTest()
        {
            var result = new GetStructurePendingChangesDetailQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetStructurePendingChangesDetailQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO> { new StructureNodeDTO { NodeId = 100016, NodeMotiveStateId = 1 } });

            var command = new GetStructurePendingChangesDetailQuery { StructureId = 10, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetStructurePendingChangesDetailQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}