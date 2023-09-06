using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModelDefinition.Tests
{
    [TestClass()]
    public class DeleteCommandTests
    {
        private static IStructureModelDefinitionRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _repo = new StructureModelDefinitionRepository(AddDataContext._context);
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
            var command = new DeleteCommand(10003);
            var handler = new DeleteCommandHandler(_repo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}