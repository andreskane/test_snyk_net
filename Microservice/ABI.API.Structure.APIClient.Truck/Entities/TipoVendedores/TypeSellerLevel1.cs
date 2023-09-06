using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoVendedores
{
    [DataContract]
    public class TypeSellerLevel1
    {
        /// <summary>
        /// Tipo de Vendedor
        /// </summary>
        /// <value>Tipo de Vendedor</value>
        [DataMember(Name = "TpoVdrId")]
        public string TpoVdrId { get; set; }

        /// <summary>
        /// Descr.Reducida Tipo Vdr.
        /// </summary>
        /// <value>Descr.Reducida Tipo Vdr.</value>
        [DataMember(Name = "TpoVdrAbv")]
        public string TpoVdrAbv { get; set; }

        /// <summary>
        /// Descr.Completa Tipo Vdr.
        /// </summary>
        /// <value>Descr.Completa Tipo Vdr.</value>
        [DataMember(Name = "TpoVdrTxt")]
        public string TpoVdrTxt { get; set; }

        /// <summary>
        /// Id Categoría
        /// </summary>
        /// <value>Id Categoría</value>
        [DataMember(Name = "CatVdrId")]
        public string CatVdrId { get; set; }

    }
}
