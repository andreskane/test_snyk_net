using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DataStructureLevel5
    {
        /// <summary>
        /// Código de Zona Venta
        /// </summary>
        /// <value>Código de Zona Venta</value>
        [DataMember(Name = "ZonId")]
        public string ZonId { get; set; }

        /// <summary>
        /// Descripción de la Zona de Vent
        /// </summary>
        /// <value>Descripción de la Zona de Vent</value>
        [DataMember(Name = "ZonTxt")]
        public string ZonTxt { get; set; }

        /// <summary>
        /// Descripción reducida de Zona
        /// </summary>
        /// <value>Descripción reducida de Zona</value>
        [DataMember(Name = "ZonAbv")]
        public string ZonAbv { get; set; }

        /// <summary>
        /// Identificador del Supervisor
        /// </summary>
        /// <value>Identificador del Supervisor</value>
        [DataMember(Name = "ZonIdSup")]
        public string ZonIdSup { get; set; }

        /// <summary>
        /// Supervisor Nombre
        /// </summary>
        /// <value>Supervisor Nombre</value>
        [DataMember(Name = "ZonNom")]
        public string ZonNom { get; set; }

        /// <summary>
        /// Fuerza de Venta Zona
        /// </summary>
        /// <value>Fuerza de Venta Zona</value>
        [DataMember(Name = "ZonIdFvt")]
        public string ZonIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "ZonFvtAbv")]
        public string ZonFvtAbv { get; set; }

        /// <summary>
        /// Coordinador de la Zona
        /// </summary>
        /// <value>Coordinador de la Zona</value>
        [DataMember(Name = "ZonIdCoor")]
        public string ZonIdCoor { get; set; }

        /// <summary>
        /// Coordinador Nombre
        /// </summary>
        /// <value>Coordinador Nombre</value>
        [DataMember(Name = "CooNom")]
        public string CooNom { get; set; }

        /// <summary>
        /// Promotor de la Zona
        /// </summary>
        /// <value>Promotor de la Zona</value>
        [DataMember(Name = "ZonIdPrm")]
        public string ZonIdPrm { get; set; }

        /// <summary>
        /// Nombre de Promotor
        /// </summary>
        /// <value>Nombre de Promotor</value>
        [DataMember(Name = "PrmNom")]
        public string PrmNom { get; set; }

        /// <summary>
        /// Gets or Sets Level6
        /// </summary>
        [DataMember(Name = "Level6")]
        public List<DataStructureLevel6> Level6 { get; set; }

    }
}
