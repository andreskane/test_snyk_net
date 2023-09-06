using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class NodeAristaDTOTests
    {
        [TestMethod()]
        public void NodeAristaDTOTest()
        {
            var result = new NodeAristaDTO
            {
                StructureId = 1,
                NodeId = 1,
                NodeName = "TEST",
                NodeCode = "TEST",
                NodeActive = true,
                NodeLevelId = 1,
                NodeIdParent = null,
                NodeIdTo = null,
                NodeMotiveStateId = 1,
                NodeValidityFrom = DateTimeOffset.MinValue,
                NodeValidityTo = DateTimeOffset.MinValue,
                IsNew = true,
                NodeRoleId = null,
                NodeAttentionModeId = null,
                NodeSaleChannelId = null,
                NodeVacantPerson = null,
                NodeEmployeeId = null,
                AristaMotiveStateId = null,
                AristaChildMotiveStateId = null,
                AristaValidityFrom = null,
                AristaValidityTo = null
            };

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.NodeName.Should().Be("TEST");
            result.NodeCode.Should().Be("TEST");
            result.NodeActive.Should().BeTrue();
            result.NodeLevelId.Should().Be(1);
            result.NodeIdParent.Should().BeNull();
            result.NodeIdTo.Should().BeNull();
            result.NodeMotiveStateId.Should().Be(1);
            result.NodeValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.NodeValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.IsNew.Should().BeTrue();
            result.NodeRoleId.Should().BeNull();
            result.NodeAttentionModeId.Should().BeNull();
            result.NodeSaleChannelId.Should().BeNull();
            result.NodeVacantPerson.Should().BeNull();
            result.NodeEmployeeId.Should().BeNull();
            result.AristaMotiveStateId.Should().BeNull();
            result.AristaChildMotiveStateId.Should().BeNull();
            result.AristaValidityFrom.Should().BeNull();
            result.AristaValidityTo.Should().BeNull();
        }
    }
}