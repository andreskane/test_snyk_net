using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class ActiveNodePortalToTruckTransformation : TransformationBase
    {

        public ActiveNodePortalToTruckTransformation()
        {


        }

        public override async Task<object> DoTransform(object value)
        {
            var active = Convert.ToBoolean(value);

            return await Task.FromResult(active ? 200 : 201);

        }

    }
}
