using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Entities
{
    [TestClass()]
    public class VersionedTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new Versioned
            {
                StructureId = 1,
                Date = DateTimeOffset.MinValue,
                Version = "1",
                Validity = DateTimeOffset.MinValue,
                StatusId = 1,
                User = "TEST",
                VersionedStatus = null,
                VersionedsNode = null,
                VersionedsArista = null,
                VersionedsLog = null
            };

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.Version.Should().Be("1");
            result.Validity.Should().Be(DateTimeOffset.MinValue);
            result.StatusId.Should().Be(1);
            result.User.Should().Be("TEST");
            result.VersionedStatus.Should().BeNull();
            result.VersionedsNode.Should().BeNull();
            result.VersionedsArista.Should().BeNull();
            result.VersionedsLog.Should().BeNull();
        }
    }
}