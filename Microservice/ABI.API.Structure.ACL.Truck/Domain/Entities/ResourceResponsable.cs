namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class ResourceResponsable
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string TruckId { get; set; }
        public bool IsVacant { get; set; }
        public string VendorCategory { get; set; }
        public int CountryId { get; set; }
    }
}
