namespace ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate
{
    public class ChangeTrackingNode
    {
        public string Field { get; set; }
        public string FieldName { get; set; }
        public ItemNode Node { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
