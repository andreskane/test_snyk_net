using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PortalStructureNodeDTO
    {
        public virtual int StructureId { get; set; }
        public virtual int StructureModelID { get; set; }
        public virtual string StructureModelName { get; set; }
        public virtual string StructureModelShortName { get; set; }
        public virtual string StructureModelDescription { get; set; }
        public virtual bool StructureModelActive { get; set; }
        public virtual int RootNodeId { get; set; }
        public virtual DateTimeOffset? StructureValidityFrom { get; set; }
        public virtual int NodeId { get; set; }
        public virtual int? NodeParentId { get; set; }
        public virtual int? ContainsNodeId { get; set; }
        public virtual string NodeName { get; set; }
        public virtual string NodeCode { get; set; }
        public virtual int NodeLevelId { get; set; }
        public virtual string NodeLevelName { get; set; }
        public virtual bool? NodeActive { get; set; }
        public virtual int? NodeAttentionModeId { get; set; }
        public virtual string NodeAttentionModeName { get; set; }
        public virtual int? NodeRoleId { get; set; }
        public virtual string NodeRoleName { get; set; }
        public virtual int? NodeEmployeeId { get; set; }
        public virtual string NodeEmployeeName { get; set; }
        public virtual int? NodeSaleChannelId { get; set; }
        public virtual string NodeSaleChannelName { get; set; }
        public virtual DateTimeOffset? NodeValidityFrom { get; set; }
        public virtual DateTimeOffset NodeValidityTo { get; set; }
        public virtual int NodeDefinitionId { get; set; }
        public virtual string VersionType { get; set; }


    }
}
