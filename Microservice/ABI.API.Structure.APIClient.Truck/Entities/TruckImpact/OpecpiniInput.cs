using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{

    [DataContract]
    public partial class OpecpiniInput
    {

        [DataMember(Name = "epeciniin")]
        public Epecini Epeciniin { get; set; }


        public OpecpiniInput()
        {
            Epeciniin = new Epecini();
        }


    }
}
