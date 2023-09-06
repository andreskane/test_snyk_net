using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstadoApi
{
    public class TruckStatus
    {
        /// <summary>
        /// Gets or Sets DatoOut
        /// </summary>
        [DataMember(Name = "DatoOut")]
        public string DatoOut { get; set; }
    }
}
