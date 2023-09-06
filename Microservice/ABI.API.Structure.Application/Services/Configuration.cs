using ABI.API.Structure.Application.Services.Interfaces;
using ABI.API.Structure.Application.Services.Notification;
using ABI.Framework.MS.Service.Generics;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Application.Service
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadOnlyService<,,>), typeof(ReadOnlyService<,,>));
            services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<INotificationStatusService, NotificationStatusService>();

            return services;
        }
    }
}
