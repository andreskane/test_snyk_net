using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class LevelControllerTests : BaseControllerTests
    {


        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Level/getAll");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [TestMethod()]
        public async Task GetAllAsyncActiveTrueTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Level/getAll?active=true");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [TestMethod()]
        public async Task GetAllAsyncActiveFalseTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Level/getAll?active=false");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [TestMethod()]
        public async Task GetAllForSelectAsyncTest()
        {
            var response = await _client.GetAsync("/api/Level/GetAllForSelect");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveTrueAsyncTest()
        {
            var response = await _client.GetAsync("/api/Level/GetAllForSelect?active=true");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveFalseAsyncTest()
        {
            var response = await _client.GetAsync("/api/Level/GetAllForSelect?active=false");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetOneAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Level/getOne?id=1");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


    }
}