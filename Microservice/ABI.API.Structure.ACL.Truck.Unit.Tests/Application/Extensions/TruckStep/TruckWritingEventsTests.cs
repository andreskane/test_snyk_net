using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.Extensions.TruckStep.Tests
{
    [TestClass()]
    public class TruckWritingEventsTests
    {
        [TestMethod()]
        public void ToStartEventTest()
        {
            var result = TruckWritingEvents.ToStartEvent(new TruckWritingPayload
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            });

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureName.Should().Be("TEST");
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.Username.Should().Be("TEST");
        }

        [TestMethod()]
        public void ToDoneEventTest()
        {
            var result = TruckWritingEvents.ToDoneEvent(new TruckWritingPayload
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            });

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureName.Should().Be("TEST");
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.Username.Should().Be("TEST");
        }

        [TestMethod()]
        public void ToErrorEventTest()
        {
            var result = TruckWritingEvents.ToErrorEvent(new TruckWritingPayload
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            });

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureName.Should().Be("TEST");
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.Username.Should().Be("TEST");
        }
    }
}