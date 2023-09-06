using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures.Tests
{
    [TestClass()]
    public class ValidateTruckIllegalCharacterCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IStructureModelRepository _modelRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            _modelRepo = new StructureModelRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void ValidateTruckIllegalCharacterCommandTest()
        {
            var result = new ValidateTruckIllegalCharacterCommand();
            result.Should().NotBeNull();
            result.StructureId.Should().Be(0);
            result.Name.Should().BeNull();
        }

        [TestMethod()]
        public async Task ValidateTruckIllegalCharacterCommandHandlerTest()
        {
            var command = new ValidateTruckIllegalCharacterCommand { StructureId = 1 };
            var handler = new ValidateTruckIllegalCharacterCommandHandler(_structureRepo);
            var result = await handler.Handle(command, default);

            result.Should().BeOfType<MediatR.Unit>();
        }

        [TestMethod()]
        public async Task ValidateTruckIllegalCharacterCommandHandlerNullNameTest()
        {
            var command = new ValidateTruckIllegalCharacterCommand { StructureId = 6 };
            var handler = new ValidateTruckIllegalCharacterCommandHandler(_structureRepo);
            var result = await handler.Handle(command, default);

            result.Should().BeOfType<MediatR.Unit>();
        }

        [TestMethod()]
        public async Task ValidateTruckIllegalCharacterCommandHandlerThrowsTruckInvalidCharacterExceptionTest()
        {
            var command = new ValidateTruckIllegalCharacterCommand { StructureId = 6, Name = "ÁÉÍÓÚ" };
            var handler = new ValidateTruckIllegalCharacterCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<TruckInvalidCharacterException>();
        }

        [TestMethod()]
        public async Task ValidateTruckIllegalCharacterCommandHandlerOkTest()
        {
            var command = new ValidateTruckIllegalCharacterCommand { StructureId = 6, Name = "ABCD" };
            var handler = new ValidateTruckIllegalCharacterCommandHandler(_structureRepo);
            var result = await handler.Handle(command, default);

            result.Should().BeOfType<MediatR.Unit>();
        }
    }
}