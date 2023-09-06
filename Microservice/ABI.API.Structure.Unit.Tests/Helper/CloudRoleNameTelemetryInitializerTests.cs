using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Helper.Tests
{
    [TestClass()]
    public class CloudRoleNameTelemetryInitializerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var configDic = new Dictionary<string, string> { { "KubernetesEnv", "TEST" } };
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configDic)
                .Build();

            var result = new CloudRoleNameTelemetryInitializer(config);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateTwoTest()
        {
            var result = new CloudRoleNameTelemetryInitializer(null);
            result.Should().NotBeNull();
        }
    }
}