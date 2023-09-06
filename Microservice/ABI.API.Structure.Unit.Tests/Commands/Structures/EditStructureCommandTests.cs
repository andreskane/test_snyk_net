using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures.Tests
{
    [TestClass()]
    public class EditStructureCommandTests
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
        public void EditStructureCommandFullParametersTest()
        {
            var result = new EditStructureCommand(1, "TEST", DateTime.UtcNow.Date, "TEST");
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
        }

        [TestMethod()]
        public async Task EditStructureCommandHandlerTest()
        {
            var command = new EditStructureCommand(3, "TESTY", DateTime.UtcNow.Date, "TESTA");
            var handler = new EditStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task EditStructureCommandHandlerNullStructureRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditStructureCommandHandler(null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task EditStructureCommandCodeExceptionHandlerTest()
        {
            var command = new EditStructureCommand(12, "NODEROOTA", DateTime.UtcNow.Date, "ARG_VTA_TESTB");
            var handler = new EditStructureCommandHandler(_structureRepo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<StructureCodeExistsException>();
        }
        [TestMethod()]
        public async Task EditStructureCommandCodeLengthExceptionHandlerTest()
        {
            var command = new EditStructureCommand(12, "NODEROOTAB", DateTime.UtcNow.Date, "ARG_VTA_NODEROOTAABCD");
            var handler = new EditStructureCommandHandler(_structureRepo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeLengthException>();
        }
    }
}