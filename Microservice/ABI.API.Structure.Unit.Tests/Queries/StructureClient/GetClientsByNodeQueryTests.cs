using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

using AutoMapper;

using FluentAssertions;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.StructureClient
{
    [TestClass()]
    public class GetClientsByNodeQueryTests
    {
        private static IStructureNodeRepository _structureNodeRepository;
        private static IMediator _mediator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.Is<GetOneNodeClientQuery>(p => p.NodeId.Equals(3)), default))
                .ReturnsAsync(new List<Application.DTO.StructureClientDTO>() { 
                    new Application.DTO.StructureClientDTO
                    {
                        NodeId = 3,
                        ClientId = "1"
                    }
                });

            mediator
                .Setup(s => s.Send(It.Is<GetOneNodeClientQuery>(p => p.NodeId.Equals(100)), default))
                .ReturnsAsync(new List<Application.DTO.StructureClientDTO>());

            mediator
                .Setup(s => s.Send(It.Is<GetAllAristaQuery>(p => p.NodeId.Equals(5)), default))
                .ReturnsAsync(new List<StructureArista>()
                {
                    new StructureArista(1,5,1,3,1, Convert.ToDateTime("2020-10-08"), Convert.ToDateTime("9999-12-31T20:59:59.9999999-03:00"))
                });

            mediator
                .Setup(s => s.Send(It.Is<GetAllNodeClientQuery>(p => p.ValidityFrom.Equals(Convert.ToDateTime("2021-07-30"))), default))
                .ReturnsAsync(new List<Application.DTO.StructureClientDTO>() {
                    new Application.DTO.StructureClientDTO
                    {
                        NodeId = 3,
                        ClientId = "1"
                    }
                });

            mediator
                .Setup(s => s.Send(It.Is<GetAllAristaQuery>(p => p.NodeId.Equals(87)), default))
                .ReturnsAsync(new List<StructureArista>()
                {
                    new StructureArista(1,87,1,100,1, Convert.ToDateTime("2020-10-08"), Convert.ToDateTime("9999-12-31T20:59:59.9999999-03:00"))
                });

            mediator
                .Setup(s => s.Send(It.Is<GetAllNodeClientQuery>(p => p.ValidityFrom.Equals(Convert.ToDateTime("2021-07-29"))), default))
                .ReturnsAsync(new List<Application.DTO.StructureClientDTO>());

            _mediator = mediator.Object;
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void GetClientsByNodeQueryTest()
        {
            var result = new GetClientsByNodeQuery { LevelId = 1, NodeCode = "TEST", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-30") };
            result.Should().NotBeNull();
            result.LevelId.Should().Be(1);
            result.StructureId.Should().Be(1);
            result.NodeCode.Should().Be("TEST");
            result.ValidityFrom.Should().Be(Convert.ToDateTime("2021-07-30"));
        }

        [TestMethod()]
        public async Task GetClientsByNodeQueryHandlerEmptyNodeTest()
        {
            var command = new GetClientsByNodeQuery { LevelId = 1, NodeCode = "TEST", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-30") };
            var handler = new GetClientsByNodeQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);
            result.Should().NotBeNull();
            result.Quantity.Should().Be(0);
            result.Clients.Should().BeEmpty();
        }

        [TestMethod()]
        public async Task GetClientsByNodeQueryHandlerLevel8Test()
        {
            var command = new GetClientsByNodeQuery { LevelId = 8, NodeCode = "71220", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-30") };
            var handler = new GetClientsByNodeQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);
            result.Should().NotBeNull();
            result.Quantity.Should().Be(1);
            result.Clients.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetClientsByNodeQueryHandlerLevel8EmptyTest()
        {
            var command = new GetClientsByNodeQuery { LevelId = 8, NodeCode = "72120", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-30") };
            var handler = new GetClientsByNodeQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);
            result.Should().NotBeNull();
            result.Quantity.Should().Be(0);
            result.Clients.Should().BeEmpty();
        }

        [TestMethod()]
        public async Task GetClientsByNodeQueryHandlerOtherLevelTest()
        {
            var command = new GetClientsByNodeQuery { LevelId = 7, NodeCode = "0861", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-30") };
            var handler = new GetClientsByNodeQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);
            result.Should().NotBeNull();
            result.Quantity.Should().Be(1);
            result.Clients.Should().BeEmpty();
        }

        [TestMethod()]
        public async Task GetClientsByNodeQueryHandlerOtherLevelEmptyTest()
        {
            var command = new GetClientsByNodeQuery { LevelId = 7, NodeCode = "0721", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-07-29") };
            var handler = new GetClientsByNodeQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);
            result.Should().NotBeNull();
            result.Quantity.Should().Be(0);
            result.Clients.Should().BeEmpty();
        }
    }
}
