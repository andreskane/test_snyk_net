namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class StructureModelDTO
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public string ShortName { get; set; }
        public virtual string Description { get; set; }
        public bool? Active { get; set; }
        public bool CanBeExportedToTruck { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }

    }
}
