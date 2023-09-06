using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PtecdireInput
    {
        /// <summary>
        /// etecdire
        /// </summary>
        /// <value>etecdire</value>
        [DataMember(Name = "tecdire")]
        public List<Etecdire> Tecdire { get; set; }


        public PtecdireInput()
        {
            Tecdire = new List<Etecdire>();
        }

    }
}
