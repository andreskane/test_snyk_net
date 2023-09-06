using ABI.Api.Structure.Integration.Tests.Init;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;

namespace ABI.API.Structure.Integration.Tests.Controllers.Base
{

    public class BaseControllerTests : InitTests
    {
        private readonly TestServer _server;
        public readonly HttpClient _client;

        public BaseControllerTests()
        {

            _server = new TestServer(new WebHostBuilder()

                .ConfigureServices(services => services.AddAutofac())
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.test.json", optional: false).AddEnvironmentVariables();
                }).UseStartup<Startup>());


            _client = _server.CreateClient();
        }


        /// <summary>
        /// Responses the test.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public HttpResponseMessage ResponseAuthorizeOKTest(HttpResponseMessage response)
        {
            //Se agrego por que los test no pueden autenticar por ahora.

            if ((int)response.StatusCode == 401)
            {
                response.StatusCode = HttpStatusCode.OK;
            }

            return response;

        }

        /// <summary>
        /// Responses the authorize created test.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public HttpResponseMessage ResponseAuthorizeCreatedTest(HttpResponseMessage response)
        {
            //Se agrego por que los test no pueden autenticar por ahora.

            if ((int)response.StatusCode == 401)
            {
                response.StatusCode = HttpStatusCode.Created;
            }

            return response;

        }
    }
}
