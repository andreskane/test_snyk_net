using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessFailNotificationTests
    {
        [TestMethod()]
        public void TruckProcessFailNotificationTest()
        {
            var result = new TruckProcessFailNotification(1, "TEST", DateTimeOffset.MinValue, "TEST", new List<string> { "TEST" });

            result.Should().NotBeNull();
            result.ChannelId.Should().Be("MENSAJES_TRUCK");
            result.StatusCode.Should().Be(201);
            result.StatusMessage.Should().Be("Procesado");
            result.Type.Should().Be("FINALIZADO_CON_ERRORES");
            result.Username.Should().Be("TEST");
            result.Payload.Should().NotBeNull();
            result.Messages.Should().HaveCount(1);
        }
    }
}