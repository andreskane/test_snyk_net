using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CategoriaVendedor
{
    [DataContract]
    public class SellerCategoryLevel1
    {
        /// <summary>
        /// Id Categorí­a
        /// </summary>
        /// <value>Id Categorí­a</value>
        [DataMember(Name = "CatVdrId")]
        public string CatVdrId { get; set; }

        /// <summary>
        /// Descripción Categorí­a
        /// </summary>
        /// <value>Descripción Categorí­a</value>
        [DataMember(Name = "CatVdrTxt")]
        public string CatVdrTxt { get; set; }

        /// <summary>
        /// Desc Abreviada Categorí­a
        /// </summary>
        /// <value>Desc Abreviada Categorí­a</value>
        [DataMember(Name = "CatVdrAbv")]
        public string CatVdrAbv { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "CatVdrSts")]
        public string CatVdrSts { get; set; }

        /// <summary>
        /// Responsable
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "CatVdrEstVta")]
        public string CatVdrEstVta { get; set; }

        /// <summary>
        /// Consultar
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "CatVdrIngPed")]
        public string CatVdrIngPed { get; set; }

        /// <summary>
        /// Consultar
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "CatVdrRutEnt")]
        public string CatVdrRutEnt { get; set; }

        /// <summary>
        /// Consultar
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "CatVdrRutVta")]
        public string CatVdrRutVta { get; set; }
    }
}
