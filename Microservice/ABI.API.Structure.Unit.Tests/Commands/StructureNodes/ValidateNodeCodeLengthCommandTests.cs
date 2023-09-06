using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Net.RestClient;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class ValidateNodeCodeLengthCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IStructureModelRepository _structureModelRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            _structureModelRepo = new StructureModelRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void ValidateNodeCodeLengthCommandTest()
        {
            var result = new ValidateNodeCodeLengthCommand();
            result.Should().NotBeNull();
            result.StructureId.Should().Be(0);
            result.LevelId.Should().Be(0);
            result.Code.Should().BeNull();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerTest()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "123",
                LevelId = 1
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerNotFoundStructureTest()
        {
            var command = new ValidateNodeCodeLengthCommand { StructureId = 999999 };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NotFoundException>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerLevel6Test()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "123",
                LevelId = 6
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerLevel7Test()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "ABCD",
                LevelId = 7
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerLevel8Test()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "12345",
                LevelId = 8
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerMustBeNumericTest()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "ABC",
                LevelId = 1
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeNotNumericException>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeLengthCommandHandlerMustBeCorrectLengthTest()
        {
            var command = new ValidateNodeCodeLengthCommand
            {
                StructureId = 6,
                Code = "ABCDE",
                LevelId = 7
            };
            var handler = new ValidateNodeCodeLengthCommandHandler(_structureRepo, _structureModelRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeLengthException>();
        }
    }
}
