using ABI.API.Structure.Application.Infrastructure;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Mappings
{
    [TestClass]
    public class MappingsTests
    {

        [TestMethod]
        public void MappingTieneQueTenerUnMapeoValido()
        {
            var configuration = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperConfig()); });
            configuration.AssertConfigurationIsValid();
        }
    }
}
