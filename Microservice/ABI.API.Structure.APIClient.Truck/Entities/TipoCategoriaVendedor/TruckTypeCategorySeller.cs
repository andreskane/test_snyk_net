using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoCategoriaVendedor
{
    [DataContract]
    public class TruckTypeCategorySeller
    {
        /// <summary>
        /// Tipo Categoria Vendedor
        /// </summary>
        /// <value>Tipo Categoria Vendedor</value>
        [DataMember(Name = "TipoCategoriaVendedor")]
        public TypeCategorySeller TypeCategorySeller { get; set; }

    }
}
