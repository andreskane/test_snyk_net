using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Etecgere
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
        /// Gerencia de Ventas
        /// </summary>
        /// <value>Gerencia de Ventas</value>
        [DataMember(Name = "ECGrcId")]
        public int? ECGrcId { get; set; }

        /// <summary>
        /// Descripción de Gerencia de Ven
        /// </summary>
        /// <value>Descripción de Gerencia de Ven</value>
        [DataMember(Name = "ECGrcTxt")]
        public string ECGrcTxt { get; set; }

        /// <summary>
        /// Desc. Gerencia de Venta
        /// </summary>
        /// <value>Desc. Gerencia de Venta</value>
        [DataMember(Name = "ECGrcAbv")]
        public string ECGrcAbv { get; set; }

        /// <summary>
        /// Area de Ventas
        /// </summary>
        /// <value>Area de Ventas</value>
        [DataMember(Name = "ECAveId")]
        public int? ECAveId { get; set; }

        /// <summary>
        /// Gerente
        /// </summary>
        /// <value>Gerente</value>
        [DataMember(Name = "ECGrcIdGte")]
        public long? ECGrcIdGte { get; set; }

        /// <summary>
        /// Fuerza de ventas de gerencia
        /// </summary>
        /// <value>Fuerza de ventas de gerencia</value>
        [DataMember(Name = "ECGrcIdFvt")]
        public int? ECGrcIdFvt { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECGrcUsuMo")]
        public string ECGrcUsuMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECGrcFecMo")]
        public DateTime? ECGrcFecMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECGrcHorMo")]
        public string ECGrcHorMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECGrcTipCr")]
        public string ECGrcTipCr { get; set; }

    }
}
