using ABI.Framework.MS.Helpers.Extensions;
using Autofac;

namespace ABI.API.Structure.APIClient.Truck.Infrastructure.AutofacModules
{
    public class ApplicationModuleAPIClientTRuck : Autofac.Module
    {

        private readonly string _baseUrl;
        private readonly int _timeoutApi;
        public ApplicationModuleAPIClientTRuck(string baseUrl,string timeoutApi)
        {
            _baseUrl = baseUrl;
            _timeoutApi = timeoutApi.ToInt();
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ApiTruck>()
               .As<IApiTruck>()
               .InstancePerDependency();

            builder.Register(r => new ApiTruckUrls(_baseUrl, _timeoutApi))
                .As<IApiTruckUrls>()
                .InstancePerLifetimeScope();

        }
    }
}
