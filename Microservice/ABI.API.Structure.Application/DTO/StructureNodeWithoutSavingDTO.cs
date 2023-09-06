using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureNodeWithoutSavingDTO
    {
        public virtual StructureDTO Structure { get; set; }
        public virtual bool ChangesWithoutSaving { get; set; }
        public virtual DateTimeOffset CalendarDate { get; set; }

        public List<NodeDTO> Nodes { get; set; }

        public StructureNodeWithoutSavingDTO()
        {
            Structure = new StructureDTO();
            Nodes = new List<NodeDTO>();
        }
    }
}
