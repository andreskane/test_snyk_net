using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class VacantPersonTruckToPortalTransformation : TransformationBase
    {
        public VacantPersonTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var result = false;

            var resourceTransf = new ResourceTruckToPortalTransformation
            {
                Items = this.Items
            };

            var resource = (ResourceDTO) await resourceTransf.DoTransform(value);

            if (resource != null)
            {
                var relationTruck = resource.Relations.FirstOrDefault(r => r.Type == 1); // 1 - Truck

                if (relationTruck != null)
                {
                    result =  relationTruck.Attributes.Vacante == "S";
                }
            }

            return await Task.FromResult(result);

        }
    }
}
