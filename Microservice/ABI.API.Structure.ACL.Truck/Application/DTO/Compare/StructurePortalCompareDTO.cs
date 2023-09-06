using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Compare
{
    public class StructurePortalCompareDTO
    {
        public virtual DateTimeOffset Date { get; set; }
        public virtual int StructureId { get; set; }
        public virtual int StructureModelId { get; set; }

        public virtual List<NodePortalCompareDTO> TruckNodes { get; set; }

        public virtual List<NodePortalCompareDTO> PortalNodes { get; set; }

        public virtual List<NodePortalCompareDTO> UpdateNodes { get; set; }


        public StructurePortalCompareDTO()
        {

            TruckNodes = new List<NodePortalCompareDTO>();
            PortalNodes = new List<NodePortalCompareDTO>();
            UpdateNodes = new List<NodePortalCompareDTO>();
        }

    }
}
