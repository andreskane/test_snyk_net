using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PtecareaInput
    {
        /// <summary>
        /// etecarea
        /// </summary>
        /// <value>etecarea</value>
        [DataMember(Name = "tecarea")]
        public List<Etecarea> Tecarea { get; set; }

        public PtecareaInput()
        {
            Tecarea = new List<Etecarea>();
        }

    }
}
