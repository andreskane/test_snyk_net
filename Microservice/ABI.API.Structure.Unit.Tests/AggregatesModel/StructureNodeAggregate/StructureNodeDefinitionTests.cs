using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.Tests
{
    [TestClass()]
    public class StructureNodeDefinitionTests
    {
        [TestMethod()]
        public void StructureNodeDefinitionCreateTest()
        {
            var result = new StructureNodeDefinition(null, 1, 1, 1, 1, DateTimeOffset.MinValue, null, true);
            result.EditValidityTo(DateTime.UtcNow.Date);
            result.EditMotiveStateId(1);
            result.EditVacantPerson(true);


            result.Should().NotBeNull();
            result.Active.Should().BeTrue();
            result.AttentionMode.Should().BeNull();
            result.AttentionModeId.Should().Be(1);
            result.EmployeeId.Should().Be(1);
            result.Name.Should().BeNull();
            result.Node.Should().BeNull();
            result.NodeId.Should().Be(0);
            result.Role.Should().BeNull();
            result.RoleId.Should().Be(1);
            result.SaleChannel.Should().BeNull();
            result.SaleChannelId.Should().Be(1);
            result.ValidityTo.Should().Be(DateTime.UtcNow.Date);
            result.MotiveStateId.Should().Be(1);
            result.VacantPerson.Should().BeTrue();
            result.MotiveState.Should().BeNull();

        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateTwoTest()
        {
            var result = new StructureNodeDefinition(1, 1, 1, 1, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateThreeTest()
        {
            var result = new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateFourTest()
        {
            var result = new StructureNodeDefinition(attentionModeId: 1, null, null, null, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateFiveTest()
        {
            var result = new StructureNodeDefinition(attentionModeId: 0, 0, 0, 0, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateSixTest()
        {
            var result = new StructureNodeDefinition(nodeId: 1, 0, 0, 0, 0, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateSevenTest()
        {
            var result = new StructureNodeDefinition(node: null, 0, 0, 0, 0, DateTimeOffset.MinValue, null, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateEightTest()
        {
            var result = new StructureNodeDefinition(attentionMode: null, null, null);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureNodeDefinitionCreateNineTest()
        {
            var result = new StructureNodeDefinition(null, null, null, null);
            result.Should().NotBeNull();
        }
    }
}