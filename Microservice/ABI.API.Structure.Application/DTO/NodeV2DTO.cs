using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class NodeV2DTO
    {
        public int Id { get; set; }
        public int StructureId { get; set; }

        public int? NodeIdParent { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int LevelId { get; set; }

        public bool? Active { get; set; }

        public bool? ChildNodeActiveFieldCanBeEdited { get; set; }

        public int? AttentionModeId { get; set; }

        public int? RoleId { get; set; }

        public int? SaleChannelId { get; set; }

        public int? EmployeeId { get; set; }

        public bool? IsRootNode { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
        public DateTimeOffset ValidityTo { get; set; }
        public List<NodeV2DTO> Nodes { get; set; }
        public string VersionType { get; set; }
        public DTO.NodeVersionV2DTO Version { get; set; }

        public NodeV2DTO()
        {
            Nodes = new List<NodeV2DTO>();
            ChildNodeActiveFieldCanBeEdited = true;
        }
    }
}
