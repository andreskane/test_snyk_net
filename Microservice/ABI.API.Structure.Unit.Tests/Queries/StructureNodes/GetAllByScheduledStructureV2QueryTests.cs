using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using AutoMapper;
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
    public class GetAllByScheduledStructureV2QueryTests
    {

        private static IMapper _mapper;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
        }

        [TestMethod()]
        public void GetAllByScheduledStructureQueryTest()
        {
            var result = new GetAllByScheduledStructureV2Query();
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

            var command1 = new GetAllByScheduledStructureQuery { Id = 2, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler1 = new GetAllByScheduledStructureHandler(mediatorMock.Object);
            var results1 = await handler1.Handle(command1, default);

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllByScheduledStructureQuery>(), default))
                .ReturnsAsync(results1);

            var command = new GetAllByScheduledStructureV2Query { Id = 2, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureV2Handler(mediatorMock.Object, _mapper);
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

            var command1 = new GetAllByScheduledStructureQuery { Id = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler1 = new GetAllByScheduledStructureHandler(mediatorMock.Object);
            var results1 = await handler1.Handle(command1, default);

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllByScheduledStructureQuery>(), default))
                .ReturnsAsync(results1);

            var command = new GetAllByScheduledStructureV2Query { Id = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllByScheduledStructureV2Handler(mediatorMock.Object, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}