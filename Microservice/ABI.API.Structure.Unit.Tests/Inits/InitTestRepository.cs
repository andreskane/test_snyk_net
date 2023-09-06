using AutoMapper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Inits
{
    [TestClass]
    public class InitTestRepository
    {
        public IConfiguration Configuration { get; }

        [AssemblyInitialize()]
        public static void Setup(TestContext context)
        {
            AddDataContext.PrepareFactoryData();
            AddDataTruckACLContext.PrepareFactoryData();
        }
    }
}
