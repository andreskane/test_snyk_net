using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModel.Tests
{
    [TestClass()]
    public class DeleteCommandTests
    {
        private static IStructureModelRepository _repo;
        private static IStructureModelDefinitionRepository _structureModelDefinitionRepository;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _repo = new StructureModelRepository(AddDataContext._context);
            _structureModelDefinitionRepository = new StructureModelDefinitionRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void DeleteCommandTest()
        {
            var result = new DeleteCommand(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerTest()
        {
            var command = new DeleteCommand(10001);
            var handler = new DeleteCommandHandler(_repo, _structureModelDefinitionRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod()]
        public async Task DeleteCommandHandlerStructureTest()
        {
            var command = new DeleteCommand(10008);
            var handler = new DeleteCommandHandler(_repo, _structureModelDefinitionRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}