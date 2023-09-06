using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Etecregi
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
        /// Región de Ventas
        /// </summary>
        /// <value>Región de Ventas</value>
        [DataMember(Name = "ECRegId")]
        public int? ECRegId { get; set; }

        /// <summary>
        /// Desc. Reducida Región de Venta
        /// </summary>
        /// <value>Desc. Reducida Región de Venta</value>
        [DataMember(Name = "ECRegAbv")]
        public string ECRegAbv { get; set; }

        /// <summary>
        /// Descripción de la Región
        /// </summary>
        /// <value>Descripción de la Región</value>
        [DataMember(Name = "ECRegTxt")]
        public string ECRegTxt { get; set; }

        /// <summary>
        /// Gerencia de Ventas
        /// </summary>
        /// <value>Gerencia de Ventas</value>
        [DataMember(Name = "ECGrcId")]
        public int? ECGrcId { get; set; }

        /// <summary>
        /// Jefe de la Región
        /// </summary>
        /// <value>Jefe de la Región</value>
        [DataMember(Name = "ECRegIdJfe")]
        public long? ECRegIdJfe { get; set; }

        /// <summary>
        /// Fuerza de ventas de Región
        /// </summary>
        /// <value>Fuerza de ventas de Región</value>
        [DataMember(Name = "ECRegIdFvt")]
        public int? ECRegIdFvt { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECRegUsuMo")]
        public string ECRegUsuMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECRegFecMo")]
        public DateTime? ECRegFecMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECRegHorMo")]
        public string ECRegHorMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECRegTipCr")]
        public string ECRegTipCr { get; set; }

    }
}
