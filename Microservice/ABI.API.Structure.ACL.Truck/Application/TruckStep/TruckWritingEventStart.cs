using Coravel.Events.Interfaces;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.TruckStep
{
    public class TruckWritingEventStart : IEvent
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Username { get; set; }
    }
}
