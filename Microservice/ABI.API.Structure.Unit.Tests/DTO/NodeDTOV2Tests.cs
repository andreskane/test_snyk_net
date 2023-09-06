using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class NodeDTOV2Tests
    {
        [TestMethod()]
        public void NodeDTOV2Test()
        {
            var result = new NodeV2DTO();
            result.Id = 1;
            result.StructureId = 1;
            result.NodeIdParent = null;
            result.Name = "TEST";
            result.Code = "TEST";
            result.LevelId = 1;
            result.Active = null;
            result.ChildNodeActiveFieldCanBeEdited = null;
            result.AttentionModeId = null;
            result.RoleId = null;
            result.SaleChannelId = null;
            result.EmployeeId = null;
            result.IsRootNode = null;
            result.ValidityFrom = null;
            result.ValidityTo = DateTimeOffset.MinValue;
            result.Nodes = null;
            result.VersionType = "TEST";
            result.Version = null;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.StructureId.Should().Be(1);
            result.NodeIdParent.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.Active.Should().BeNull();
            result.ChildNodeActiveFieldCanBeEdited.Should().BeNull();
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.IsRootNode.Should().BeNull();
            result.ValidityFrom.Should().BeNull();
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
            result.VersionType.Should().Be("TEST");
            result.Version.Should().BeNull();
        }
    }
}