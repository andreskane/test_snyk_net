using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Etecarea
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
        /// Area de Ventas
        /// </summary>
        /// <value>Area de Ventas</value>
        [DataMember(Name = "ECAveId")]
        public int? ECAveId { get; set; }

        /// <summary>
        /// Descrip del Area de Ventas
        /// </summary>
        /// <value>Descrip del Area de Ventas</value>
        [DataMember(Name = "ECAveTxt")]
        public string ECAveTxt { get; set; }

        /// <summary>
        /// Descrip del Area de Ventas
        /// </summary>
        /// <value>Descrip del Area de Ventas</value>
        [DataMember(Name = "ECAveAbv")]
        public string ECAveAbv { get; set; }

        /// <summary>
        /// DirecciÃ³n de Ventas
        /// </summary>
        /// <value>Dirección de Ventas</value>
        [DataMember(Name = "ECDveId")]
        public int? ECDveId { get; set; }

        /// <summary>
        /// Gerente de Area
        /// </summary>
        /// <value>Gerente de Area</value>
        [DataMember(Name = "ECAveIdGea")]
        public long? ECAveIdGea { get; set; }

        /// <summary>
        /// Fuerza de ventas de dirección
        /// </summary>
        /// <value>Fuerza de ventas de dirección</value>
        [DataMember(Name = "ECAveIdFvt")]
        public int? ECAveIdFvt { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECAveUsuMo")]
        public string ECAveUsuMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECAveFecMo")]
        public DateTime? ECAveFecMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECAveHorMo")]
        public string ECAveHorMo { get; set; }

        /// <summary>
        /// Tipo de CreaciÃ³n
        /// </summary>
        /// <value>Tipo de Creación</value>
        [DataMember(Name = "ECAveTipCr")]
        public string ECAveTipCr { get; set; }

    }
}
