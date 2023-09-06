using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DataStructureLevel4
    {
        /// <summary>
        /// Código de Region
        /// </summary>
        /// <value>Código de Region</value>
        [DataMember(Name = "RegId")]
        public string RegId { get; set; }

        /// <summary>
        /// Descripción de Region
        /// </summary>
        /// <value>Descripción de Region</value>
        [DataMember(Name = "RegTxt")]
        public string RegTxt { get; set; }

        /// <summary>
        /// Desc.Reducida de Region
        /// </summary>
        /// <value>Desc.Reducida de Region</value>
        [DataMember(Name = "RegAbv")]
        public string RegAbv { get; set; }

        /// <summary>
        /// Jefe Id
        /// </summary>
        /// <value>Jefe Id</value>
        [DataMember(Name = "RegIdJfe")]
        public string RegIdJfe { get; set; }

        /// <summary>
        /// Jefe Nombre
        /// </summary>
        /// <value>Jefe Nombre</value>
        [DataMember(Name = "RegNom")]
        public string RegNom { get; set; }

        /// <summary>
        /// Fuerza Vta Región
        /// </summary>
        /// <value>Fuerza Vta Región</value>
        [DataMember(Name = "RegIdFvt")]
        public string RegIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "RegFvtAbv")]
        public string RegFvtAbv { get; set; }

        /// <summary>
        /// Asistente Id
        /// </summary>
        /// <value>Asistente Id</value>
        [DataMember(Name = "RegIdAsist")]
        public string RegIdAsist { get; set; }

        /// <summary>
        /// Nombre Asistente
        /// </summary>
        /// <value>Nombre Asistente</value>
        [DataMember(Name = "AsiNom")]
        public string AsiNom { get; set; }

        /// <summary>
        /// Gets or Sets Level5
        /// </summary>
        [DataMember(Name = "Level5")]
        public List<DataStructureLevel5> Level5 { get; set; }


    }
}
