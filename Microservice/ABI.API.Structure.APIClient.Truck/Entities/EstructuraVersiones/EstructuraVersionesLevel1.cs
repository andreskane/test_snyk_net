using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EstructuraVersionesLevel1
    {
        /// <summary>
        /// Empresa de Trabajo
        /// </summary>
        /// <value>Empresa de Trabajo</value>
        [DataMember(Name = "EmpId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "EmpId")]
        public int? EmpId { get; set; }

        /// <summary>
        /// Nro de Versión
        /// </summary>
        /// <value>Nro de Versión</value>
        [DataMember(Name = "ECVerNro", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECVerNro")]
        public long? ECVerNro { get; set; }

        /// <summary>
        /// Fecha Desde
        /// </summary>
        /// <value>Fecha Desde</value>
        [DataMember(Name = "ECFecDes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecDes")]
        public string ECFecDes { get; set; }

        /// <summary>
        /// Fecha Hasta
        /// </summary>
        /// <value>Fecha Hasta</value>
        [DataMember(Name = "ECFecHas", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecHas")]
        public string ECFecHas { get; set; }

        /// <summary>
        /// Código de Estado
        /// </summary>
        /// <value>Código de Estado</value>
        [DataMember(Name = "ECStsCod", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECStsCod")]
        public string ECStsCod { get; set; }

        /// <summary>
        /// Usuario que Aprueba
        /// </summary>
        /// <value>Usuario que Aprueba</value>
        [DataMember(Name = "ECUsuApr", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECUsuApr")]
        public string ECUsuApr { get; set; }

        /// <summary>
        /// Fecha de aprobación/rechazo
        /// </summary>
        /// <value>Fecha de aprobación/rechazo</value>
        [DataMember(Name = "ECFecApr", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecApr")]
        public string ECFecApr { get; set; }

        /// <summary>
        /// Hora de aprobación/rechazo
        /// </summary>
        /// <value>Hora de aprobación/rechazo</value>
        [DataMember(Name = "ECHorApr", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECHorApr")]
        public string ECHorApr { get; set; }

        /// <summary>
        /// Usuario alta
        /// </summary>
        /// <value>Usuario alta</value>
        [DataMember(Name = "ECUsuAlt", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECUsuAlt")]
        public string ECUsuAlt { get; set; }

        /// <summary>
        /// EC - Fecha alta
        /// </summary>
        /// <value>EC - Fecha alta</value>
        [DataMember(Name = "ECFecAlt", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecAlt")]
        public string ECFecAlt { get; set; }

        /// <summary>
        /// EC - Hora alta
        /// </summary>
        /// <value>EC - Hora alta</value>
        [DataMember(Name = "ECHorAlt", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECHorAlt")]
        public string ECHorAlt { get; set; }

        /// <summary>
        /// EC - Usuario que modifica
        /// </summary>
        /// <value>EC - Usuario que modifica</value>
        [DataMember(Name = "ECUsuMod", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECUsuMod")]
        public string ECUsuMod { get; set; }

        /// <summary>
        /// EC - Fecha de modificación
        /// </summary>
        /// <value>EC - Fecha de modificación</value>
        [DataMember(Name = "ECFecMod", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecMod")]
        public string ECFecMod { get; set; }

        /// <summary>
        /// EC - Hora de modificación
        /// </summary>
        /// <value>EC - Hora de modificación</value>
        [DataMember(Name = "ECHorMod", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECHorMod")]
        public string ECHorMod { get; set; }

        /// <summary>
        /// Tipo de creación
        /// </summary>
        /// <value>Tipo de creación</value>
        [DataMember(Name = "ECTipCre", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECTipCre")]
        public string ECTipCre { get; set; }

        /// <summary>
        /// Indicación de Traspaso
        /// </summary>
        /// <value>Indicación de Traspaso</value>
        [DataMember(Name = "ECIndTra", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECIndTra")]
        public string ECIndTra { get; set; }

        /// <summary>
        /// Fecha de Transpaso
        /// </summary>
        /// <value>Fecha de Transpaso</value>
        [DataMember(Name = "ECFecTra", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECFecTra")]
        public string ECFecTra { get; set; }

        /// <summary>
        /// Hora de Traspaso
        /// </summary>
        /// <value>Hora de Traspaso</value>
        [DataMember(Name = "ECHorTra", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECHorTra")]
        public string ECHorTra { get; set; }

        /// <summary>
        /// Usuario de Traspaso
        /// </summary>
        /// <value>Usuario de Traspaso</value>
        [DataMember(Name = "ECUsuTra", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECUsuTra")]
        public string ECUsuTra { get; set; }

        /// <summary>
        /// Mensaje de Inconsistencia
        /// </summary>
        /// <value>Mensaje de Inconsistencia</value>
        [DataMember(Name = "ECIncMsg", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECIncMsg")]
        public string ECIncMsg { get; set; }

    }
}
