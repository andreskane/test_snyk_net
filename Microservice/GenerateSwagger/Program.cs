using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;


namespace GenerateSwagger
{
    static class Program
    {



        static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower().Contains("swaggerpath"))
            {
                Console.WriteLine(await GenerateSwaggerAsync(args));

            }
        }

        private static async System.Threading.Tasks.Task<string> GenerateSwaggerAsync(string[] args)
        {
            TestServer _server;
            HttpClient _client;


            _server = new TestServer(new WebHostBuilder()

                .ConfigureServices(services => services.AddAutofac())
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false).AddEnvironmentVariables();
                }).UseStartup<ABI.API.Structure.Startup>());


            _client = _server.CreateClient();

            String SwaggerPath = "";


            var fileInfo = new FileInfo("swagger.json");
            SwaggerPath = fileInfo.FullName;
            Console.WriteLine(SwaggerPath);
            if (args.Length > 0)
            {
                var path = args[0].Split('=');
                if (path.Length > 0)
                {
                    SwaggerPath = $"{path[1].ToLower()}swagger.json";

                }


            }
            Console.WriteLine(SwaggerPath);

            var response = await _client.GetAsync("/swagger/v1/swagger.json");
            response.EnsureSuccessStatusCode();
            await using var ms = await response.Content.ReadAsStreamAsync();
            await using var fs = File.Create(SwaggerPath);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);

            return SwaggerPath.ToString();
        }


    }
}
