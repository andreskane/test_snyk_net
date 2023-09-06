using System;

namespace ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate
{
    public class ItemAristaCompare
    {
        public int StructureIdFrom { get; set; }
        public int NodeIdFrom { get; set; }
        public DateTimeOffset AristaValidityTo { get; set; }
        public string Description { get; set; }
    }
}
