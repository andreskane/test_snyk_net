using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class AttentionModeControllerTests : BaseControllerTests
    {

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/getAll");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllActiveTrueAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/getAll?active=true");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllAsyncActiveFalseTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/getAll?active=false");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/GetAllForSelect");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveTrueAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/GetAllForSelect?active=true");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveFalseAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/GetAllForSelect?active=false");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetOneAsyncTest()
        {
            var response = await _client.GetAsync("/api/AttentionMode/getOne?id=1");
            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}