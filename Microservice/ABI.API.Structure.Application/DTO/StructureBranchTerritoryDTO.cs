using ABI.API.Structure.Application.DTO.Interfaces;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureBranchTerritoryDTO : IStructureBranchDTO
    {
        public int Id { get; set; }
        public int StructureId { get; set; }
        public int? NodeIdParent { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int LevelId { get; set; }
        public bool? Active { get; set; }
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public int? SaleChannelId { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsRootNode { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public DateTimeOffset ValidityTo { get; set; }
        public List<IStructureBranchDTO> Nodes { get; set; }
        public ItemNodeDTO AttentionMode { get; set; }
        public ItemNodeDTO Role { get; set; }
        public ItemNodeDTO SaleChannel { get; set; }
        public List<StructureClientDTO> Clients { get; set; }

        public StructureBranchTerritoryDTO()
        {
            Nodes = new List<IStructureBranchDTO>();
            Clients = new List<StructureClientDTO>();
        }
    }
}
