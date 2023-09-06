using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.TruckStep.Tests
{
    [TestClass()]
    public class TruckWritingPayloadTests
    {
        [TestMethod()]
        public void TruckWritingPayloadTest()
        {
            var result = new TruckWritingPayload();
            result.StructureId = 1;
            result.StructureName = "TEST";
            result.Date = DateTimeOffset.MinValue;
            result.Username = "TEST";

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureName.Should().Be("TEST");
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.Username.Should().Be("TEST");
        }
    }
}