using ABI.Framework.MS.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ABI.API.Structure
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var children = configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            ICacheStore cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            services.AddSingleton(cacheStore);
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOpenApiDocument(configure =>
                {
                    BaseConfigure(configuration, configure, "v1");
                })
                .AddOpenApiDocument(configure =>
                {
                    BaseConfigure(configuration, configure, "v2");
                });

            return services;
        }

        private static void BaseConfigure(IConfiguration configuration, AspNetCoreOpenApiDocumentGeneratorSettings configure, string version)
        {
            var strBuildVersion = "#{BuildVersion}#";
            var strVersion = typeof(Startup).Assembly.GetName().Version.ToString();

            //es para saber si cargo bien el json de configuracion por kubernetes
            string stra8kEnviroment = "<b>sin configuracion</b>";
            if (configuration.GetSection("KubernetesEnv").Value != null)
            {
                stra8kEnviroment = configuration.GetSection("KubernetesEnv").Value.Trim();
            }

            String strDescription = "Permite el manejo de los elementos que componen una estructura";

            configure.DocumentName = version;
            configure.ApiGroupNames = new[] { version };

            configure.PostProcess = document =>
            {
                document.Info.Version = version;
                document.Info.Title = "API Estructuras - Microservicio";
                document.Info.Description = string.Format(
                        "{0} <br> Version: {1}<br>Build Version:{2}<br>Kubernetes Enviroment:{3}",
                        strDescription,
                        strVersion,
                        strBuildVersion,
                        stra8kEnviroment);

                document.Info.Version = string.Format("{0}", strVersion);
            };

            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        }
    }
}