using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Notification.Tests
{
    [TestClass()]
    public class NotificationStatusServiceTests
    {
        [TestMethod()]
        public void NotificationStatusServiceTest()
        {
            var loggerMock = new Mock<ILogger<NotificationStatusService>>();
            var contextMock = new Mock<IHubContext<NotificationHub>>();
            var result = new NotificationStatusService(
                loggerMock.Object,
                contextMock.Object
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task NotificationStatusServiceNotifyTest()
        {
            var loggerMock = new Mock<ILogger<NotificationStatusService>>();
            var clientsMock = new Mock<IHubClients>();
            var clientProxyMock = new Mock<IClientProxy>();
            var contextMock = new Mock<IHubContext<NotificationHub>>();
            contextMock.Setup(s => s.Clients).Returns(clientsMock.Object);
            contextMock.Setup(s => s.Clients.Group(It.IsAny<string>())).Returns(clientProxyMock.Object);

            var notificationService = new NotificationStatusService(loggerMock.Object, contextMock.Object);
            var response = new GenericResponse();

            await notificationService.Notify("TEST", response);

            contextMock.Verify(context =>
                context.Clients.Group(It.IsAny<string>())
                    .SendCoreAsync(
                    "result",
                    It.Is<object[]>(w => w[0].GetType().Equals(typeof(GenericResponse))),
                    default
                ),
                Times.Once
            );
        }
    }
}