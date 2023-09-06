using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class NodePortalTruckDTO
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int LevelId { get; set; }
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public int? SaleChannelId { get; set; }
        public int? EmployeeId { get; set; }
        public int? NodeIdParent { get; set; }
        public bool? IsRootNode { get; set; }
        public bool ActiveNode { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
        public DateTimeOffset ValidityTo { get; set; }
        public string ParentNodeCode { get; set; }
        public string ChildNodeCode { get; set; }
        public int? NodeIdTo { get; set; }
        public int? NodeDefinitionId { get; set; }

        public List<NodePortalTruckDTO> ChildNodes { get; set; }


        public NodePortalTruckDTO()
        {
            ChildNodes = new List<NodePortalTruckDTO>();
        }


    }
}
