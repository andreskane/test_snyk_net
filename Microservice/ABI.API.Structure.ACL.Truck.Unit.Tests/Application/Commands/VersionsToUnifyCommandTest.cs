using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionsToUnifyCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }


        [TestMethod()]
        public void VersionsToUnifyCommand()
        {
            var result = new VersionsToUnifyCommand
            {
                Payload = null
            };

            result.Should().NotBeNull();
            result.Payload.Should().BeNull();

        }
    }
}
