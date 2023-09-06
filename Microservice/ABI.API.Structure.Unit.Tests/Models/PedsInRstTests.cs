using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.APIClient.Truck.Models.Tests
{
    [TestClass()]
    public class PedsInRstTests
    {
        [TestMethod()]
        public void PedsInRstTest()
        {
            var result = new PedsInRst();
            result.DatoIn = "TEST";
            result.DatoExtra = "TEST";

            result.Should().NotBeNull();
            result.DatoIn.Should().Be("TEST");
            result.DatoExtra.Should().Be("TEST");
        }
    }
}