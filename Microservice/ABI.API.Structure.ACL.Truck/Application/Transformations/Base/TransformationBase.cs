using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations.Base
{
    public abstract class TransformationBase
    {

        public Dictionary<int, ItemTranformation> ItemsD { get; set; }
        public object Items { get; set; }

        protected TransformationBase()
        {
            ItemsD = new Dictionary<int, ItemTranformation>();
        }

        public abstract  Task<object> DoTransform(object value);

        public async Task<object> Transform(object value)
        {
            return await DoTransform(value);
        }


    }
}
