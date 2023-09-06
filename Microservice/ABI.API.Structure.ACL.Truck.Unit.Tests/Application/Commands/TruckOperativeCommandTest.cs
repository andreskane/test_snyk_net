using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckOperativeCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TruckOperativeCommand()
        {
            var result = new TruckOperativeCommand
            {
                PlayLoad = null
            };

            result.Should().NotBeNull();
            result.PlayLoad.Should().BeNull();

        }
    }
}
