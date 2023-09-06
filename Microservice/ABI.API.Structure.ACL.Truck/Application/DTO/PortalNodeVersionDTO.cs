using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PortalNodeVersionDTO
    {
        public int NodeDefinitionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int LevelId { get; set; }
        public bool? Active { get; set; }
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public int? SaleChannelId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTimeOffset? ValidityFrom { get; set; }
        public ItemNodeDTO AttentionMode { get; set; }
        public ItemNodeDTO Role { get; set; }
        public ItemNodeDTO SaleChannel { get; set; }

    }
}
