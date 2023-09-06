using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckSendVersionCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TruckSendVersionCommand()
        {
            var result = new TruckSendVersionCommand
            {
                Versioned = null,
                PlayLoad = null
            };

            result.Should().NotBeNull();
            result.Versioned.Should().BeNull();
            result.PlayLoad.Should().BeNull();
        }
    }
}
