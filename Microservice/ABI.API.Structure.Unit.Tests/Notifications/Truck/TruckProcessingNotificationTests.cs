using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessingNotificationTests
    {
        [TestMethod()]
        public void TruckProcessingNotificationTest()
        {
            var result = new TruckProcessingNotification(1, "TEST", DateTimeOffset.MinValue, "TEST");

            result.Should().NotBeNull();
            result.ChannelId.Should().Be("MENSAJES_TRUCK");
            result.StatusCode.Should().Be(200);
            result.StatusMessage.Should().Be("Procesando");
            result.Type.Should().Be("PROCESANDO_ESTRUCTURA");
            result.Username.Should().Be("TEST");
            result.Payload.Should().NotBeNull();
            result.Messages.Should().BeEmpty();
        }
    }
}