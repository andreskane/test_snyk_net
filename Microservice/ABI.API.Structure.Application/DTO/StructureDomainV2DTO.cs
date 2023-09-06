using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureDomainV2DTO
    {
        public virtual StructureV2DTO Structure { get; set; }
        public virtual bool HasUnsavedChanges { get; set; }
        public virtual DateTimeOffset CalendarDate { get; set; }
        public List<NodeV2DTO> Nodes { get; set; }

        public StructureDomainV2DTO()
        {
            Structure = new StructureV2DTO();
            Nodes = new List<NodeV2DTO>();
        }
    }
}
