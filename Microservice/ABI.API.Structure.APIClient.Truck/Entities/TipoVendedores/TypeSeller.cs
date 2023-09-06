using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoVendedores
{
    [DataContract]
    public class TypeSeller
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<TypeSellerLevel1> Level1 { get; set; }
    }
}
