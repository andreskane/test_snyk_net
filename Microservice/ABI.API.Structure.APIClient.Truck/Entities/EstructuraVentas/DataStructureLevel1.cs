using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DataStructureLevel1
    {
        /// <summary>
        /// Dve Id
        /// </summary>
        /// <value>Dve Id</value>
        [DataMember(Name = "DveId")]
        public string DveId { get; set; }

        /// <summary>
        /// Descripción Dcción Ventas
        /// </summary>
        /// <value>Descripción Dcción Ventas</value>
        [DataMember(Name = "DveTxt")]
        public string DveTxt { get; set; }

        /// <summary>
        /// Descr.Red.Dcción Vtas
        /// </summary>
        /// <value>Descr.Red.Dcción Vtas</value>
        [DataMember(Name = "DveAbv")]
        public string DveAbv { get; set; }

        /// <summary>
        /// Fza de Ventas de Dirección
        /// </summary>
        /// <value>Fza de Ventas de Dirección</value>
        [DataMember(Name = "DveIdFvt")]
        public string DveIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "DveFvtAbv")]
        public string DveFvtAbv { get; set; }

        /// <summary>
        /// Director de Ventas
        /// </summary>
        /// <value>Director de Ventas</value>
        [DataMember(Name = "DveIdDve")]
        public string DveIdDve { get; set; }

        /// <summary>
        /// Nombre Director de Ventas
        /// </summary>
        /// <value>Nombre Director de Ventas</value>
        [DataMember(Name = "DveNom")]
        public string DveNom { get; set; }

        /// <summary>
        /// Gets or Sets Level2
        /// </summary>
        [DataMember(Name = "Level2")]
        public List<DataStructureLevel2> Level2 { get; set; }


    }
}
