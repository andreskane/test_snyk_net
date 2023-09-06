using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TruckImpact
{

    [DataContract]
    public partial class OpecpiniOut
    {

        [DataMember(Name = "epeciniout")]
        public Epecini Epeciniin { get; set; }

        [DataMember(Name = "msglog")]
        public MsgLog Msglog { get; set; }

        public OpecpiniOut()
        {
            Epeciniin = new Epecini();
            Msglog = new MsgLog();

        }


    }
}
