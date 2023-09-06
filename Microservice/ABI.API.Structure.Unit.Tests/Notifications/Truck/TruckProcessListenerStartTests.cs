using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Services.Interfaces;
using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessListenerStartTests
    {
        private static Mock<INotificationStatusService> _notificationServiceMock;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _notificationServiceMock = new Mock<INotificationStatusService>();
        }


        [TestMethod()]
        public void TruckProcessListenerStartTest()
        {
            var result = new TruckProcessListenerStart(null);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task HandleAsyncTest()
        {

            var listener = new TruckProcessListenerStart(_notificationServiceMock.Object);

            var broadcast = new TruckWritingEventStart
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            };

            await listener.HandleAsync(broadcast);

            _notificationServiceMock.Verify(v =>
                v.Notify(It.IsAny<string>(), It.Is<GenericResponse>(w => w.Type == "PROCESANDO_ESTRUCTURA"))
            );
        }
    }
}