using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class LevelTruckToPortalTransformation : TransformationBase
    {

        public override  async Task<object> DoTransform(object value)
        {
            var result = 0;
            var nivelText = value.ToString();
            var item = (Items as List<LevelTruckPortal>).FirstOrDefault(l => l.LevelTruckName == nivelText);

            if (item != null)
                result =  item.LevelPortalId;

            return await Task.FromResult(result);

        }
    }
}
