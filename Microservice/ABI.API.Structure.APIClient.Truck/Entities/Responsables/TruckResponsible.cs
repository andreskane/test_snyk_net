using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.Responsables
{
    [DataContract]
    public class TruckResponsible
    {
        /// <summary>
        /// Responsables
        /// </summary>
        /// <value>Responsables</value>
        [DataMember(Name = "Responsables")]
        public Responsible Responsible { get; set; }

    }
}
