using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoCategoriaVendedor
{
    [DataContract]
    public class TypeCategorySeller
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<TypeCategorySellerLevel1> Level1 { get; set; }

    }
}
