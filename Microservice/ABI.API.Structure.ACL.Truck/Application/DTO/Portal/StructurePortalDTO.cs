using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Portal
{
    public class StructurePortalDTO
    {
        public virtual int Id { get; set; }

        public virtual int StructureModelId { get; set; }

        public virtual DateTimeOffset? ValidityFrom { get; set; }

        public virtual List<NodePortalDTO> Nodes { get; set; }

        public StructurePortalDTO()
        {

            Nodes = new List<NodePortalDTO>();
        }

    }
}
