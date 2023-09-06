using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ABI.API.Structure.Controllers.Base.Tests
{
    [TestClass()]
    public class AbiControllerBaseTests
    {
        [TestMethod()]
        public void ApiMenssageErrorTest()
        {
            var loggerMock = new Mock<ILogger>();
            var controller = new AbiControllerBase();
            var result = controller.ApiMenssageError(loggerMock.Object, "TEST");
            result.Should().NotBeNull();
        }
    }
}