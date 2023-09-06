using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities.Tests
{
    [TestClass()]
    public class VersionedLogStatusTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new VersionedLogStatus
            {
                Code = "TEST",
                VersionedLogs = null
            };

            result.Should().NotBeNull();
            result.Code.Should().Be("TEST");
            result.VersionedLogs.Should().BeNull();
        }
    }
}