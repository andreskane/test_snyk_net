using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Models
{
    public class AttentionModelRolTypeVender
    {
        public AttentionMode AttentionMode { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int AttentionModeRoleId { get; set; }
        public int TypeVendorTruckId { get; set; }
        public int? VendorTruckId { get; set; }
        public string VendorTruckName { get; set; }

        public AttentionModelRolTypeVender()
        {

        }

        public AttentionModelRolTypeVender(int typeVendorTruckId, int attentionModeRoleId, int? vendorTruckId)
        {
            TypeVendorTruckId = typeVendorTruckId;
            AttentionModeRoleId = attentionModeRoleId;
            VendorTruckId = vendorTruckId;
        }
    }
}
