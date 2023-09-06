using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckOpeAPRCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TruckOpeAPRCommand()
        {
            var result = new TruckOpeAPRCommand();
            result.VersionedId = 1;
            result.OpeiniOut = null;
            result.ValidityFrom = DateTimeOffset.MinValue;
            result.PlayLoad = null;

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.OpeiniOut.Should().BeNull();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.PlayLoad.Should().BeNull();

        }
    }
}
