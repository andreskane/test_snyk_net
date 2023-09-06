using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetCodeHintQueryHandlerTests
    {
        [TestMethod()]
        public async Task HandleTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());


            var query = new GetCodeHintQuery
            {
                StructureId = 1,
                LevelId = 1
            };
            var handler = new GetCodeHintQueryHandler(mediatrMock.Object);
            var result = await handler.Handle(query, default);

            result.Should().Be("1");
        }
    }
}