using System.Runtime.Serialization;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Resource
{
    public class ResourceRelationDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        [DataMember(Name = "Attributes")]
        public ResourceAttributesDTO Attributes { get; set; }
    }
}