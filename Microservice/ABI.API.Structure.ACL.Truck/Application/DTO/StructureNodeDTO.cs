using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class StructureNodeDTO
    {
        public virtual StructureDTO Structure { get; set; }
        public virtual bool ChangesWithoutSaving { get; set; }

        public List<NodeDTO> Nodes { get; set; }

        public StructureNodeDTO()
        {
            Structure = new StructureDTO();
            Nodes = new List<NodeDTO>();
        }
    }
}
