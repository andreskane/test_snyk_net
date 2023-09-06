using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class VersionedLogTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new VersionedLog
            {
                VersionedId = 1,
                Date = DateTime.MinValue,
                LogStatusId = 1,
                Detaill = "TEST",
                Versioned = null,
                LogStatus = null
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
            result.Date.Should().Be(DateTime.MinValue);
            result.LogStatusId.Should().Be(1);
            result.Detaill.Should().Be("TEST");
            result.Versioned.Should().BeNull();
            result.LogStatus.Should().BeNull();
        }
    }
}