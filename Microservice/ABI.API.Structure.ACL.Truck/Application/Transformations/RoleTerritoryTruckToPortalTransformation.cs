using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.Framework.MS.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class RoleTerritoryTruckToPortalTransformation : TransformationBase
    {
        public RoleTerritoryTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var result = default(int?);

            //Rol para territorio.
            var id = value.ToInt();

            var item = (Items as List<TypeVendorTruckPortal>).FirstOrDefault(t => t.MappingTruckReading == true && t.VendorTruckId == id);

            if (item != null)
                return item.RoleId;

            return await Task.FromResult(result);

        }

    }
}
