using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class NodeDTOTests
    {
        [TestMethod()]
        public void NodeDTOTest()
        {
            var result = new NodeDTO
            {
                Id = 1,
                StructureId = 1,
                NodeIdParent = null,
                Name = "TEST",
                Code = "TEST",
                LevelId = 1,
                Active = null,
                ChildNodeActiveFieldCanBeEdited = null,
                AttentionModeId = null,
                RoleId = null,
                SaleChannelId = null,
                EmployeeId = null,
                IsRootNode = null,
                ValidityFrom = null,
                ValidityTo = DateTime.MinValue,
                Nodes = null,
                AttentionMode = null,
                Role = null,
                SaleChannel = null,
                VersionType = "TEST",
                Version = null
            };

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
            result.ValidityTo.Should().Be(DateTime.MinValue);
            result.Nodes.Should().BeNull();
            result.AttentionMode.Should().BeNull();
            result.Role.Should().BeNull();
            result.SaleChannel.Should().BeNull();
            result.VersionType.Should().Be("TEST");
            result.Version.Should().BeNull();
        }
    }
}