using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EstructuraVersionInput
    {
        /// <summary>
        /// Gets or Sets EmpId
        /// </summary>
        [DataMember(Name = "EmpId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "EmpId")]
        public int? EmpId { get; set; }

        /// <summary>
        /// Gets or Sets ECVerNro
        /// </summary>
        [DataMember(Name = "ECVerNro", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ECVerNro")]
        public string ECVerNro { get; set; }


    }
}
