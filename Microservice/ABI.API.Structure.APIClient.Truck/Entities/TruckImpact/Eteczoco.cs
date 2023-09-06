using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Eteczoco
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
        /// Coordinador de Ventas
        /// </summary>
        /// <value>Coordinador de Ventas</value>
        [DataMember(Name = "ECZonIdCoo")]
        public long? ECZonIdCoo { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECZoCoUsMo")]
        public string ECZoCoUsMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECZoCoFeMo")]
        public DateTime? ECZoCoFeMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECZoCoHoMo")]
        public string ECZoCoHoMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECZoCoTipCr")]
        public string ECZoCoTipCr { get; set; }

    }
}
