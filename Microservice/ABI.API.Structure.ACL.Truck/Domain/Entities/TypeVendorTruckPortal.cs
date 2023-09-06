using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class TypeVendorTruckPortal : BaseEntity<int>
    {

        public int VendorTruckId { get; set; }
        public string VendorTruckName { get; set; }
        public int? AttentionModeId { get; set; }
        public int? RoleId { get; set; }
        public bool? MappingTruckReading { get; set; }
        public bool? MappingTruckWriting { get; set; }

    }
}
