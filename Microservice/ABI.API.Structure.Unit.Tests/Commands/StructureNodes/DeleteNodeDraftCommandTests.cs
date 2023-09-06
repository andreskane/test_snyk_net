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
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class DeleteNodeDraftCommandTests
    {
        private static IStructureNodeRepository _nodeRepo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void DeleteNodeDraftCommandCostructorTest()
        {
            var result = new DeleteNodeDraftCommand();
            result.Should().NotBeNull();
        }


        [TestMethod()]
        public void DeleteNodeDraftCommandTest()
        {
            var result = new DeleteNodeDraftCommand(1, 1, DateTimeOffset.MinValue.Date);
            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeDefinitionId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue.Date);
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new DTO.NodePendingDTO
                {
                    TypeVersion = "B"
                });

            var command = new DeleteNodeDraftCommand(100007, 3,DateTimeOffset.MinValue);
            var handler = new DeleteNodeDraftCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerThrowsRepositoryExceptionTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var handler = new DeleteNodeDraftCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(null, default))
                .Should().ThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerNullNodeRepoTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodeDraftCommandHandler(null, mediatrMock.Object))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerNullMediatrTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodeDraftCommandHandler(_nodeRepo, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerNewTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodePendingChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(new DTO.NodePendingDTO
                {
                    TypeVersion = "B"
                });

            var command = new DeleteNodeDraftCommand(100032, 7, Convert.ToDateTime("2021-05-29"));
            var handler = new DeleteNodeDraftCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodeDraftCommandHandlerCancelledTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeDefinitionCanceledQuery>(), default))
                .ReturnsAsync(new DTO.StructureNodeDTO
                {
                    NodeDefinitionId = 100033
                });

            var command = new DeleteNodeDraftCommand(100034, 7, Convert.ToDateTime("2021-05-29"));
            var handler = new DeleteNodeDraftCommandHandler(_nodeRepo, mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}