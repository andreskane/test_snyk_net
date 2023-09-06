using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.APIClient.Truck.Models.Tests
{
    [TestClass()]
    public class WkgrestTesterTests
    {
        [TestMethod()]
        public void WkgrestTesterTest()
        {
            var result = new WkgrestTester
            {
                DatoIn = null
            };

            result.Should().NotBeNull();
            result.DatoIn.Should().BeNull();
        }
    }
}