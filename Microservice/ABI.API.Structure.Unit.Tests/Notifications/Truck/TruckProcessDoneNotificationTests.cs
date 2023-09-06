using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessDoneNotificationTests
    {
        [TestMethod()]
        public void TruckProcessDoneNotificationTest()
        {
            var result = new TruckProcessDoneNotification(1, "TEST", DateTimeOffset.MinValue, "TEST");

            result.Should().NotBeNull();
            result.ChannelId.Should().Be("MENSAJES_TRUCK");
            result.StatusCode.Should().Be(201);
            result.StatusMessage.Should().Be("Procesado");
            result.Type.Should().Be("FINALIZADO_SIN_ERRORES");
            result.Username.Should().Be("TEST");
            result.Payload.Should().NotBeNull();
            result.Messages.Should().BeEmpty();
        }
    }
}