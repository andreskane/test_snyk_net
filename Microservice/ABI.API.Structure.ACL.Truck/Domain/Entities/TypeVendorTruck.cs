using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class TypeVendorTruck : BaseEntity<int>
    {

        public int AttentionModeRoleId { get; set; }

        public int? VendorTruckId { get; set; }

    }
}
