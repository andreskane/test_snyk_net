using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PteczonaInput
    {
        /// <summary>
        /// eteczona
        /// </summary>
        /// <value>eteczona</value>
        [DataMember(Name = "teczona")]
        public List<Eteczona> Teczona { get; set; }

        public PteczonaInput()
        {
            Teczona = new List<Eteczona>();
        }

    }
}
