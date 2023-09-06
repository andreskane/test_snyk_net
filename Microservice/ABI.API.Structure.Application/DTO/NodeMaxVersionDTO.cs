using System;

namespace ABI.API.Structure.Application.DTO
{
    public class NodeMaxVersionDTO
    {
        public virtual int NodeId { get; set; }
        public virtual DateTimeOffset ValidityFrom { get; set; }
    }
}
