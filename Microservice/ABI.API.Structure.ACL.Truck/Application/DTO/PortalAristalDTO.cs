namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PortalAristalDTO
    {
        public int? AristaId { get; set; }
        public int StructureId { get; set; }
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public string NodeCode { get; set; }
        public bool NodeActive { get; set; }
        public int NodeLevelId { get; set; }
        public int? NodeIdTo { get; set; }

    }
}
