using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CategoriaVendedor
{
    [DataContract]
    public class TruckSellerCategory
    {
        [DataMember(Name = "CategoriaVendedor")]
        public SellerCategory SellerCategory { get; set; }

    }
}
