using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class EmployeeIdTruckToPortalTransformation : TransformationBase
    {
        public EmployeeIdTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var result = (int?)null;

            var resourceTransf = new ResourceTruckToPortalTransformation
            {
                Items = this.Items
            };

            var resource = (ResourceDTO)await resourceTransf.DoTransform(value);

            if (resource != null)
                result =  resource.Id;

            return await Task.FromResult(result);
        }

    }
}
