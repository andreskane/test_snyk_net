using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Interfaces
{
    public interface IStructureBranchDTO
    {
         int Id { get; set; }
         int StructureId { get; set; }
         int? NodeIdParent { get; set; }
         string Name { get; set; }
         string Code { get; set; }
         int LevelId { get; set; }
         bool? Active { get; set; }
         int? AttentionModeId { get; set; }
         int? RoleId { get; set; }
         int? SaleChannelId { get; set; }
         int? EmployeeId { get; set; }
         bool? IsRootNode { get; set; }
         DateTimeOffset ValidityFrom { get; set; }
         DateTimeOffset ValidityTo { get; set; }
         List<IStructureBranchDTO> Nodes { get; set; }
         ItemNodeDTO AttentionMode { get; set; }
         ItemNodeDTO Role { get; set; }
         ItemNodeDTO SaleChannel { get; set; }
    }
}
