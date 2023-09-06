using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Behaviors;
using ABI.API.Structure.Application.Notifications.Truck;
using ABI.API.Structure.Application.Services;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using Coravel;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ABI.API.Structure.Application
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<TruckProcessListenerStart>();
            services.AddTransient<TruckProcessListenerDone>();
            services.AddTransient<TruckProcessListenerError>();
         

            return services;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;
            var registration = provider.ConfigureEvents();
            registration
                .Register<TruckWritingEventStart>()
                .Subscribe<TruckProcessListenerStart>();
            registration
                .Register<TruckWritingEventDone>()
                .Subscribe<TruckProcessListenerDone>();
            registration
                .Register<TruckWritingEventError>()
                .Subscribe<TruckProcessListenerError>();

            return app;
        }
    }
}
