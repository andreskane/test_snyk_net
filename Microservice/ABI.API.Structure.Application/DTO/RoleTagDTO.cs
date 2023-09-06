using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class RoleTagDTO
    {
        public List<string> Tags { get; set; }
        public RoleTagDTO()
        {
            Tags = new List<string>();
        }

    }
}
