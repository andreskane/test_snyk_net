
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;

namespace ABI.API.Structure.Helper
{
    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string roleName;

        public CloudRoleNameTelemetryInitializer(IConfiguration configuration)
        {
            String enviroment = "local";
            try
            {
                if (configuration["KubernetesEnv"] != null)
                {
                    enviroment = configuration["KubernetesEnv"].Trim();
                }
            }
            catch
            {
                //no esta declarada la key en el app settings, esto tendria que suceder en el entono local


            }
            String AppName = typeof(Startup).Assembly.GetName().Name.ToString();
            this.roleName = AppName + "-" + enviroment;

        }
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = this.roleName;
            telemetry.Context.Cloud.RoleInstance = this.roleName;
        }

    }
}




