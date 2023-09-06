using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.AggregatesModel.StructureNodeAggregate
{
    [TestClass()]
    public class StructureAristaTests
    {
        [TestMethod()]
        public void StructureAristaCreateTest()
        {
            var result = new StructureArista(1, 1, 1, 1, 1, DateTimeOffset.MinValue, DateTimeOffset.MinValue);
            result.EditValidityTo(DateTimeOffset.UtcNow.Date);
            result.EditMotiveStateId(1);


            result.Should().NotBeNull();
            result.ValidityTo.Should().Be(DateTimeOffset.UtcNow.Date);
            result.MotiveStateId.Should().Be(1);
            result.MotiveState.Should().BeNull();

        }
    }
}
