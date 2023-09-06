using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Compare
{
    [TestClass()]
    public class NodePortalCompareDTOTests
    {
        [TestMethod()]
        public void NodePortalCompareDTOTest()
        {
            var result = new NodePortalCompareDTO
            {
                Name = "TEST",
                Code = "TEST",
                LevelId = 1,
                ParentNodeCode = "TEST",
                ParentNodeLevelId = null,
                EmployeeId = null,
                RoleId = null,
                AttentionModeId = null,
                IsRootNode = true,
                Active = true,
                TypeActionNode = TypeActionNode.New,
                VacantPerson = true
            };

            result.Should().NotBeNull();
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.LevelId.Should().Be(1);
            result.ParentNodeCode.Should().Be("TEST");
            result.ParentNodeLevelId.Should().BeNull();
            result.EmployeeId.Should().BeNull();
            result.RoleId.Should().BeNull();
            result.AttentionModeId.Should().BeNull();
            result.IsRootNode.Should().BeTrue();
            result.Active.Should().BeTrue();
            result.TypeActionNode.Should().Be(TypeActionNode.New);
            result.VacantPerson.Should().BeTrue();
        }
    }
}