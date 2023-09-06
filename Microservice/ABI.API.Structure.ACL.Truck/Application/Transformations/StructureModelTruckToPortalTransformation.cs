using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class StructureModelTruckToPortalTransformation : TransformationBase
    {
        public StructureModelTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var result = default(int);
            var code = value.ToString().Trim();

            var item = (Items as List<BusinessTruckPortal>).FirstOrDefault(l => l.BusinessCode == code);

            if (item != null)
                result =  item.StructureModelId;

            return await Task.FromResult(result);
        }

    }
}
