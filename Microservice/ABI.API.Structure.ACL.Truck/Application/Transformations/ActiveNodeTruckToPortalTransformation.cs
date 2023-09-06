using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class ActiveNodeTruckToPortalTransformation : TransformationBase
    {
        public ActiveNodeTruckToPortalTransformation()
        {

        }

        public override async  Task<object> DoTransform(object value)
        {
            var itemText = value.ToString();
            var result = (bool?)false;

            if (!string.IsNullOrEmpty(itemText))
            {
                switch (itemText)
                {
                    case "200": //Activo
                        result = true;
                        break;

                    case "201": //Inactivo
                        result = false;
                        break;
                    default:
                        result = null;
                        break;
                }
            }

            return await Task.FromResult(result);

        }

    }
}
