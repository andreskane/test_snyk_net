using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Extension.Tests
{
    [TestClass()]
    public class StructureBranchNodeDTOExtensionsTests
    {
        [TestMethod()]
        public void ToBranchNodeDTOTest()
        {
            var result = StructureBranchNodeDTOExtensions.ToBranchNodeDTO(new StructureNodeDTO
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
            new StructureBranchNodeDTO { Id = 1},
            1,
            null
            );

            result.Should().NotBeNull();
        }


        [TestMethod()]
        public void ToChildBranchNodesDTOTest()
        {
            var result = StructureBranchNodeDTOExtensions.ToChildBranchNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { 
                        NodeId = 1, 
                        ContainsNodeId = 2, 
                        NodeValidityFrom = DateTimeOffset.MinValue,        
                        NodeActive = true,
                        NodeAttentionModeId = 1,
                        NodeAttentionModeName = "TEST",
                        NodeRoleId = 1,
                        NodeRoleName = "TEST",
                        NodeSaleChannelId = 1,
                        NodeSaleChannelName = "TEST",
                        NodeEmployeeId = 1,
                        NodeEmployeeName = "TEST"},
                    new StructureNodeDTO { 
                        NodeId = 2, 
                        NodeValidityFrom = DateTimeOffset.MinValue,         
                        NodeActive = true,
                        NodeAttentionModeId = 1,
                        NodeAttentionModeName = "TEST2",
                        NodeRoleId = 1,
                        NodeRoleName = "TEST2",
                        NodeSaleChannelId = 1,
                        NodeSaleChannelName = "TEST2",
                        NodeEmployeeId = 1,
                        NodeEmployeeName = "TEST2"}
                },
                1,
                1,
                true,
                null
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToChildBranchNodesDTOTwoTest()
        {
            var result = StructureBranchNodeDTOExtensions.ToChildBranchNodesDTO(
                new List<StructureNodeDTO>(),
                0,
                1,
                true,
                null
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildBranchNodesDTOThreeTest()
        {
            var result = StructureBranchNodeDTOExtensions.ToChildBranchNodesDTO(
                new List<StructureNodeDTO>(),
                1,
                1,
                true,
                null
            );

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToChildBranchTerritoryDTOTest()
        {

            var Clients = new List<StructureClientDTO>();
            var client = new StructureClientDTO { Id = 1, ClientId = "11111", Name = "TEST", NodeId = 2, State = "1", ValidityFrom = DateTimeOffset.MinValue, ValidityTo = DateTimeOffset.MaxValue };
            Clients.Add(client);


            var result = StructureBranchNodeDTOExtensions.ToChildBranchNodesDTO(
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { 
                        NodeId = 1, 
                        ContainsNodeId = 2, 
                        NodeValidityFrom = DateTimeOffset.MinValue, 
                        NodeLevelId = 7, 
                        NodeValidityTo = DateTimeOffset.MaxValue,              
                        NodeActive = true,
                        NodeAttentionModeId = 1,
                        NodeAttentionModeName = "TEST",
                        NodeRoleId = 1,
                        NodeRoleName = "TEST",
                        NodeSaleChannelId = 1,
                        NodeSaleChannelName = "TEST",
                        NodeEmployeeId = 1,
                        NodeEmployeeName = "TEST"},
                    new StructureNodeDTO { 
                        NodeId = 2, 
                        NodeValidityFrom = DateTimeOffset.MinValue, 
                        NodeLevelId = 8 , 
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,
                        NodeAttentionModeId = 2,
                        NodeAttentionModeName = "TEST2",
                        NodeRoleId = 2,
                        NodeRoleName = "TEST2",
                        NodeSaleChannelId = 2,
                        NodeSaleChannelName = "TEST2",
                        NodeEmployeeId = 2,
                        NodeEmployeeName = "TEST2"}
                },
                1,
                1,
                true,
                Clients
            );

            result.Should().NotBeNull();
        }

    }
}