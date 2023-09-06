using ABI.API.Structure.Integration.Tests.Controllers.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class RequestsTrayControllerTests : BaseControllerTests
    {
        [TestMethod()]
        public async Task GetFiltersOptionsTest()
        {
            var response = await _client.GetAsync("/api/RequestsTray/getFiltersOptions");

            response = ResponseAuthorizeOKTest(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod()]
        public async Task GetOneWithDetailTest()
        {
            //var response = await _client.GetAsync("/api/RequestsTray/getOneWithDetail");

            //response = ResponseAuthorizeOKTest(response);
            //response.StatusCode.Should().Be(HttpStatusCode.OK);

            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task GetAllPaginatedSearchTest()
        {
            //var response = await _client.GetAsync("/api/RequestsTray/getAllPaginatedSearch");

            //response = ResponseAuthorizeOKTest(response);
            //response.StatusCode.Should().Be(HttpStatusCode.OK);

            Assert.Inconclusive();
        }
    }
}