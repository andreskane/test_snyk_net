using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PteczocoInput
    {
        /// <summary>
        /// eteczoco
        /// </summary>
        /// <value>eteczoco</value>
        [DataMember(Name = "teczoco")]
        public List<Eteczoco> Teczoco { get; set; }

        public PteczocoInput()
        {
            Teczoco = new List<Eteczoco>();
        }

    }
}
