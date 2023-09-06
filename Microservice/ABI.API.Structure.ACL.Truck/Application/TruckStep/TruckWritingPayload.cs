using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Application.TruckStep
{
    public class TruckWritingPayload
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Username { get; set; }
        public List<Versioned> VersionsToUnify { get; set; }

        public TruckWritingPayload()
        {
            VersionsToUnify = new List<Versioned>();
        }
    }
}
