using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PortalNodePendingDTO
    {
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }
        public int NodeLevelId { get; set; }
        public int NodeDefinitionId { get; set; }
        public virtual DateTimeOffset? NodeValidityFrom { get; set; }
        public string TypeVersion { get; set; }
    }
}
