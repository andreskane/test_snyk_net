using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class RoleControllerTests : BaseControllerTests
    {


        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Role/getAll");

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
            var response = await _client.GetAsync("/api/Role/getAll?active=true");

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
            var response = await _client.GetAsync("/api/Role/getAll?active=false");

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
            var response = await _client.GetAsync("/api/Role/getAllForSelect");

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
            var response = await _client.GetAsync("/api/Role/getAllForSelect?active=true");

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
            var response = await _client.GetAsync("/api/Role/getAllForSelect?active=false");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetAllTagsAsyncTest()
        {
            //arragne
            var response = await _client.GetAsync("/api/Role/getAllTags");

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
            var response = await _client.GetAsync("/api/Role/getOne?id=1");

            response = ResponseAuthorizeOKTest(response);

            //Assert
            Assert.IsTrue((int)response.StatusCode == 200);
            //fluent assertion
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task CreateDataAsyncTest()
        {
            var entity = new RoleDTO
            {
                Name = "NEWROLE",
                ShortName = "NWR",
                Erasable = false,
                Active = true,
                AttentionMode = null,
                Tag = null

            };
            var body = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Role/AddItem", body);

            response = ResponseAuthorizeCreatedTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}