using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureDomainDTO
    {
        public virtual StructureDTO Structure { get; set; }
        public virtual bool HasUnsavedChanges { get; set; }
        public virtual DateTimeOffset CalendarDate { get; set; }
        public List<NodeDTO> Nodes { get; set; }

        public StructureDomainDTO()
        {
            Structure = new StructureDTO();
            Nodes = new List<NodeDTO>();
        }
    }
}
