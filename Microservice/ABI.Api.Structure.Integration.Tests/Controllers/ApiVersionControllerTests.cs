using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class ApiVersionControllerTests : BaseControllerTests
    {
        [TestMethod()]
        public async Task GetTest()
        {
            var response = await _client.GetAsync("/api/Version");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}