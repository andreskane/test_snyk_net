using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MsgLog
    {
        /// <summary>
        /// Estado
        /// </summary>
        /// <value>Estado</value>
        [DataMember(Name = "Level1")]
        public List<MsgLogLevel1> Level1 { get; set; }

    }
}
