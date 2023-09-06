namespace ABI.API.Structure.ACL.Truck.Application.Transformations.Base
{
    public class SubItemTranformation
    {
        public object Key { get; set; }
        public object Value { get; set; }


        public SubItemTranformation(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
