using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class TruckOpeUPDCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void TruckOpeUPDCommand()
        {
            var result = new TruckOpeUPDCommand
            {
                VersionedId = 1,
                OpeiniOut = null,
                ValidityFrom = DateTimeOffset.MinValue
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.OpeiniOut.Should().BeNull();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);

        }
    }
}
