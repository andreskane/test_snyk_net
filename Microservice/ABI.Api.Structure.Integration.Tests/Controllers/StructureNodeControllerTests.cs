using ABI.API.Structure.Integration.Tests.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class StructureNodeControllerTests : BaseControllerTests
    {
        [TestMethod()]
        public async Task GetScheduledDatesByStructureAsyncTest()
        {
            //var response = await _client.GetAsync("/api/StructureNode/getScheduledDatesByStructure?id=1");

            //response = ResponseAuthorizeOKTest(response);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureAsyncTest()
        {
            //var response = await _client.GetAsync("/api/StructureNode/getAllByScheduledStructure?id=1&code=VTA&validity=2020-01-01");

            //response = ResponseAuthorizeOKTest(response);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureV2AsyncTest()
        {
            //_client.DefaultRequestHeaders.Add("X-Version", "2.0");
            //var response = await _client.GetAsync("/api/StructureNode/getAllByScheduledStructure?id=1&code=VTA&validity=2020-01-01");

            //response = ResponseAuthorizeOKTest(response);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Inconclusive();
        }
    }
}