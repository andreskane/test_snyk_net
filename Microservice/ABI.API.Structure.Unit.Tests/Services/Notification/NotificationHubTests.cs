using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Notification.Tests
{
    [TestClass()]
    public class NotificationHubTests
    {
        [TestMethod()]
        public async Task NotificationHubAssociateTest()
        {
            var contextMock = new Mock<HubCallerContext>();
            var clientsMock = new Mock<IHubCallerClients>();
            var clientProxyMock = new Mock<IClientProxy>();
            var groupsMock = new Mock<IGroupManager>();
            var connId = Guid.NewGuid().ToString();

            groupsMock
                .Setup(s => s.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), default))
                .Returns(Task.FromResult(true));

            clientsMock
                .Setup(s => s.All)
                .Returns(clientProxyMock.Object);

            contextMock
                .Setup(s => s.ConnectionId)
                .Returns(connId);

            var hub = new NotificationHub()
            {
                Context = contextMock.Object,
                Clients = clientsMock.Object,
                Groups = groupsMock.Object
            };

            await hub.AssociateHub("TEST");

            groupsMock.Verify(groups => groups.AddToGroupAsync(connId, "TEST", default));
        }

        [TestMethod()]
        public async Task NotificationHubIsProcessingTest()
        {
            var hub = new NotificationHub();

            await hub.Invoking(i => i.IsProcessing())
                .Should().NotThrowAsync();
        }
    }
}