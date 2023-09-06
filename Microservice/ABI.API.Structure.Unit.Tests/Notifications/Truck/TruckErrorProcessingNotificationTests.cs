using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckErrorProcessingNotificationTests
    {
        [TestMethod()]
        public void TruckErrorProcessingNotificationTest()
        {
            var result = new TruckProcessingErrorNotification(1, "TEST", DateTimeOffset.MinValue, "TEST");

            result.Should().NotBeNull();
            result.ChannelId.Should().Be("MENSAJES_TRUCK");
            result.StatusCode.Should().Be(200);
            result.StatusMessage.Should().Be("Procesando");
            result.Type.Should().Be("PROCESANDO_CON_ERRORES");
            result.Username.Should().Be("TEST");
            result.Payload.Should().NotBeNull();
            result.Messages.Should().NotBeEmpty();
        }
    }
}