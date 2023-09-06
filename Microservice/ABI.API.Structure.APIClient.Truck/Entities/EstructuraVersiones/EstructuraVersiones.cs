using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EstructuraVersiones
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Level1")]
        public List<EstructuraVersionesLevel1> Level1 { get; set; }

    }
}
