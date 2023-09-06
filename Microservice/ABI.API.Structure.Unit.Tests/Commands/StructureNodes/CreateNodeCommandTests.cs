using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
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
    public class CreateNodeCommandTests
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
        public void CreateNodeCommandTest()
        {
            var from = new DateTime(2020, 12, 1);

            var result = new CreateNodeCommand(1, null, "TEST", "1", 1, true, null, null, null, null, from, false, null);
            result.Should().NotBeNull();
            result.Active.Should().BeTrue();
            result.AttentionModeId.Should().BeNull();
            result.Code.Should().Be("1");
            result.EmployeeId.Should().BeNull();
            result.IsRootNode.Should().BeFalse();
            result.LevelId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.NodeIdParent.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.StructureId.Should().Be(1);
            result.TypeRelation.Should().Be(RelationshipNodeType.Contains);
            result.ValidityFrom.Should().Be(from);
            result.ValidityTo.Should().Be(DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
        }

        [TestMethod()]
        public void CreateNodeCommandWithValidityTest()
        {
            var from = new DateTime(2020, 12, 1);
            var to = new DateTime(2020, 12, 2);
            var result = new CreateNodeCommand(1, null, "TEST", "1", 1, true, null, null, null, null, from, false, to);

            result.ValidityFrom.Should().Be(from);
            result.ValidityTo.Should().Be(to);
        }

        [TestMethod()]
        public async Task CreateNodeCommandHandlerTest()
        {
            var from = new DateTime(2020, 12, 1);
            var command = new CreateNodeCommand(3, 100000, "TEST", "1", 2, true, null, null, null, null, from, false, null);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task CreateNodeCommandHandlerRootNodeTest()
        {
            var from = new DateTime(2020, 12, 1);
            var command = new CreateNodeCommand(3, null, "TEST", "2", 1, true, null, null, null, null, from, true, null);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task CreateNodeCommandHandlerNullNodeRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new CreateNodeCommandHandler(null, _structureRepo))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task CreateNodeCommandHandlerNullStructureRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new CreateNodeCommandHandler(_nodeRepo, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task CreateNodeCommandHandlerThrowsRepositoryExceptionTest()
        {
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(null, default))
                .Should().ThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public async Task CreateNodeCommandRolEmployeIdNullHandlerTest()
        {
            var command = new CreateNodeCommand(3, null, "TEST3", "3",1, true, 3, 1, 1, null, new DateTime(9999, 12, 2), true, DateTimeOffset.MaxValue.Date);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }


        [TestMethod()]
        public async Task CreateNodeCommandRolNullEmployeIdHandlerTest()
        {
            var command = new CreateNodeCommand(3, null, "TEST4", "4", 1, true, 3, null, 1, 1, new DateTime(9999, 12, 2), true, DateTimeOffset.MaxValue.Date);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task CreateNodeCommandRolNullEmployeIdNullHandlerTest()
        {
            var command = new CreateNodeCommand(3, null, "TEST5", "5", 1, true, 3, null, 1, null, new DateTime(9999, 12, 2), true, DateTimeOffset.MaxValue.Date);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }


        [TestMethod()]
        public async Task CreateNodeCommandRolEmployeIdHandlerTest()
        {
            var command = new CreateNodeCommand(3, null, "TEST6", "6", 1, true, 3, 1, 1, 1, new DateTime(9999, 12, 2), true, DateTimeOffset.MaxValue.Date);
            var handler = new CreateNodeCommandHandler(_nodeRepo, _structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}