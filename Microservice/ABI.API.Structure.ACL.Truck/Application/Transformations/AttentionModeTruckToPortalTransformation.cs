using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.Framework.MS.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class AttentionModeTruckToPortalTransformation : TransformationBase
    {
        public AttentionModeTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var id = value.ToInt();
            var result = (int?) null;

            var item = (Items as List<TypeVendorTruckPortal>)?.FirstOrDefault(t => t.VendorTruckId == id);

            if (item != null)
                result = item.AttentionModeId;

            return await Task.FromResult(result);

        }

    }
}
