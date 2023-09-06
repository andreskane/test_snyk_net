using System;

namespace ABI.API.Structure.Application.DTO
{
    public class TruckProcessStatusDTO
    {
        public TruckProcessStatusStructureDTO Structure { get; set; }
        public string Username { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }
}
