using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class ResourceTruckToPortalTransformation : TransformationBase
    {
        public ResourceTruckToPortalTransformation()
        {

        }

        public override async Task<object> DoTransform(object value)
        {
            var result = default(ResourceDTO);
            var data = (dynamic)value;

            if (data != null)
            {
                var employeeId = (data.EmployeeId as string).Trim();
                var leveltruck = (data.TypeEmployeeTruck as string).Trim().Split(',');

                foreach (var itemlevel in leveltruck)
                {
                    var resource = (Items as List<ResourceDTO>).FirstOrDefault(r => r.Relations.Any(x => x.Attributes.VdrCod == employeeId && x.Attributes.VdrTpoCat == itemlevel));

                    if (resource != null)
                    {
                        result = resource;
                    }
                }
            }

            return await Task.FromResult(result); 
        }

    }
}
