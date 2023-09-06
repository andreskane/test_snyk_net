using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {

//# 2.5MB - 2621440
//# 5MB - 5242880
//# 10MB - 10485760
//# 20MB - 20971520
//# 50MB - 5242880
//# 100MB 104857600
//# 250MB - 214958080
//# 500MB - 429916160

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(
                new JsonFormatter(), 
                "logs/log.json",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 2621440,
                retainedFileCountLimit: 15
                
    
                )
                //.WriteTo.File(
                
                //    "logs/log.txt",
                //    rollingInterval: RollingInterval.Day,
                //    fileSizeLimitBytes: 2621440,
                //    retainedFileCountLimit: 15,
                //    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{Properties:j}{NewLine}{Exception}"
                //    )
                .WriteTo.Console(LogEventLevel.Debug)
                .WriteTo.Debug(LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .CreateLogger();

            try
            {
                Log.Information(string.Format("Starting up {0}", typeof(Startup).Assembly.GetName().FullName.ToString()));
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>


    Host.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((hostContext, config) =>
         {
             var env = hostContext.HostingEnvironment;

             config.AddJsonFile("assets/config/config.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
         })
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder => webBuilder

        .UseStartup<Startup>());




    }
}
