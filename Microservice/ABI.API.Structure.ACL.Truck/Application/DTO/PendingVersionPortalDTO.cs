using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PendingVersionPortalDTO
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public string Message { get; set; }

    }
}
