using ABI.API.Structure.Integration.Tests.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class AttentionModeRoleConfigurationControllerTests : BaseControllerTests
    {
        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/AttentionModeRoleConfiguration/getAll");

            response = ResponseAuthorizeOKTest(response);
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task GetSellerTypeAllForSelectAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/AttentionModeRoleConfiguration/getSellerTypeAllForSelect");

            response = ResponseAuthorizeOKTest(response);
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Inconclusive();
        }

    }
}