using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class NodePortalTruckDTOTests
    {
        [TestMethod()]
        public void NodePortalTruckDTOTest()
        {
            var result = new NodePortalTruckDTO
            {
                NodeId = 1,
                Name = "TEST",
                Code = "TEST",
                LevelId = 1,
                AttentionModeId = null,
                RoleId = null,
                SaleChannelId = null,
                EmployeeId = null,
                NodeIdParent = null,
                IsRootNode = null,
                ActiveNode = true,
                ValidityFrom = null,
                ValidityTo = DateTimeOffset.MinValue,
                ParentNodeCode = "TEST",
                ChildNodeCode = "TEST",
                NodeIdTo = null,
                NodeDefinitionId = null,
                ChildNodes = null
            };

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.NodeIdParent.Should().BeNull();
            result.IsRootNode.Should().BeNull();
            result.ActiveNode.Should().BeTrue();
            result.ValidityFrom.Should().BeNull();
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.ParentNodeCode.Should().Be("TEST");
            result.ChildNodeCode.Should().Be("TEST");
            result.NodeIdTo.Should().BeNull();
            result.NodeDefinitionId.Should().BeNull();
            result.ChildNodes.Should().BeNull();
        }
    }
}