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
    public class GetAllByScheduledStructureQueryTests
    {
        [TestMethod()]
        public void GetAllByScheduledStructureQueryTest()
        {
            var result = new GetAllByScheduledStructureQuery();
            result.Id = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;
            result.Active = null;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.Active.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerActualNodesTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("TEST", 1, 1, DateTime.UtcNow.Date));
            mediatorMock
                .Setup(s => s.Send(It.Is<GetAllStructureNodesQuery>(w => w.ValidityFrom.Equals(DateTime.UtcNow.Date)), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());
            mediatorMock
                .Setup(s => s.Send(It.Is<GetAllStructureNodesQuery>(w => w.ValidityFrom.Equals(DateTime.UtcNow.Date.AddDays(1))), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new StructureNodeDTO
                    {
                        NodeId = 1,
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 2 // Confirmado
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeId = 1,
                        NodeValidityFrom = DateTime.UtcNow.Date.AddDays(1),
                        NodeMotiveStateId = 2 // Confirmado
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO> { new StructureNodeDTO { NodeId = 1 } });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>());

            var command = new GetAllByScheduledStructureQuery { Id = 2, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerPendigNodesTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("TEST", 1, 1, DateTime.UtcNow.Date));
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new StructureNodeDTO
                    {
                        NodeId = 1,
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
                        NodeId = 1,
                        NodeValidityFrom = DateTime.UtcNow.Date,
                        NodeMotiveStateId = 1 // Borrador
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO> { new StructureNodeDTO { NodeId = 1 } });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);

            var command = new GetAllByScheduledStructureQuery { Id = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerPendigAristasTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("TEST", 1, 1, DateTime.UtcNow.Date));
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new StructureNodeDTO
                    {
                        NodeId = 1,
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
                        NodeId = 1,
                        NodeValidityFrom = DateTime.UtcNow.Date.AddDays(1),
                        NodeMotiveStateId = 1, // Borrador
                        AristaValidityFrom = DateTime.UtcNow.Date.AddDays(1),
                        AristaMotiveStateId = 1 // Borrador
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO> { new StructureNodeDTO { NodeId = 1 } });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);

            var command = new GetAllByScheduledStructureQuery { Id = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }


        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerThrowsNotFoundTest()
        {
            var mediatorMock = new Mock<IMediator>();
            var command = new GetAllByScheduledStructureQuery { Id = 2, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);

            await handler
                .Invoking(i => i.Handle(command, default))
                .Should().ThrowAsync<NotFoundException>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerThrowsNotFoundTwoTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("TEST", 1, 1, DateTime.UtcNow.Date));

            var command = new GetAllByScheduledStructureQuery { Id = 2, Active = true, ValidityFrom = DateTimeOffset.MinValue };
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);

            await handler
                .Invoking(i => i.Handle(command, default))
                .Should().ThrowAsync<NotFoundException>();
        }


        [TestMethod()]
        public async Task GetAllByScheduledStructureQueryHandlerPendingChangesDateTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("TEST", 1, 1, DateTime.UtcNow.Date));

            var date = DateTimeOffset.MaxValue;

            mediatorMock
             .Setup(s => s.Send(It.IsAny<GetPendingChangesDateQuery>(), default))
             .ReturnsAsync(date.AddDays(-1));

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<NodePendingDTO>
                {
                    new NodePendingDTO
                    {
                        NodeId = 1,
                        NodeValidityFrom = date,
                        NodeMotiveStateId = 1 // Borrador
                    }
                });

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);
            mediatorMock
         .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
         .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new StructureNodeDTO
                    {
                        NodeId = 1,
                        NodeValidityFrom = DateTimeOffset.UtcNow.Date,
                        NodeMotiveStateId = 2, // Confirmado
                        NodeName = "TEST"
                    }
                });

            mediatorMock
            .Setup(s => s.Send(It.IsAny<GetAllNodePendingScheduledChangesQuery>(), default))
            .ReturnsAsync(new List<NodePendingDTO>
            {
                    new NodePendingDTO
                    {
                        NodeId = 1,
                        NodeValidityFrom = date,
                        NodeMotiveStateId = 2, // Confirmado
                         NodeName = "TEST"
                    }
            });

            mediatorMock
               .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
               .ReturnsAsync(new List<StructureNodeDTO> { new StructureNodeDTO { NodeId = 1 } });

            var command = new GetAllByScheduledStructureQuery { Id = 2, Active = true, ValidityFrom = DateTimeOffset.UtcNow.Date};
            var handler = new GetAllByScheduledStructureHandler(mediatorMock.Object);

            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}