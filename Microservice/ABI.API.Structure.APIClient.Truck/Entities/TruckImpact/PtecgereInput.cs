using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PtecgereInput
    {
        /// <summary>
        /// etecgere
        /// </summary>
        /// <value>etecgere</value>
        [DataMember(Name = "tecgere")]
        public List<Etecgere> Tecgere { get; set; }

        public PtecgereInput()
        {
            Tecgere = new List<Etecgere>();
        }

    }
}
