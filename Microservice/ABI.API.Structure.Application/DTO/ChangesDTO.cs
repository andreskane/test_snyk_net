using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class ChangesDTO
    {
        public List<ChangeNodeDTO> Nodes { get; set; }

        public ChangesDTO()
        {
            Nodes = new List<ChangeNodeDTO>();
        }

    }
}
