using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessWarningNotificationTests
    {
        [TestMethod()]
        public void TruckProcessWarningNotificationTest()
        {
            var result = new TruckProcessWarningNotification(1, "TEST", DateTimeOffset.MinValue, "user" ,"TEST");

            result.Should().NotBeNull();
            result.ChannelId.Should().Be("MENSAJES_TRUCK");
            result.StatusCode.Should().Be(200);
            result.StatusMessage.Should().Be("Advertencia");
            result.Type.Should().Be("ADVERTENCIAS");
            result.Username.Should().Be("user");
            result.Payload.Should().NotBeNull();
            result.Messages.Should().NotBeEmpty();
        }
    }
}