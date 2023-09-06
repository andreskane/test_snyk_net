using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DataStructureLevel2
    {
        /// <summary>
        /// Area de Ventas
        /// </summary>
        /// <value>Area de Ventas</value>
        [DataMember(Name = "AveId")]
        public string AveId { get; set; }

        /// <summary>
        /// Descripción Area de Ventas
        /// </summary>
        /// <value>Descripción Area de Ventas</value>
        [DataMember(Name = "AveTxt")]
        public string AveTxt { get; set; }

        /// <summary>
        /// Descr.Red.Area de Ventas
        /// </summary>
        /// <value>Descr.Red.Area de Ventas</value>
        [DataMember(Name = "AveAbv")]
        public string AveAbv { get; set; }

        /// <summary>
        /// Cód.Gerente de Area
        /// </summary>
        /// <value>Cód.Gerente de Area</value>
        [DataMember(Name = "AveIdGea")]
        public string AveIdGea { get; set; }

        /// <summary>
        /// Nombre Gerente de Area
        /// </summary>
        /// <value>Nombre Gerente de Area</value>
        [DataMember(Name = "AveNom")]
        public string AveNom { get; set; }

        /// <summary>
        /// Ave Id Fvt
        /// </summary>
        /// <value>Ave Id Fvt</value>
        [DataMember(Name = "AveIdFvt")]
        public string AveIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "AveFvtAbv")]
        public string AveFvtAbv { get; set; }

        /// <summary>
        /// Gets or Sets Level3
        /// </summary>
        [DataMember(Name = "Level3")]
        public List<DataStructureLevel3> Level3 { get; set; }


    }
}
