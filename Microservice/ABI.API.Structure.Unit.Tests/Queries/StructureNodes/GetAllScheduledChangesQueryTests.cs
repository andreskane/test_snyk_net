using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Net.RestClient;
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
    public class GetAllScheduledChangesQueryTests
    {
        [TestMethod()]
        public void GetAllScheduledChangesQueryTest()
        {
            var result = new GetAllScheduledChangesQuery();
            result.Id = 1;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetAllScheduledChangesQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 2 // Confirmado
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 1 // Borrador
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureExistenceDateQuery>(), default))
                .ReturnsAsync(DateTime.UtcNow.AddDays(1).Date);

            var command = new GetAllScheduledChangesQuery();
            var handler = new GetAllScheduledChangesHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetAllScheduledChangesQueryHandlerExistenceDateTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>());

            var command = new GetAllScheduledChangesQuery();
            var handler = new GetAllScheduledChangesHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task GetAllScheduledChangesQueryHandlerThrowsTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var command = new GetAllScheduledChangesQuery();
            var handler = new GetAllScheduledChangesHandler(mediatorMock.Object);
            
            await handler
                .Invoking(i => handler.Handle(command, default))
                .Should().ThrowAsync<NotFoundException>();
        }

        [TestMethod()]
        public async Task GetAllScheduledChangesQueryHandlerPendingAristaTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 2 // Confirmado
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 1, // Borrador
                        AristaMotiveStateId = 1 // Borrador
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureExistenceDateQuery>(), default))
                .ReturnsAsync(DateTime.UtcNow.AddDays(1).Date);

            var command = new GetAllScheduledChangesQuery();
            var handler = new GetAllScheduledChangesHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeEmpty();
        }
    }
}