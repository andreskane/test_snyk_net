using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Commands.Entities.Tests
{
    [TestClass()]
    public class ValidateDateStructureTests
    {
        [TestMethod()]
        public void ValidateDateStructureTest()
        {
            var result = new ValidateDateStructure();
            result.Should().NotBeNull();
            result.StructureId.Should().Be(0);
            result.ValidateDate.Should().BeNull();
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }
    }
}