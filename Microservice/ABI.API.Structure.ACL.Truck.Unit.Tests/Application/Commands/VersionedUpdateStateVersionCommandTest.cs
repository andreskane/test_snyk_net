using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedUpdateStateVersionCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedUpdateStateVersionCommand()
        {
            var result = new VersionedUpdateStateVersionCommand();
            result.VersionedId = 1;
            result.State = VersionedState.Aceptado;
            result.Message = "TEST";

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.State.Should().Be(VersionedState.Aceptado);
            result.Message.Should().Be("TEST");

        }
    }
}
