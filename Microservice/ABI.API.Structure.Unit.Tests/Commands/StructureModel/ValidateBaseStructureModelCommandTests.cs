using ABI.API.Structure.Application.Commands.StructureModel;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Service.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Commands.StructureModel
{
    [TestClass()]
    public class ValidateBaseStructureModelCommandTests
    {
        private static IStructureModelRepository _structureModelRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureModelRepo = new StructureModelRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void ValidateBaseStructureModelCommandTest()
        {
            var result = new ValidateBaseStructureModelCommand();
            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.CountryId.Should().Be(0);
            result.Name.Should().BeNull();
            result.Code.Should().BeNull();
        }

        [TestMethod()]
        public void ValidateBaseStructureModelCommandHandlerTest()
        {
            var result = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);
            result.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task ValidateStructureModelCodeExistsCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Name ="TEST999",
                Code = "VTD",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<StructureCodeExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureModelNameExistsCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Name = "NOTREMOVEABLE",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NameExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureModelNameExistsEditCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Id = 10006,
                Name = "NOTREMOVEABLE",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NameExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureModelCodeExistsEditCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Id = 10006,
                Name = "TEST999",
                Code = "VTD",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<StructureCodeExistsException>();
        }
        [TestMethod()]
        public async Task ValidateStructureModelCodeLengthCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Name = "TEST999",
                Code = "ABCD",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeLengthException>();
        }
        [TestMethod()]
        public async Task ValidateStructureModelCodeLengthPassCommandHandlerTest()
        {
            var command = new ValidateBaseStructureModelCommand
            {
                Name = "TEST999",
                Code = "ABC",
                CountryId = 1
            };
            var handler = new ValidateBaseStructureModelCommandHandler(_structureModelRepo);

            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}
