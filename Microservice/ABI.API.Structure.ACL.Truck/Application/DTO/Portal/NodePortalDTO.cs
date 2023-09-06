using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Portal
{
    public class NodePortalDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int LevelId { get; set; }
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public int? SaleChannelId { get; set; }
        public int? EmployeeId { get; set; }
        public int? NodeIdParent { get; set; }
        public bool IsRootNode { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
        public DateTimeOffset ValidityTo { get; set; }
        public List<NodePortalDTO> Nodes { get; set; }
        public bool VacantPerson { get; set; }

        public NodePortalDTO()
        {
            Nodes = new List<NodePortalDTO>();

        }
    }
}
