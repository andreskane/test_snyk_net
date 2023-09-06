using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Etecdire
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
        /// Dirección de Ventas
        /// </summary>
        /// <value>Dirección de Ventas</value>
        [DataMember(Name = "ECDveId")]
        public int? ECDveId { get; set; }

        /// <summary>
        /// Desc. Dirección de Ventas
        /// </summary>
        /// <value>Desc. Dirección de Ventas</value>
        [DataMember(Name = "ECDveTxt")]
        public string ECDveTxt { get; set; }

        /// <summary>
        /// Desc. Dirección de Ventas
        /// </summary>
        /// <value>Desc. Dirección de Ventas</value>
        [DataMember(Name = "ECDveAbv")]
        public string ECDveAbv { get; set; }

        /// <summary>
        /// Director de Ventas
        /// </summary>
        /// <value>Director de Ventas</value>
        [DataMember(Name = "ECDveIdDve")]
        public long? ECDveIdDve { get; set; }

        /// <summary>
        /// Fuerza de ventas de dirección
        /// </summary>
        /// <value>Fuerza de ventas de dirección</value>
        [DataMember(Name = "ECDveIdFvt")]
        public int? ECDveIdFvt { get; set; }

        /// <summary>
        /// Usuario que Modifica
        /// </summary>
        /// <value>Usuario que Modifica</value>
        [DataMember(Name = "ECDveUsuMo")]
        public string ECDveUsuMo { get; set; }

        /// <summary>
        /// Fecha de Modificación
        /// </summary>
        /// <value>Fecha de Modificación</value>
        [DataMember(Name = "ECDveFecMo")]
        public DateTime? ECDveFecMo { get; set; }

        /// <summary>
        /// Hora de Modificación
        /// </summary>
        /// <value>Hora de Modificación</value>
        [DataMember(Name = "ECDveHorMo")]
        public string ECDveHorMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECDveTipCr")]
        public string ECDveTipCr { get; set; }

    }
}
