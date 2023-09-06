using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedGenerateVersionCommandTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Method intentionally left empty.
        }

        [TestMethod()]
        public void VersionedGenerateVersionCommand()
        {
            var result = new VersionedGenerateVersionCommand
            {
                StructureId = 1,
                ValidityFrom = DateTimeOffset.MinValue,
                User = "TEST"
            };

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.User.Should().Be("TEST");

        }
    }
}
