using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using Autofac;

namespace ABI.API.Structure.ACL.Truck.Application.Infrastructure.AutofacModules
{
    public class ApplicationModuleACLTruck : Autofac.Module
    {

        private readonly string _apiBaseResource;

        public ApplicationModuleACLTruck()
        {
        }

        public ApplicationModuleACLTruck(string apiResourse)
        {
            _apiBaseResource = apiResourse;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StructureAdapter>()
             .As<IStructureAdapter>()
             .InstancePerDependency();

            builder.RegisterType<MapeoTableTruckPortal>()
               .As<IMapeoTableTruckPortal>()
               .InstancePerLifetimeScope();

            builder
             .Register(x => new DBUHResourceRepository(_apiBaseResource))
             .As<IDBUHResourceRepository>()
             .InstancePerLifetimeScope();

        }
    }
}
