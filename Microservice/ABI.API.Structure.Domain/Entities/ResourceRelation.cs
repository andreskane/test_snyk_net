using System.Runtime.Serialization;

namespace ABI.API.Structure.Domain.Entities
{
    public class ResourceRelation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        [DataMember(Name = "Attributes")]
        public ResourceAttributes Attributes { get; set; }
    }
}