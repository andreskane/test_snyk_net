using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class NodeAristaDTOTests
    {
        [TestMethod()]
        public void NodeAristaDTOTest()
        {
            var result = new NodeAristaDTO();
            result.StructureId = 1;
            result.StructureModelID = 1;
            result.StructureModelName = "TEST";
            result.StructureModelShortName = "TEST";
            result.StructureModelDescription = "TEST";
            result.StructureModelActive = true;
            result.RootNodeId = 1;
            result.StructureValidityFrom = null;
            result.NodeId = 1;
            result.NodeParentId = null;
            result.ContainsNodeId = null;
            result.NodeName = "TEST";
            result.NodeCode = "TEST";
            result.NodeLevelId = 1;
            result.NodeLevelName = "TEST";
            result.NodeActive = null;
            result.NodeAttentionModeId = null;
            result.NodeAttentionModeName = "TEST";
            result.NodeRoleId = null;
            result.NodeRoleName = "TEST";
            result.NodeEmployeeId = null;
            result.NodeEmployeeName = "TEST";
            result.NodeSaleChannelId = null;
            result.NodeSaleChannelName = "TEST";
            result.NodeValidityFrom = null;
            result.NodeValidityTo = DateTime.MinValue;
            result.NodeDefinitionId = 1;
            result.VersionType = "TEST";
            result.NodeIdTo = null;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureModelID.Should().Be(1);
            result.StructureModelName.Should().Be("TEST");
            result.StructureModelShortName.Should().Be("TEST");
            result.StructureModelDescription.Should().Be("TEST");
            result.StructureModelActive.Should().BeTrue();
            result.RootNodeId.Should().Be(1);
            result.StructureValidityFrom.Should().BeNull();
            result.NodeId.Should().Be(1);
            result.NodeParentId.Should().BeNull();
            result.ContainsNodeId.Should().BeNull();
            result.NodeName.Should().Be("TEST");
            result.NodeCode.Should().Be("TEST");
            result.NodeLevelId.Should().Be(1);
            result.NodeLevelName.Should().Be("TEST");
            result.NodeActive.Should().BeNull();
            result.NodeAttentionModeId.Should().BeNull();
            result.NodeAttentionModeName.Should().Be("TEST");
            result.NodeRoleId.Should().BeNull();
            result.NodeRoleName.Should().Be("TEST");
            result.NodeEmployeeId.Should().BeNull();
            result.NodeEmployeeName.Should().Be("TEST");
            result.NodeSaleChannelId.Should().BeNull();
            result.NodeSaleChannelName.Should().Be("TEST");
            result.NodeValidityFrom.Should().BeNull();
            result.NodeValidityTo.Should().Be(DateTime.MinValue);
            result.NodeDefinitionId.Should().Be(1);
            result.VersionType.Should().Be("TEST");
            result.NodeIdTo.Should().BeNull();
        }
    }
}