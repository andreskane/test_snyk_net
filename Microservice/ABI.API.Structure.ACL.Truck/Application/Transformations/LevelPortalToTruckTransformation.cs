using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.Framework.MS.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class LevelPortalToTruckTransformation : TransformationBase
    {

        public override async Task<object> DoTransform(object value)
        {
            var id = value.ToInt();

            var item = (Items as List<LevelTruckPortal>).FirstOrDefault(l => l.LevelPortalId == id);

            return await Task.FromResult(item.LevelTruckName); 
        }

    }
}
