using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PendingVersionTruckDTO
    {
        public DateTimeOffset? LastVersionDate { get; set; }
        public string VersionTruck { get; set; }
        public bool StructureEdit { get; set; }
        public string Message { get; set; }
        public bool TypePortal { get; set; }
        public bool IsPrevious { get; set; }
        public bool IsSameVersion { get; set; }
        public bool IsTrasp { get; set; }


        public PendingVersionTruckDTO()
        {
            StructureEdit = true;
            TypePortal = false;
            IsPrevious = false;
            IsSameVersion = false;
            IsTrasp = false;

        }

    }
}
