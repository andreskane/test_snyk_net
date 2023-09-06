using System;

namespace ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate
{
    public class ItemArista
    {
        public int AristaId { get; set; }
        public int StructureIdFrom { get; set; }
        public int NodeIdFrom { get; set; }
        public DateTimeOffset AristaValidityFrom { get; set; }
        public DateTimeOffset AristaValidityTo { get; set; }
    }
}
