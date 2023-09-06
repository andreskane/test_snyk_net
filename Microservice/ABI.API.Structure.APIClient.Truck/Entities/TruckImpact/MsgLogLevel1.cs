using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MsgLogLevel1
    {
        /// <summary>
        /// Estado
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "ECLogSts")]
        public string ECLogSts { get; set; }

        /// <summary>
        /// Mensaje
        /// </summary>
        /// <value>Mensaje</value>
        [DataMember(Name = "ECLogTxt")]
        public string ECLogTxt { get; set; }

        /// <summary>
        /// Fecha
        /// </summary>
        /// <value>Fecha</value>
        [DataMember(Name = "ECLogFch")]
        public DateTime? ECLogFch { get; set; }

        /// <summary>
        /// Secuencia
        /// </summary>
        /// <value>Secuencia</value>
        [DataMember(Name = "eclogsec")]
        public int? Eclogsec { get; set; }
    }
}
