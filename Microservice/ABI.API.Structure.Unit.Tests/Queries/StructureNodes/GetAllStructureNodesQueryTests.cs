using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
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
    public class GetAllStructureNodesQueryTests
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetAllStructureNodesQueryTest()
        {
            var result = new GetAllStructureNodesQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;
            result.Active = null;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.Active.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodesQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeId = 100017
                    }
                });
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeMaxVersionQuery>(), default))
                .ReturnsAsync(new List<NodeMaxVersionDTO>
                {
                    new NodeMaxVersionDTO
                    {
                        NodeId = 100017,
                        ValidityFrom = DateTimeOffset.MaxValue.Date
                    }
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodesQueryHandlerRootNodeTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeMaxVersionQuery>(), default))
                .ReturnsAsync(new List<NodeMaxVersionDTO>());
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodeRootQuery>(), default))
                .ReturnsAsync(new StructureNodeDTO
                {
                    NodeValidityFrom = null,
                    VersionType = "N"
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodesQueryHandlerNodeNewTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeId = 100017,
                        IsNew = true,
                        NodeActive = true,
                        AristaValidityFrom = DateTimeOffset.UtcNow.AddDays(1),
                        NodeValidityFrom = DateTimeOffset.UtcNow.AddDays(1)
                    }
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodesQueryHandlerNodePendingTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeId = 100017,
                        NodeActive = true,
                        AristaMotiveStateId = 2,
                        NodeMotiveStateId = 1
                    }
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodesQueryHandlerNodeActualTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeId = 100017,
                        NodeActive = true,
                        AristaMotiveStateId = 2,
                        NodeMotiveStateId = 2,
                        NodeValidityFrom = DateTimeOffset.UtcNow.AddDays(-1),
                        NodeValidityTo = DateTimeOffset.UtcNow.AddDays(1)
                    }
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        public async Task GetAllStructureNodesQueryHandlerAristaPendingTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeId = 100017,
                        NodeActive = true,
                        AristaMotiveStateId = 1
                    }
                });

            var command = new GetAllStructureNodesQuery { StructureId = 10, Active = true, ValidityFrom = DateTime.UtcNow.Date };
            var handler = new GetAllStructureNodesQueryHandler(mediatorMock.Object, _context);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}