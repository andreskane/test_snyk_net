using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PtecregiInput
    {
        /// <summary>
        /// etecregi
        /// </summary>
        /// <value>etecregi</value>
        [DataMember(Name = "tecregi")]
        public List<Etecregi> Tecregi { get; set; }

        public PtecregiInput()
        {
            Tecregi = new List<Etecregi>();
        }

    }
}
