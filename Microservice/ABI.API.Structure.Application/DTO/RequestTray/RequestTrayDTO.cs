using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class RequestTrayDTO
    {
        public ItemDTO Structure { get; set; }
        public string StructureCode { get; set; }
        public UserDTO User { get; set; }
        public DateTimeOffset Validity { get; set; }
        public List<string> ChangeType { get; set; }
        public int PortalStatus { get; set; }
        public TruckStatusDTO TruckStatus { get; set; }
        public Int32 NodeDefinitionID { get; set; }
        public RequestTrayDTO()
        {
            ChangeType = new List<string>();
            TruckStatus = new TruckStatusDTO();
        }


    }
}
