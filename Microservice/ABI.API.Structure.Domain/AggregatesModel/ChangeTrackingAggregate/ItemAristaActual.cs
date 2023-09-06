namespace ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate
{
    public class ItemAristaActual
    {
        public int AristaId { get; set; }
        public ItemAristaCompare OldValue { get; set; }
        public ItemAristaCompare NewValue { get; set; }
    }
}
