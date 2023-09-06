using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class VersionedAristaTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new VersionedArista
            {
                VersionedId = 1,
                AristaId = 1,
                Versioned = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.AristaId.Should().Be(1);
            result.Versioned.Should().BeNull();
        }
    }
}