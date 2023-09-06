using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Repository.Exceptions;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class DeleteNodesChangesWithoutSavingCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void DeleteNodesChangesWithoutSavingCommandTest()
        {
            var result = new DeleteNodesChangesWithoutSavingCommand(3, DateTimeOffset.MinValue);
            result.Should().NotBeNull();
            result.StructureId.Should().Be(3);
        }

        [TestMethod()]
        public async Task DeleteNodesChangesWithoutSavingCommandHandlerTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        NodeDefinitionId = 100012,
                        NodeId = 100012,
                        NodeMotiveStateId = 1,
                        AristaMotiveStateId = 1
                    }
                });
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeDefinitionCanceledQuery>(), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    NodeDefinitionId = 100030
                });

            var command = new DeleteNodesChangesWithoutSavingCommand(3, new DateTime(2021, 1, 1));
            var handler = new DeleteNodesChangesWithoutSavingCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodesChangesWithoutSavingCommandHandlerThrowsRepositoryExceptionTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var handler = new DeleteNodesChangesWithoutSavingCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(null, default))
                .Should().ThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public async Task DeleteNodesChangesWithoutSavingCommandHandlerNullStructureRepoTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodesChangesWithoutSavingCommandHandler(null, mediatrMock.Object))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteNodesChangesWithoutSavingCommandHandlerNullMediatrTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodesChangesWithoutSavingCommandHandler(_nodeRepo, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }
    }
}