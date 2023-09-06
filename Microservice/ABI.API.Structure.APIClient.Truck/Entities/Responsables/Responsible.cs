using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.Responsables
{
    [DataContract]
    public class Responsible
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<ResponsibleLevel1> Level1 { get; set; }
    }
}
