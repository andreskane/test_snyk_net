using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureNodeChangesDTO
    {
        public int NodeId { get; set; }
        public string Description { get; set; }
        public bool NewNode { get; set; }

        public List<ChangesWithoutSavingDTO> Changes { get; set; }
        public StructureNodeChangesDTO()
        {
            Changes = new List<ChangesWithoutSavingDTO>();
            NewNode = false;
        }
    }
}
