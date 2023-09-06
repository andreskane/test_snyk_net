using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class BusinessTruckPortal : BaseEntity<int>
    {

        public string BusinessCode { get; set; }

        public int StructureModelId { get; set; }

    }
}
