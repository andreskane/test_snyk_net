using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DataStructureLevel3
    {
        /// <summary>
        /// Gerencia de Ventas
        /// </summary>
        /// <value>Gerencia de Ventas</value>
        [DataMember(Name = "GrcId")]
        public string GrcId { get; set; }

        /// <summary>
        /// Descripción de la Gerencia
        /// </summary>
        /// <value>Descripción de la Gerencia</value>
        [DataMember(Name = "GrcTxt")]
        public string GrcTxt { get; set; }

        /// <summary>
        /// Desc.Reducida Gerencia
        /// </summary>
        /// <value>Desc.Reducida Gerencia</value>
        [DataMember(Name = "GrcAbv")]
        public string GrcAbv { get; set; }

        /// <summary>
        /// Fuerza de Venta de la Gerencia
        /// </summary>
        /// <value>Fuerza de Venta de la Gerencia</value>
        [DataMember(Name = "GrcIdFvt")]
        public string GrcIdFvt { get; set; }

        /// <summary>
        /// Desc.Reducida Fuerza de Venta
        /// </summary>
        /// <value>Desc.Reducida Fuerza de Venta</value>
        [DataMember(Name = "GrcFvtAbv")]
        public string GrcFvtAbv { get; set; }

        /// <summary>
        /// Grc Id Gte
        /// </summary>
        /// <value>Grc Id Gte</value>
        [DataMember(Name = "GrcIdGte")]
        public string GrcIdGte { get; set; }

        /// <summary>
        /// Gerente Nombre
        /// </summary>
        /// <value>Gerente Nombre</value>
        [DataMember(Name = "GrcNom")]
        public string GrcNom { get; set; }

        /// <summary>
        /// Gets or Sets Level4
        /// </summary>
        [DataMember(Name = "Level4")]
        public List<DataStructureLevel4> Level4 { get; set; }


    }
}
