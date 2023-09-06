using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class AttentionModeRoleConfigurationControllerTests
    {
        private static AttentionModeRoleConfigurationController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<AttentionModeRoleConfigurationController>>();
            var mediatrMock = new Mock<IMediator>();
            _controller = new AttentionModeRoleConfigurationController(loggerMock.Object, mediatrMock.Object);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var result = await _controller.GetAllAsync();
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }
    }
}