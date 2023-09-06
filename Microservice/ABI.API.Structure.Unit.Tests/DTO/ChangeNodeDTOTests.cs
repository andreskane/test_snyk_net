using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class ChangeNodeDTOTests
    {
        [TestMethod()]
        public void ChangeNodeDTOTest()
        {
            var result = new ChangeNodeDTO();
            result.NodeId = 1;
            result.Name = "TEST";
            result.Code = "TEST";
            result.NodeDefinitionId = 1;
            result.LevelId = 1;
            result.Level = "TEST";
            result.Active = true;
            result.AttentionModeId = null;
            result.AttentionModeName = "TEST";
            result.RoleId = null;
            result.Role = "TEST";
            result.EmployeeId = null;
            result.SaleChannelId = null;
            result.SaleChannel = "TEST";
            result.ValidityFrom = null;
            result.ValidityTo = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.NodeDefinitionId.Should().Be(1);
            result.LevelId.Should().Be(1);
            result.Level.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.AttentionModeId.Should().BeNull();
            result.AttentionModeName.Should().Be("TEST");
            result.RoleId.Should().BeNull();
            result.Role.Should().Be("TEST");
            result.EmployeeId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.SaleChannel.Should().Be("TEST");
            result.ValidityFrom.Should().BeNull();
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
        }
    }
}