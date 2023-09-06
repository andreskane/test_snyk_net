using ABI.API.Structure.Application.Commands.Structures;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Service.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Commands.Structures
{
    [TestClass()]
    public class ValidateBaseStructureCommandTests
    {
        private static IStructureRepository _structureRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
        }
        [TestMethod()]
        public void ValidateBaseStructureCommandTest()
        {
            var result = new ValidateBaseStructureCommand();
            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().BeNull();
            result.Code.Should().BeNull();
        }

        [TestMethod()]
        public void ValidateBaseStructureCommandHandlerTest()
        {
            var result = new ValidateBaseStructureCommandHandler(_structureRepo);
            result.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task ValidateStructureNameExistsCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Name = "NODEROOTA"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NameExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureNameExistsEditCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Id = 13,
                Name = "NODEROOTA"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NameExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureCodeExistsCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Name = "NODEROOTA999",
                Code = "ARG_VTA_NODEROOTA"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<StructureCodeExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureCodeExistsEditCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Id = 13,
                Name = "NODEROOTA999",
                Code = "ARG_VTA_NODEROOTA"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<StructureCodeExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureCodeLengthCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Name = "NODEROOTA999",
                Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeLengthException>();
        }
        [TestMethod()]
        public async Task ValidateStructureCodeLengthPassCommandHandlerTest()
        {
            var command = new ValidateBaseStructureCommand
            {
                Name = "NODEROOTA999",
                Code = "ABC_ABC"
            };
            var handler = new ValidateBaseStructureCommandHandler(_structureRepo);

            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}
