using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class CreateNodeLinkCommandTests
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
        public void CreateNodeLinkCommandTest()
        {
            var result = new CreateNodeLinkCommand("3", null, "TEST", "3", 1);
            result.Should().NotBeNull();
            result.AttentionModeId.Should().BeNull();
            result.Code.Should().Be("3");
            result.LevelId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.NodeRelation.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.TypeRelationId.Should().Be(2);
            result.StructureId.Should().Be("3");
        }

        [TestMethod()]
        public void CreateNodeLinkCommandFullTest()
        {
            var result = new CreateNodeLinkCommand("3", 1, "TEST", "3", 1, 1, 1, 1);
            result.Should().NotBeNull();
            result.AttentionModeId.Should().Be(1);
            result.Code.Should().Be("3");
            result.LevelId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.NodeRelation.Should().Be(1);
            result.RoleId.Should().Be(1);
            result.SaleChannelId.Should().Be(1);
            result.TypeRelationId.Should().Be(2);
            result.StructureId.Should().Be("3");
        }

        [TestMethod()]
        public async Task CreateNodeLinkCommandHandleTest()
        {
            var command = new CreateNodeLinkCommand("3", null, "TEST", "3", 1);
            var handler = new CreateNodeLinkCommandHandler();

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NotImplementedException>();
        }
    }
}