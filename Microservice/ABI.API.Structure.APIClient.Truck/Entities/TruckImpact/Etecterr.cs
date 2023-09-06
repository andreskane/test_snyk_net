using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Etecterr
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
        /// Territorio de Venta
        /// </summary>
        /// <value>Territorio de Venta</value>
        [DataMember(Name = "ECTrrId")]
        public long? ECTrrId { get; set; }

        /// <summary>
        /// Territorio de Ventas
        /// </summary>
        /// <value>Territorio de Ventas</value>
        [DataMember(Name = "ECTrrTxt")]
        public string ECTrrTxt { get; set; }

        /// <summary>
        /// Zona de Ventas
        /// </summary>
        /// <value>Zona de Ventas</value>
        [DataMember(Name = "ECZonId")]
        public string ECZonId { get; set; }

        /// <summary>
        /// Cdigo del Vendedor
        /// </summary>
        /// <value>Cdigo del Vendedor</value>
        [DataMember(Name = "VdrCod")]
        public long? VdrCod { get; set; }

        /// <summary>
        /// Desc. Territorio de Venta
        /// </summary>
        /// <value>Desc. Territorio de Venta</value>
        [DataMember(Name = "ECTrrAbv")]
        public string ECTrrAbv { get; set; }

        /// <summary>
        /// Fuerza de ventas de Zona
        /// </summary>
        /// <value>Fuerza de ventas de Zona</value>
        [DataMember(Name = "ECTrrFvtId")]
        public int? ECTrrFvtId { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        /// <value>Usuario que modifica</value>
        [DataMember(Name = "ECTrrUsuMo")]
        public string ECTrrUsuMo { get; set; }

        /// <summary>
        /// Fecha de modificación
        /// </summary>
        /// <value>Fecha de modificación</value>
        [DataMember(Name = "ECTrrFecMo")]
        public DateTime? ECTrrFecMo { get; set; }

        /// <summary>
        /// Hora de modificación
        /// </summary>
        /// <value>Hora de modificación</value>
        [DataMember(Name = "ECTrrHorMo")]
        public string ECTrrHorMo { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECTrrTipCr")]
        public string ECTrrTipCr { get; set; }

        /// <summary>
        /// Tipo de Vendedor
        /// </summary>
        /// <value>Tipo de Vendedor</value>
        [DataMember(Name = "TpoVdrId")]
        public int? TpoVdrId { get; set; }

        /// <summary>
        /// Identificador de Merchandiser
        /// </summary>
        /// <value>Identificador de Merchandiser</value>
        [DataMember(Name = "ECTrrIdMer")]
        public long? ECTrrIdMer { get; set; }

    }
}
