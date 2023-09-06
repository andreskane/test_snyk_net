using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Domain.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class RevertChangeCommandTests
    {
        [TestMethod()]
        public void RevertChangeCommandTest()
        {
            var result = new RevertChangeCommand();
            result.ObjectType = Domain.Enums.ChangeTrackingObjectType.Node;
            result.NodeId = 1;
            result.Field = "TEST";
            result.Value = "TEST";
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;

            result.Should().NotBeNull();
            result.ObjectType.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.Field.Should().Be("TEST");
            result.Value.Should().Be("TEST");
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeNoDefinitionsTestAsync()
        {
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>());
            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeNoDefinitionTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeAttentionModeTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "AttentionMode",
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeAttentionModeNullTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "AttentionMode",
                Value = "",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeEmployeeTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "EmployeeId",
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeEmployeeNullTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "EmployeeId",
                Value = "",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeNameTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "Name",
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeActiveTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "Active",
                Value = "TRUE",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeRoleTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "Role",
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeRoleNullTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "Role",
                Value = "",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeSaleChannelTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "SaleChannel",
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerNodeSaleChannelNullTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MaxValue, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Node,
                NodeId = 1,
                Field = "SaleChannel",
                Value = "",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }


        [TestMethod()]
        public async Task RevertChangeCommandHandlerAristNoAristsTestAsync()
        {
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista>());
            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Arista
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerAristNoAristTestAsync()
        {
            var arist = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.MaxValue);
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista> { arist });
            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Arista
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task RevertChangeCommandHandlerAristTestAsync()
        {
            var arist = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.MaxValue);
            arist.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista> { arist });
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);

            var command = new RevertChangeCommand
            {
                ObjectType = Domain.Enums.ChangeTrackingObjectType.Arista,
                Value = "1",
                ValidityFrom = DateTimeOffset.MaxValue
            };
            var handler = new RevertChangeCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }
    }
}