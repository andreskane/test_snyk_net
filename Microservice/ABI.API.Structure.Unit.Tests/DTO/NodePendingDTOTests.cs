using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class NodePendingDTOTests
    {
        [TestMethod()]
        public void NodePendingDTOTest()
        {
            var result = new NodePendingDTO();
            result.StructureId = 1;
            result.AristaId = 1;
            result.NodeId = 1;
            result.NodeCode = "TEST";
            result.NodeName = "TEST";
            result.NodeLevelId = 1;
            result.NodeDefinitionId = 1;
            result.NodeParentId = 1;
            result.NodeValidityFrom = DateTimeOffset.MinValue;
            result.TypeVersion = "TEST";
            result.NodeMotiveStateId = 1;
            result.AristaMotiveStateId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.AristaId.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.NodeCode.Should().Be("TEST");
            result.NodeName.Should().Be("TEST");
            result.NodeLevelId.Should().Be(1);
            result.NodeDefinitionId.Should().Be(1);
            result.NodeParentId.Should().Be(1);
            result.NodeValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.TypeVersion.Should().Be("TEST");
            result.NodeMotiveStateId.Should().Be(1);
            result.AristaMotiveStateId.Should().Be(1);
        }
    }
}