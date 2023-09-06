using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureNodeDTOTests
    {
        [TestMethod()]
        public void StructureNodeDTOTest()
        {
            var result = new StructureNodeDTO
            {
                StructureId = 1,
                StructureModelID = 1,
                StructureModelName = "TEST",
                StructureModelShortName = "TEST",
                StructureModelDescription = "TEST",
                StructureModelActive = true,
                RootNodeId = 1,
                StructureValidityFrom = null,
                NodeId = 1,
                NodeParentId = null,
                ContainsNodeId = null,
                NodeName = "TEST",
                NodeCode = "TEST",
                NodeLevelId = 1,
                NodeLevelName = "TEST",
                NodeActive = null,
                NodeAttentionModeId = null,
                NodeAttentionModeName = "TEST",
                NodeRoleId = null,
                NodeRoleName = "TEST",
                NodeEmployeeId = null,
                NodeEmployeeName = "TEST",
                NodeSaleChannelId = null,
                NodeSaleChannelName = "TEST",
                NodeValidityFrom = null,
                NodeValidityTo = DateTimeOffset.MinValue,
                NodeDefinitionId = 1,
                VersionType = "TEST",
                NodeMotiveStateId = 1,
                AristaMotiveStateId = null,
                AristaChildMotiveStateId = null,
                AristaValidityFrom = null,
                AristaValidityTo = null,
                IsNew = true,
                Clients = null
            };

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
            result.NodeValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.NodeDefinitionId.Should().Be(1);
            result.VersionType.Should().Be("TEST");
            result.NodeMotiveStateId.Should().Be(1);
            result.AristaMotiveStateId.Should().BeNull();
            result.AristaChildMotiveStateId.Should().BeNull();
            result.AristaValidityFrom.Should().BeNull();
            result.AristaValidityTo.Should().BeNull();
            result.IsNew.Should().BeTrue();
            result.Clients.Should().BeNull();
        }
    }
}