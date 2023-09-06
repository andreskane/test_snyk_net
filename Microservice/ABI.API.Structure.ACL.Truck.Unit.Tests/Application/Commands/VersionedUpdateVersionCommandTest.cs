using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedUpdateVersionCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedUpdateVersionCommand()
        {
            var result = new VersionedUpdateVersionCommand
            {
                Versioned = null
            };

            result.Should().NotBeNull();
            result.Versioned.Should().BeNull();

        }
    }
}
