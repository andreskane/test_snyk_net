using System;

namespace ABI.API.Structure.Application.DTO
{
    public class NodeAristaDTO
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public string NodeCode { get; set; }
        public bool NodeActive { get; set; }
        public int NodeLevelId { get; set; }
        public int? NodeIdParent { get; set; }
        public int? NodeIdTo { get; set; }
        public int NodeMotiveStateId { get; set; }
        public DateTimeOffset NodeValidityFrom { get; set; }
        public DateTimeOffset NodeValidityTo { get; set; }
        public bool IsNew { get; set; }
        public int? NodeRoleId { get; set; }
        public int? NodeAttentionModeId { get; set; }
        public int? NodeSaleChannelId { get; set; }
        public bool? NodeVacantPerson { get; set; }
        public int? NodeEmployeeId { get; set; }

        public int? AristaMotiveStateId { get; set; }
        public int? AristaChildMotiveStateId { get; set; }
        public DateTimeOffset? AristaValidityFrom { get; set; }
        public DateTimeOffset? AristaValidityTo { get; set; }
    }
}
