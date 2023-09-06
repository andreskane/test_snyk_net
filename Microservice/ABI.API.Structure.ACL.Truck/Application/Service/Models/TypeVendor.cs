namespace ABI.API.Structure.ACL.Truck.Application.Service.ACL.Models
{
    public class TypeVendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public TypeVendor(int id, string name, string code, string description = null)
        {
            Id = id;
            Name = $"{id} - {name}";
            Code = code;
            Description = description;
        }

        public TypeVendor()
        { }
    }
}
