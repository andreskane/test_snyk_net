using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Epecini
    {
        /// <summary>
        /// Empresa
        /// </summary>
        /// <value>Empresa</value>
        [DataMember(Name = "Empresa")]
        public string Empresa { get; set; }

        /// <summary>
        /// Tipo Proceso
        /// </summary>
        /// <value>Tipo Proceso</value>
        [DataMember(Name = "TipoProceso")]
        public string TipoProceso { get; set; }

        /// <summary>
        /// Nro Ver
        /// </summary>
        /// <value>Nro Ver</value>
        [DataMember(Name = "NroVer")]
        public string NroVer { get; set; }

        /// <summary>
        /// Fecha Desde
        /// </summary>
        /// <value>Fecha Desde</value>
        [DataMember(Name = "FechaDesde")]
        public string FechaDesde { get; set; }

        /// <summary>
        /// Fecha Hasta
        /// </summary>
        /// <value>Fecha Hasta</value>
        [DataMember(Name = "FechaHasta")]
        public string FechaHasta { get; set; }

        /// <summary>
        /// Log Sts
        /// </summary>
        /// <value>Log Sts</value>
        [DataMember(Name = "LogSts")]
        public string LogSts { get; set; }


    }
}
