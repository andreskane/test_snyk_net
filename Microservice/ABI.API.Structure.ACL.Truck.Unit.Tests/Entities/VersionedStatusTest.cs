using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class VersionedStatusTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new VersionedStatus
            {
                Versioneds = null
            };

            result.Should().NotBeNull();
            result.Versioneds.Should().BeNull();
        }
    }
}