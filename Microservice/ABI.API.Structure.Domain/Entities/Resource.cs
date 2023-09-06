using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Domain.Entities
{
    public class Resource
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [DataMember(Name = "Relations")]
        public IList<ResourceRelation> Relations { get; set; }
    }
}
