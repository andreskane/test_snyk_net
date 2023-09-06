using ABI.Framework.MS.Messages.EventBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Publishers
{
    public class TruckAclPublisher : MessagePublisher, ITruckAclPublisher
    {
        public TruckAclPublisher(IConfiguration configuration)
            : base(
                new TopicClient
                (
                    configuration["TruckAclServiceBus:ConnectionString"],
                    configuration["TruckAclServiceBus:TopicName"]
                ),
                null
            )
        {
            //
        }
    }
}
