using System;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public string LastModifiedByName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string CompanyId { get; set; }
    }
}
