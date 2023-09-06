using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using Autofac;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ApplicationModuleInfrastructure : Module
    {
        private readonly string _apiBaseResource;

        public ApplicationModuleInfrastructure()
        {
        }

        public ApplicationModuleInfrastructure(string apiResourse)
        {
            _apiBaseResource = apiResourse;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder
         .Register(x => new DBUHResourceRepository(_apiBaseResource))
         .As<IDBUHResourceRepository>()
         .InstancePerLifetimeScope();
        }
    }
}
