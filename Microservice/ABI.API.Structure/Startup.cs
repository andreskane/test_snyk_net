using ABI.API.Structure.ACL.Truck;
using ABI.API.Structure.ACL.Truck.Application.Infrastructure.AutofacModules;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.APIClient.Truck.Infrastructure.AutofacModules;
using ABI.API.Structure.Application;
using ABI.API.Structure.Application.Service;
using ABI.API.Structure.Application.Services.Notification;
using ABI.API.Structure.Helper;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Infrastructure.AutofacModules;
using ABI.API.Structure.Infrastructure.Filters;
using Autofac;
using Coravel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Identity.Web;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace ABI.API.Structure
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration);




            services.AddHttpClient();
            services.AddCors();

            services
                .AddMvc(opt => opt.Filters.Add<ApiExceptionFilterAttribute>())
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                });

            services.AddControllers();
            services.AddCache(Configuration);
            services.AddSwagger(Configuration);


            services.AddDbContext<StructureContext>(options =>
             {
                 options
                     .UseSqlServer(
                         Configuration.GetConnectionString("StructureDb"),
                         sqlServerOptions => sqlServerOptions.CommandTimeout(60)
                     )
                     .EnableDetailedErrors(true)
                     .EnableSensitiveDataLogging(true);
             });

            services.AddResponseCompression(options =>
            {
                var MimeTypes = new List<string>
                {
                     "text/plain",
                     "application/json"
                };
                options.EnableForHttps = true;
                options.MimeTypes = MimeTypes;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue; // <-- ! long.MaxValue
                options.MultipartBoundaryLengthLimit = int.MaxValue;
                options.MultipartHeadersCountLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddHttpClient();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<ITelemetryInitializer>(new CloudRoleNameTelemetryInitializer(Configuration));

            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"].Trim());

            services.AddApplication();
            services.AddServices();
            services.AddRepositories(Configuration);

            services.AddACLTruck(Configuration);

            services.AddQueue();
            services.AddEvents();
            services.AddSignalR(o => { o.EnableDetailedErrors = true; });


            services.AddMvcCore().AddApiExplorer();


            services.AddApiVersioning( options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default; // 1.0
                options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModuleAPIClientTRuck(Configuration.GetSection("API").GetSection("UrlApiTruck").Value,Configuration.GetSection("API").GetSection("UrlApiTruckTimeoutApi").Value));
            builder.RegisterModule(new ApplicationModuleACLTruck(Configuration.GetSection("API").GetSection("UrlApiDBUHResource").Value));
            builder.RegisterModule(new ApplicationModuleInfrastructure(Configuration.GetSection("API").GetSection("UrlApiDBUHResource").Value));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StructureContext dbContext, TruckACLContext dbTruckContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            var RunMigration = Configuration["IsSwager"].Trim();
            if (RunMigration != "1")
            {
                //no comentar estas lineas
                dbContext.Database.Migrate();
                dbTruckContext.Database.Migrate();
            }
           

            app.UseResponseCompression();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                        await context.Response.WriteAsync(
                            new ErrorDetails(contextFeature.Error.Source, contextFeature.Error.Message).ToString());
                });
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(options =>
            {
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/NotificationService");

            });
           app.UseOpenApi();
            app.UseSwaggerUi3();
           // app.UseOpenApi();
            app.UseApplication();
        }
    }
}
