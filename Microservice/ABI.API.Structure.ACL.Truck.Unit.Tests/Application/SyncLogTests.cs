using ABI.API.Structure.ACL.Truck.Domain.Entities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application
{
    [TestClass]
    public class SyncLogTests
    {
        [TestMethod]
        public void SyncLogTest()
        {
            var result = new SyncLog();
            result.Id = 1;
            result.Message = "TEST";

            result.Message.Should().Be("TEST");
            result.Id.Should().Be(1);
        }
    }
}
