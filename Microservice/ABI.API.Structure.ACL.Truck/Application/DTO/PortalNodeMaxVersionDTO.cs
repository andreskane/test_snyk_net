using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class PortalNodeMaxVersionDTO
    {
        public virtual int NodeId { get; set; }
        public virtual DateTimeOffset? ValidityFrom { get; set; }
    }
}
