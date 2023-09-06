using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Extension.Tests
{
    [TestClass()]
    public class StructureNodeDTOExtensionsTests
    {
        [TestMethod()]
        public void ToNodeDTOTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeDTO(
                new StructureNodeDTO
                {
                    NodeId = 1
                },
                DateTimeOffset.MaxValue
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToNodeDTOItemNullTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeDTO(null, 1 , 1);

            result.Should().BeNull();


            var result2 = StructureNodeDTOExtensions.ToNodeDTO(new StructureNodeDTO(), 1, 1);

            result2.Should().NotBeNull();
        }
        
        

        [TestMethod()]
        public void ToNodeDTOTwoTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeDTO(
                new StructureNodeDTO
                {
                    NodeId = 1,
                    StructureId = 1,
                    NodeName = "TEST",
                    NodeCode = "1",
                    NodeLevelId = 1,
                    NodeActive = true,
                    NodeAttentionModeId = 1,
                    NodeAttentionModeName = "TEST",
                    NodeRoleId = 1,
                    NodeRoleName = "TEST",
                    NodeSaleChannelId = 1,
                    NodeSaleChannelName = "TEST",
                    NodeEmployeeId = 1,
                    NodeEmployeeName = "TEST",
                    RootNodeId = 1,
                    NodeValidityFrom = DateTime.UtcNow.Date,
                    NodeValidityTo = DateTime.UtcNow.Date,
                    VersionType = "N"
                },
                1,
                1
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToNodeDTOThreeTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeDTO(
                new StructureNodeDTO
                {
                    NodeId = 1,
                    StructureId = 1,
                    NodeName = "TEST",
                    NodeCode = "1",
                    NodeLevelId = 1,
                    NodeActive = true,
                    NodeAttentionModeId = null,
                    NodeRoleId = null,
                    NodeSaleChannelId = null,
                    NodeEmployeeId = 1,
                    NodeEmployeeName = "TEST",
                    RootNodeId = 1,
                    NodeValidityFrom = DateTime.UtcNow.Date,
                    NodeValidityTo = DateTime.UtcNow.Date,
                    VersionType = "N"
                },
                1,
                1
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, ContainsNodeId = 2 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                DateTimeOffset.MaxValue,
                1,
                1,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOTwoTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>(),
                DateTimeOffset.MaxValue,
                0,
                1,
                true
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOThreeTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>(),
                DateTimeOffset.MaxValue,
                1,
                1,
                true
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOFourTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, ContainsNodeId = 2, IsNew = true, VersionType = "N", AristaMotiveStateId = 2, NodeMotiveStateId = 2 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                DateTimeOffset.MaxValue,
                1,
                1,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOFiveTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, ContainsNodeId = 2, VersionType = "B", AristaMotiveStateId = 1 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                DateTimeOffset.MaxValue,
                1,
                1,
                true
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOSixTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, VersionType = "F", AristaMotiveStateId = 2, NodeMotiveStateId = 2, AristaValidityFrom = DateTimeOffset.UtcNow.AddDays(1) },
                    new StructureNodeDTO { NodeId = 3, ContainsNodeId = 1, AristaValidityFrom = DateTimeOffset.UtcNow.AddDays(1) }
                },
                DateTimeOffset.UtcNow.Date,
                1,
                3,
                true
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildNodesDTOSevenTest()
        {
            var result = StructureNodeDTOExtensions.ToChildNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, ContainsNodeId = 2, IsNew = true },
                    new StructureNodeDTO { NodeId = 2 }
                },
                DateTimeOffset.UtcNow.Date,
                1,
                1,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToNodeVersionDTOTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeVersionDTO(
                new StructureNodeDTO
                {
                    NodeDefinitionId = 1,
                    NodeName = "TEST",
                    NodeCode = "1",
                    NodeLevelId = 1,
                    NodeActive = true,
                    NodeAttentionModeId = 1,
                    NodeAttentionModeName = "TEST",
                    NodeRoleId = 1,
                    NodeRoleName = "TEST",
                    NodeSaleChannelId = 1,
                    NodeSaleChannelName = "TEST",
                    NodeEmployeeId = 1,
                    NodeEmployeeName = "TEST",
                    NodeValidityFrom = DateTime.UtcNow.Date
                }
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToNodeVersionDTOTwoTest()
        {
            var result = StructureNodeDTOExtensions.ToNodeVersionDTO(
                new StructureNodeDTO
                {
                    NodeDefinitionId = 1,
                    NodeName = "TEST",
                    NodeCode = "1",
                    NodeLevelId = 1,
                    NodeActive = true,
                    NodeAttentionModeId = null,
                    NodeRoleId = null,
                    NodeSaleChannelId = null,
                    NodeEmployeeId = 1,
                    NodeEmployeeName = "TEST",
                    NodeValidityFrom = DateTime.UtcNow.Date
                }
            );

            result.Should().NotBeNull();
        }
    }
}