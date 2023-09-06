using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoVendedores
{
    [DataContract]
    public class TruckTypeSeller
    {
        /// <summary>
        /// Tipo Vendedor
        /// </summary>
        /// <value>Tipo Vendedor</value>
        [DataMember(Name = "TipoVendedor")]
        public TypeSeller TypeSeller { get; set; }
    }
}
