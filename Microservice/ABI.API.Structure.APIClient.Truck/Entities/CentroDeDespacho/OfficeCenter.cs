using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CentroDeDespacho
{
    [DataContract]
    public class OfficeCenter
    {
        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<OfficeCenterLevel1> Level1 { get; set; }
    }
}
