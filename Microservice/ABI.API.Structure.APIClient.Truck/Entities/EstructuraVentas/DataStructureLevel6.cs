using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DataStructureLevel6
    {
        /// <summary>
        /// Código de Territorio
        /// </summary>
        /// <value>Código de Territorio</value>
        [DataMember(Name = "TrrId")]
        public string TrrId { get; set; }

        /// <summary>
        /// Nombre del Territorio
        /// </summary>
        /// <value>Nombre del Territorio</value>
        [DataMember(Name = "TrrTxt")]
        public string TrrTxt { get; set; }

        /// <summary>
        /// Nombre reducido Territorio
        /// </summary>
        /// <value>Nombre reducido Territorio</value>
        [DataMember(Name = "TrrAbv")]
        public string TrrAbv { get; set; }

        /// <summary>
        /// Cdigo del Vendedor
        /// </summary>
        /// <value>Cdigo del Vendedor</value>
        [DataMember(Name = "VdrCod")]
        public string VdrCod { get; set; }

        /// <summary>
        /// Nombre Vendedor
        /// </summary>
        /// <value>Nombre Vendedor</value>
        [DataMember(Name = "TrrNom")]
        public string TrrNom { get; set; }

        /// <summary>
        /// Fuerza de Venta
        /// </summary>
        /// <value>Fuerza de Venta</value>
        [DataMember(Name = "TrrIdFvt")]
        public string TrrIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "TrrFvtAbv")]
        public string TrrFvtAbv { get; set; }

        /// <summary>
        /// Deposito del Territorio
        /// </summary>
        /// <value>Deposito del Territorio</value>
        [DataMember(Name = "TrrIdDps")]
        public string TrrIdDps { get; set; }

        /// <summary>
        /// Descripción reducida Depósito
        /// </summary>
        /// <value>Descripción reducida Depósito</value>
        [DataMember(Name = "DpsAbv")]
        public string DpsAbv { get; set; }

        /// <summary>
        /// Categora del Personal
        /// </summary>
        /// <value>Categora del Personal</value>
        [DataMember(Name = "VdrTpoCat")]
        public string VdrTpoCat { get; set; }

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
        /// Identificador del Merchandiser
        /// </summary>
        /// <value>Identificador del Merchandiser</value>
        [DataMember(Name = "TrrIdMer")]
        public string TrrIdMer { get; set; }

        /// <summary>
        /// Nombre Merchandiser
        /// </summary>
        /// <value>Nombre Merchandiser</value>
        [DataMember(Name = "MerNom")]
        public string MerNom { get; set; }

        /// <summary>
        /// Id Categoría
        /// </summary>
        /// <value>Id Categoría</value>
        [DataMember(Name = "CatVdrId")]
        public string CatVdrId { get; set; }

        /// <summary>
        /// Desc Abreviada Categoría
        /// </summary>
        /// <value>Desc Abreviada Categoría</value>
        [DataMember(Name = "CatVdrAbv")]
        public string CatVdrAbv { get; set; }


    }
}
