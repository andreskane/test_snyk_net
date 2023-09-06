using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABI.API.Structure.ACL.Truck.Infrastructure.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Configuration;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Publishers.Tests
{
    [TestClass()]
    public class TruckAclPublisherTests
    {
        [TestMethod()]
        public void TruckAclPublisherTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"TruckAclServiceBus:ConnectionString", "Endpoint=sb://abi-las-dev-brs-servicebus.servicebus.windows.net/;SharedAccessKeyName=TEST;SharedAccessKey=TEST"},
                {"TruckAclServiceBus:TopicName", "TEST"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var result = new TruckAclPublisher(configuration);

            result.Should().NotBeNull();
        }
    }
}