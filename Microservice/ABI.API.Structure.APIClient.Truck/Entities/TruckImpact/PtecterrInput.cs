using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PtecterrInput
    {
        /// <summary>
        /// etecterr
        /// </summary>
        /// <value>etecterr</value>
        [DataMember(Name = "tecterr")]
        public List<Etecterr> Tecterr { get; set; }

        public PtecterrInput()
        {
            Tecterr = new List<Etecterr>();
        }

    }
}
