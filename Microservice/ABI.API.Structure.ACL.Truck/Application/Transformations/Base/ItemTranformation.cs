namespace ABI.API.Structure.ACL.Truck.Application.Transformations.Base
{
    public class ItemTranformation
    {
        public object Source { get; set; }
        public object Target { get; set; }

        public ItemTranformation(object source, object target)
        {
            Source = source;
            Target = target;
        }
    }
}
