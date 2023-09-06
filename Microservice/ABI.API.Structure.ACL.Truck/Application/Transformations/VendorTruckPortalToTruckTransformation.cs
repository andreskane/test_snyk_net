using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class VendorTruckPortalToTruckTransformation : TransformationBase
    {

        public VendorTruckPortalToTruckTransformation()
        {


        }

        public override async Task<object> DoTransform(object value)
        {
            var node = (DTO.NodePortalTruckDTO)value;

            var result = default(int?);

            TypeVendorTruckPortal item = null;

            if (node.RoleId.HasValue)
            {
                item = (Items as List<TypeVendorTruckPortal>).FirstOrDefault(l => l.MappingTruckWriting == true && l.AttentionModeId == node.AttentionModeId && l.RoleId == node.RoleId);
            }
            else
            {
                item = (Items as List<TypeVendorTruckPortal>).FirstOrDefault(l => l.MappingTruckWriting == true && l.AttentionModeId == node.AttentionModeId && !l.RoleId.HasValue);
            }


            if (item != null)
                result = item.VendorTruckId;

            return await Task.FromResult(result);
        }

    }
}
