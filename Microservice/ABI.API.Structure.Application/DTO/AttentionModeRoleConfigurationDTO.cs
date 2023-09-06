namespace ABI.API.Structure.Application.DTO
{
    public class AttentionModeRoleConfigurationDTO
    {
        public int AttentionModeRoleId { get; set; }
        public int? AttentionModeId { get; set; }
        public string AttentionModeName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int TypeVendorTruckId { get; set; }
        public int? VendorTruckId { get; set; }
        public string VendorTruckName { get; set; }
    }
}
