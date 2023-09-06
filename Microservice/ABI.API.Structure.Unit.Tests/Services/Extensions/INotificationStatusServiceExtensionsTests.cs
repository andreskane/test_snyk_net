using ABI.API.Structure.Application.Services.Interfaces;
using ABI.Framework.MS.Helpers.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Extensions.Tests
{
    [TestClass()]
    public class INotificationStatusServiceExtensionsTests
    {
        [TestMethod()]
        public async Task NotifyTest()
        {
            var notificationServiceMock = new Mock<INotificationStatusService>();
            var notificationMock = new Mock<INotificationMessage<object>>();
            notificationMock.Setup(s => s.Messages).Returns(new List<string>());

            await INotificationStatusServiceExtensions.Notify(notificationServiceMock.Object, notificationMock.Object);

            notificationServiceMock.Verify(service =>
                service.Notify(It.IsAny<string>(), It.IsAny<GenericResponse>()),
                Times.Once
            );
        }
    }
}