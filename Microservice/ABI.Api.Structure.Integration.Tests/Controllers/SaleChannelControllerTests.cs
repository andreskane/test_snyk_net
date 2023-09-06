using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class SaleChannelControllerTests : BaseControllerTests
    {

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAll");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllActiveTrueAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAll?active=true");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllActiveFalseAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAll?active=false");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAllForSelect");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveTrueAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAllForSelect?active=true");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllForSelectActiveFalseAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/SaleChannel/getAllForSelect?active=false");

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
            var response = await _client.GetAsync("/api/SaleChannel/getOne?id=1");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}