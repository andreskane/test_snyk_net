using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Portal
{
    [TestClass()]
    public class NodePortalDTOTests
    {
        [TestMethod()]
        public void NodePortalDTOTest()
        {
            var result = new NodePortalDTO
            {
                Name = "TEST",
                Code = "TEST",
                LevelId = 1,
                AttentionModeId = null,
                RoleId = null,
                SaleChannelId = null,
                EmployeeId = null,
                NodeIdParent = null,
                IsRootNode = true,
                Active = true,
                ValidityFrom = null,
                ValidityTo = DateTimeOffset.MinValue,
                Nodes = null,
                VacantPerson = true
            };

            result.Should().NotBeNull();
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.AttentionModeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.SaleChannelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.NodeIdParent.Should().BeNull();
            result.IsRootNode.Should().BeTrue();
            result.Active.Should().BeTrue();
            result.ValidityFrom.Should().BeNull();
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
            result.VacantPerson.Should().BeTrue();
        }
    }
}