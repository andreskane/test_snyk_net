
using ABI.API.Structure.ACL.Truck.Application.BackgroundServices;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Interface;
using ABI.API.Structure.ACL.Truck.Application.Translators.Interface;
using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Infrastructure.Publishers;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces.Structure;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Structure;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using Coravel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ABI.API.Structure.ACL.Truck
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static IServiceCollection AddACLTruck(this IServiceCollection services, IConfiguration configuration)
        {
            //EventBus Publishers
            services.AddSingleton<ITruckAclPublisher>(_ => new TruckAclPublisher(configuration));

            //Contexts
            services.AddDbContext<TruckACLContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TruckACLDb"));
            });


            services.AddSingleton<ISyncLogRepository>(s =>
            {
                var builder = new DbContextOptionsBuilder<TruckACLContext>();
                builder.UseSqlServer(
                    configuration.GetConnectionString("TruckACLDb"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(60)
                );

                return new SyncLogRepository(new TruckACLContext(builder.Options));
            });

            services.AddSingleton<ISyncResourceResponsable, SyncResourceResponsable>();
            services.AddHostedService<ResourceResponsableBackgroundService>();

            // Repository Truck


            //Truck repositories
            services.AddScoped<ISyncLogRepository, SyncLogRepository>();
            services.AddScoped<ILevelTruckPortalRepository, LevelTruckPortalRepository>();
            services.AddScoped<ITypeVendorTruckRepository, TypeVendorTruckRepository>();

            services.AddScoped<IVersionedAristaRepository, VersionedAristaRepository>();
            services.AddScoped<IVersionedLogRepository, VersionedLogRepository>();
            services.AddScoped<IVersionedLogStatusRepository, VersionedLogStatusRepository>();
            services.AddScoped<IVersionedNodeRepository, VersionedNodeRepository>();
            services.AddScoped<IVersionedRepository, VersionedRepository>();
            services.AddScoped<IVersionedStatusRepository, VersionedStatusRepository>();

            services.AddScoped<IEstructuraClienteTerritorioIORepository, EstructuraClienteTerritorioIORepository>();
            services.AddScoped<IImportProcessRepository, ImportProcessRepository>();
            services.AddScoped<IPeriodicityDaysRepository, PeriodicityDaysRepository>();
            services.AddScoped<IStructureNodeDefinitionsRespository, StructureNodeDefinitionsRespository>();
            services.AddScoped<IResourceResponsableRepository, ResourceResponsableRepository>();

            //Truck Services
            services.AddScoped<ITruckToPortalService, TruckToPortalService>();
            services.AddScoped<ITruckService, TruckService>();
            services.AddScoped<IPortalService, PortalService>();
            services.AddScoped<IStructureNodePortalRepository, StructureNodePortalRepository>();

            services.AddScoped<ILevelTruckPortalService, LevelTruckPortalService>();
            services.AddScoped<ITypeVendorTruckService, TypeVendorTruckService>();
            services.AddScoped<ICategoryVendorTruckService, CategoryVendorTruckService>();

            services.AddScoped<ITranslatorsStructuresPortalToTruck, TranslatorsStructuresPortalToTruck>();
            services.AddScoped<ITranslatorsStructuresTruckToPortal, TranslatorsStructuresTruckToPortal>();
            services.AddScoped<ICompareStructuresTruckAndPortal, CompareStructures> ();
            services.AddScoped <IStructureAdapter, StructureAdapter>();

            //Queue
            services.AddQueue();
            services.AddTransient<TruckWritingExecutor>();
            services.AddTransient<IOFinishProcessExecutor>();

            services.AddTransient<ProcessVersionedExecutor>();            

            services.AddMediatR(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}