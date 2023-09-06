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
    public class RevertAllChangesCommandTests
    {
        [TestMethod()]
        public void RevertAllChangesCommandTest()
        {
            var result = new RevertAllChangesCommand();
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerFalseTestAsync()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista>());
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>());

            var command = new RevertAllChangesCommand();
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerNoAristTestAsync()
        {
            var arist = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.MinValue);
            arist.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista> { arist });
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>());

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.MaxValue };
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerAristWithLaterVersionTestAsync()
        {
            var arist = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, DateTimeOffset.UtcNow.Date);
            arist.EditMotiveStateId(2);

            var mockLaterArist = new Mock<StructureArista>();
            mockLaterArist.SetupGet(s => s.Id).Returns(1);
            mockLaterArist.Object.EditValidityFrom(DateTimeOffset.UtcNow.Date.AddDays(-1));
            mockLaterArist.Object.EditValidityTo(DateTimeOffset.UtcNow.Date.AddDays(1));
            mockLaterArist.Object.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista> { arist, mockLaterArist.Object });
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>());

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.UtcNow.Date};
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerAristWithEarlierVersionTestAsync()
        {
            var arist = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, DateTimeOffset.UtcNow.Date);
            arist.EditMotiveStateId(2);

            var mockEarlierArist = new Mock<StructureArista>();
            mockEarlierArist.SetupGet(s => s.Id).Returns(1);
            mockEarlierArist.Object.EditValidityFrom(DateTimeOffset.UtcNow.Date.AddDays(-1));
            mockEarlierArist.Object.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista> { arist, mockEarlierArist.Object });
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition>());

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.UtcNow.Date };
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeTrue();
        }


        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerNoDefinitionsTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, "TEST", true);
            definition.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista>());
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition });

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.MaxValue };
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandlerNodeWithLaterVersionTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, "TEST", true);
            definition.EditValidityTo(DateTimeOffset.UtcNow.Date);
            definition.EditMotiveStateId(2);

            var mockLaterDefinition = new Mock<StructureNodeDefinition>();
            mockLaterDefinition.SetupGet(s => s.Id).Returns(1);
            mockLaterDefinition.Object.EditValidityFrom(DateTimeOffset.UtcNow.Date.AddDays(-1));
            mockLaterDefinition.Object.EditValidityTo(DateTimeOffset.UtcNow.Date.AddDays(1));
            mockLaterDefinition.Object.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista>());
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition, mockLaterDefinition.Object });

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.UtcNow.Date };
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task RevertAllChangesCommandHandleNodeWithEarlierVersionTestAsync()
        {
            var definition = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.UtcNow.Date, "TEST", true);
            definition.EditValidityTo(DateTimeOffset.UtcNow.Date);
            definition.EditMotiveStateId(2);

            var mockEarlierDefinition = new Mock<StructureNodeDefinition>();
            mockEarlierDefinition.SetupGet(s => s.Id).Returns(1);
            mockEarlierDefinition.Object.EditValidityFrom(DateTimeOffset.UtcNow.Date.AddDays(-1));
            mockEarlierDefinition.Object.EditMotiveStateId(2);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            mockNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUnitOfWork.Object);
            mockNodeRepo
                .Setup(s => s.GetAllAristaByNodeTo(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureArista>());
            mockNodeRepo
                .Setup(s => s.GetAllNodoDefinitionByNodeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNodeDefinition> { definition, mockEarlierDefinition.Object });

            var command = new RevertAllChangesCommand { ValidityFrom = DateTimeOffset.UtcNow.Date };
            var handler = new RevertAllChangesCommandHandler(mockNodeRepo.Object);
            var result = await handler.Handle(command, default);

            result.Should().BeTrue();
        }
    }
}