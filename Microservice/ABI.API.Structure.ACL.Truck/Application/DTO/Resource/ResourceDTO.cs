using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Resource
{
    public class ResourceDTO
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        [DataMember(Name = "Relations")]
        public IList<ResourceRelationDTO> Relations { get; set; }
    }
}
