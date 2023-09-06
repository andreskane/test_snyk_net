using System;

namespace ABI.API.Structure.Application.DTO
{
    public class ChangeNodeDTO
    {
        public virtual int NodeId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual int NodeDefinitionId { get; set; }
        public virtual int LevelId { get; set; }
        public virtual string Level { get; set; }
        public virtual bool Active { get; set; }
        public virtual int? AttentionModeId { get; set; }
        public virtual string AttentionModeName { get; set; }
        public virtual int? RoleId { get; set; }
        public virtual string Role { get; set; }
        public virtual int? EmployeeId { get; set; }
        public virtual int? SaleChannelId { get; set; }
        public virtual string SaleChannel { get; set; }
        public virtual DateTimeOffset? ValidityFrom { get; set; }
        public virtual DateTimeOffset ValidityTo { get; set; }
        public virtual int? NodeParentId { get; set; }
        public virtual string NodeParentCode { get; set; }
        public virtual string NodeParentName { get; set; }
        public virtual int NodeMotiveStateId { get; set; }
        public virtual int AristaMotiveStateId { get; set; }
    }
}
