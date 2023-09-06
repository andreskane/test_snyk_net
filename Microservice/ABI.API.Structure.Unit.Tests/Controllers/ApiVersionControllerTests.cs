using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class ApiVersionControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var controller = new ApiVersionController();
            var result = controller.Get();
            result.Should().NotBeNull();
        }
    }
}