using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Repository.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class DeleteNodeCommandTests
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
        public void DeleteNodeCommandTest()
        {
            var result = new DeleteNodeCommand(100001, null, 3, DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(1).Date);
            result.Should().NotBeNull();
            result.Id.Should().Be(100001);
            result.NodeIdParent.Should().BeNull();
            result.StructureId.Should().Be(3);
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
            result.ValidityTo.Should().Be(DateTime.UtcNow.AddDays(1).Date);
        }

        [TestMethod()]
        public void DeleteNodeCommandWithParentTest()
        {
            var result = new DeleteNodeCommand(100001, 1, 3, DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(1).Date);
            result.Should().NotBeNull();
            result.Id.Should().Be(100001);
            result.NodeIdParent.Should().Be(1);
            result.StructureId.Should().Be(3);
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
            result.ValidityTo.Should().Be(DateTime.UtcNow.AddDays(1).Date);
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerTest()
        {
            var command = new DeleteNodeCommand(100005, null, 3, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var handler = new DeleteNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerNodeNotInStructureTest()
        {
            var command = new DeleteNodeCommand(100004, null, 3, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var handler = new DeleteNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerWithNodeParentTest()
        {
            var command = new DeleteNodeCommand(100002, 100001, 3, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var handler = new DeleteNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerNullNodeRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodeCommandHandler(null, _structureRepo))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerNullStructureRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteNodeCommandHandler(_nodeRepo, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task DeleteNodeCommandHandlerThrowsRepositoryExceptionTest()
        {
            var handler = new DeleteNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(null, default))
                .Should().ThrowAsync<RepositoryException>();
        }
    }
}