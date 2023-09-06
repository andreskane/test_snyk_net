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
    public class DeleteStructureCommandTests
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
        public void DeleteStructureCommandTest()
        {
            var result = new DeleteStructureCommand(4);
            result.Should().NotBeNull();
            result.StructureId.Should().Be(4);
        }

        [TestMethod()]
        public async Task DeleteStructureCommandHandlerTest()
        {
            var command = new DeleteStructureCommand(4);
            var handler = new DeleteStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteStructureCommandHandlerNullStructureRepoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new DeleteStructureCommandHandler(null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }
    }
}