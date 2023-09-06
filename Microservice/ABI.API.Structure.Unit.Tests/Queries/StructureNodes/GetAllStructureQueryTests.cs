using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
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
    public class GetAllStructureQueryTests
    {
        [TestMethod()]
        public void GetAllStructureQueryTest()
        {
            var result = new GetAllStructureQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;
            result.Active = null;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.Active.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);

            var command = new GetAllStructureQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetStructurePortalQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}