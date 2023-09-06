using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Eteczona
    {
        /// <summary>
        /// Empresa de Trabajo
        /// </summary>
        /// <value>Empresa de Trabajo</value>
        [DataMember(Name = "EmpId")]
        public int? EmpId { get; set; }

        /// <summary>
        /// Nro de Versión
        /// </summary>
        /// <value>Nro de Versión</value>
        [DataMember(Name = "ECVerNro")]
        public long? ECVerNro { get; set; }

        /// <summary>
        /// Zona de Ventas
        /// </summary>
        /// <value>Zona de Ventas</value>
        [DataMember(Name = "ECZonId")]
        public string ECZonId { get; set; }

        /// <summary>
        /// Descripción de Zona
        /// </summary>
        /// <value>Descripción de Zona</value>
        [DataMember(Name = "ECZonTxt")]
        public string ECZonTxt { get; set; }

        /// <summary>
        /// Desc. Reducida Zona de Ventas
        /// </summary>
        /// <value>Desc. Reducida Zona de Ventas</value>
        [DataMember(Name = "ECZonAbv")]
        public string ECZonAbv { get; set; }

        /// <summary>
        /// Región de Ventas
        /// </summary>
        /// <value>Región de Ventas</value>
        [DataMember(Name = "ECRegId")]
        public int? ECRegId { get; set; }

        /// <summary>
        /// Supervisor
        /// </summary>
        /// <value>Supervisor</value>
        [DataMember(Name = "ECZonIdSup")]
        public long? ECZonIdSup { get; set; }

        /// <summary>
        /// Fuerza de ventas de Zona
        /// </summary>
        /// <value>Fuerza de ventas de Zona</value>
        [DataMember(Name = "ECZonIdFvt")]
        public int? ECZonIdFvt { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECZonUsuMo")]
        public string ECZonUsuMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECZonFecMo")]
        public DateTime? ECZonFecMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECZonHorMo")]
        public string ECZonHorMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECZonTipCr")]
        public string ECZonTipCr { get; set; }

    }
}
