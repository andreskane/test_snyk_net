using System;

namespace ABI.API.Structure.Application.DTO
{
    public class NodePendingDTO
    {
        public int StructureId { get; set; }
        public int AristaId { get; set; }
        public int NodeId { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }
        public int NodeLevelId { get; set; }
        public int NodeDefinitionId { get; set; }
        public int? NodeParentId { get; set; }
        public virtual DateTimeOffset NodeValidityFrom { get; set; }
        public virtual string TypeVersion { get; set; }
        public virtual int NodeMotiveStateId { get; set; }
        public virtual DateTimeOffset AristaValidityFrom { get; set; }
        public virtual DateTimeOffset AristaValidityTo { get; set; }
        public virtual int AristaMotiveStateId { get; set; }
    }
}
