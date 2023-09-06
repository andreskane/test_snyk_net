using System;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureClientDTO
    {
        public virtual int Id { get; set; }
        public virtual int NodeId { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string Name { get; set; }
        public virtual string State { get; set; }

        public virtual DateTimeOffset ValidityFrom { get; set; }
        public virtual DateTimeOffset ValidityTo { get; set; }


        public StructureClientDTO()
        {
 
        }
    }
}
