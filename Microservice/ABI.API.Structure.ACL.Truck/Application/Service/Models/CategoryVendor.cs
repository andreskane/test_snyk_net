namespace ABI.API.Structure.ACL.Truck.Application.Service.Models
{
    public class CategoryVendor
    {
        public string CategoryId { get; set; }

        public string CategoryDescription { get; set; }

        public string CategoryShortDesc { get; set; }

        public string CategoryStatus { get; set; }

        public string CategoryResponsable { get; set; }

        public string CatVdrIngPed { get; set; }

        public string CatVdrRutEnt { get; set; }

        public string CatVdrRutVta { get; set; }

        public CategoryVendor(string id, string description, string shortDescription, string status, string responsable)
        {
            CategoryId = id;
            CategoryDescription = description;
            CategoryShortDesc = shortDescription;
            CategoryStatus = description;
            CategoryResponsable = responsable;
        }

        public CategoryVendor()
        { }
    }
}
