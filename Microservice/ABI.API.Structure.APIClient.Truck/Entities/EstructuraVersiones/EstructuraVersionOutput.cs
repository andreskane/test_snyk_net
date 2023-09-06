using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EstructuraVersionOutput
    {
        /// <summary>
        /// Estructura Versiones
        /// </summary>
        /// <value>Estructura Versiones</value>
        [DataMember(Name = "EstructuraVersiones", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "EstructuraVersiones")]
        public EstructuraVersiones EstructuraVersiones { get; set; }

    }
}
