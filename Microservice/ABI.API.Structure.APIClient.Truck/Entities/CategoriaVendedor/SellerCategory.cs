using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CategoriaVendedor
{
    [DataContract]
    public class SellerCategory
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<SellerCategoryLevel1> Level1 { get; set; }
    }
}
