using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.Framework.MS.Messages.EventBus;
using ABI.Framework.MS.Repository.Generics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IReadOnlyRepository<,,>), typeof(ReadOnlyRepository<,,>));
            services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAttentionModeRepository, AttentionModeRepository>();
            services.AddScoped<IStructureModelRepository, StructureModelRepository>();
            services.AddScoped<IStructureModelDefinitionRepository, StructureModelDefinitionRepository>();
            services.AddScoped<ISaleChannelRepository, SaleChannelRepository>();
            services.AddScoped<IAttentionModeRoleRepository, AttentionModeRoleRepository>();
            services.AddScoped<IChangeTrackingRepository, ChangeTrackingRepository>();
            services.AddScoped<IChangeTrackingStatusRepository, ChangeTrackingStatusRepository>();
            services.AddScoped<IDBUHResourceRepository, DBUHResourceRepository>();
            services.AddScoped<IStructureRepository, StructureRepository>();
            services.AddScoped<IStructureNodeRepository, StructureNodeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IStructureClientRepository, StructureClientRepository>();
            services.AddScoped<IMostVisitedFilterRepository, MostVisitedFilterRepository>();


            services.AddMessagePublisher(
               configuration["EstructuraServiceBus:ConnectionString"],
               configuration["EstructuraServiceBus:TopicName"]);

            return services;
        }
    }
}