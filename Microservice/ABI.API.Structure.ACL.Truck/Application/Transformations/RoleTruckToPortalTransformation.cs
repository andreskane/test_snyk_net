using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class RoleTruckToPortalTransformation : TransformationBase
    {
        public RoleTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            //Rol para los niveles mennos territorio.
            var result = default(int?);

            var nivelText = value.ToString();
            var item = (Items as List<LevelTruckPortal>).FirstOrDefault(l => l.LevelTruckName == nivelText);

            if (item != null)
                result = item.RolPortalId;

            return await Task.FromResult(result);
        }

    }
}
