using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureBranchNodeDTOTests
    {
        [TestMethod()]
        public void StructureBranchNodeDTOTest()
        {
            var result = new StructureBranchNodeDTO
            {
                Id = 1,
                StructureId = 1,
                NodeIdParent = null,
                Name = "TEST",
                Code = "TEST",
                LevelId = 1,
                Active = null,
                AttentionModeId = null,
                RoleId = null,
                SaleChannelId = null,
                EmployeeId = null,
                IsRootNode = null,
                ValidityFrom = DateTimeOffset.MinValue,
                ValidityTo = DateTimeOffset.MinValue,
                Nodes = null,
                AttentionMode = null,
                Role = null,
                SaleChannel = null
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.StructureId.Should().Be(1);
            result.NodeIdParent.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.Active.Should().BeNull();
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.IsRootNode.Should().BeNull();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
            result.AttentionMode.Should().BeNull();
            result.Role.Should().BeNull();
            result.SaleChannel.Should().BeNull();
        }
    }
}