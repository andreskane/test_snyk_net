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
    public class EditNodeCommandTests
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
        public void EditNodeCommandTest()
        {
            var result = new EditNodeCommand(1, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.NodeIdParent.Should().Be(1);
            result.StructureId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("1");
            result.Active.Should().BeTrue();
            result.AttentionModeId.Should().Be(1);
            result.RoleId.Should().Be(1);
            result.SaleChannelId.Should().Be(1);
            result.EmployeeId.Should().Be(1);
            result.LevelId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
            result.ValidityTo.Should().Be(DateTime.UtcNow.Date);
        }

        [TestMethod()]
        public void EditNodeCommandEditedPropertiesTest()
        {
            var result = new EditNodeCommand(1, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);

            result.EditName("EDITED");
            result.EditCode("E");
            result.EditRoleId(2);
            result.EditAttentionModeId(2);
            result.EditSaleChannelId(2);
            result.EditEmployeeId(2);
            result.EditLevel(1);
            result.EditActive(false);

            result.Should().NotBeNull();
            result.Name.Should().Be("EDITED");
            result.Code.Should().Be("E");
            result.RoleId.Should().Be(2);
            result.AttentionModeId.Should().Be(2);
            result.SaleChannelId.Should().Be(2);
            result.EmployeeId.Should().Be(2);
            result.Active.Should().BeFalse();
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerTest()
        {
            var command = new EditNodeCommand(100009, 100009, 3, "TEST", "1", true, 1, 1, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

             await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerNullValidityFromTest()
        {
            var command = new EditNodeCommand(100010, 100010, 6, "TEST", "1", true, 1, 1, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerFutureValidityFromTest()
        {
            var command = new EditNodeCommand(100011, 100011, 3, "TEST", "1", true, 1, 1, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerDraftFromTest()
        {
            var command = new EditNodeCommand(100012, 100012, 3, "TEST", "1", true, 1, 1, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerNullStructureRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditNodeCommandHandler(null, AddDataContext._context))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task EditNodeCommandHandlerNullCommandTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<RepositoryException>(() =>
                Task.Run(() => new EditNodeCommandHandler(_nodeRepo, AddDataContext._context).Handle(null, default))
            );

            throws.Should().BeOfType(typeof(RepositoryException));
        }

        [TestMethod()]
        public async Task EditNodeCommandRolNullHandlerDraftFromTest()
        {
            var command = new EditNodeCommand(100012, 100012, 3, "TEST", "1", true, 1, null, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolEmployeIdNullHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, 1, 1, null, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }


        [TestMethod()]
        public async Task EditNodeCommandRolNullEmployeIdHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, null, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolNullEmployeIdNullHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, null, 1, null, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolEmployeIdHandlerTest()
        {
            var command = new EditNodeCommand(100020, 100020, 11, "TEST", "1", true, 3, 1, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolEmployeIdNullEqualHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, 1, 1, null, 1, new DateTime(2020, 1, 4), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolNullEmployeIdEqualHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, null, 1, 1, 1, new DateTime(2020, 1, 2), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolNullEmployeIdNullEqualHandlerTest()
        {
            var command = new EditNodeCommand(100019, 100019, 11, "TEST", "1", true, 3, null, 1, null, 1, new DateTime(2020, 1, 3), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRolEmployeIdEqualHandlerTest()
        {
            var command = new EditNodeCommand(100020, 100020, 11, "TEST", "1", true, 3, 1, 1, 1, 1, new DateTime(2020, 1, 5), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRootHandlerDraftFromTest()
        {
            var command = new EditNodeCommand(1, null, 1, "ARGENTINA2", "1", true, 1, null, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditNodeCommandRootHandlerNewNodeTest()
        {
            var command = new EditNodeCommand(100036, null, 1, "ARGENTINA2", "1", true, 1, null, 1, 1, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.Date);
            var handler = new EditNodeCommandHandler(_nodeRepo, AddDataContext._context);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}