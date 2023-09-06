using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureDomainDTOTests
    {
        [TestMethod()]
        public void StructureDomainDTOTest()
        {
            var result = new StructureDomainDTO();
            result.HasUnsavedChanges = true;
            result.CalendarDate = DateTimeOffset.MinValue;
            result.Nodes = null;
            result.Structure = null;

            result.Should().NotBeNull();
            result.HasUnsavedChanges.Should().BeTrue();
            result.CalendarDate.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
            result.Structure.Should().BeNull();
        }
    }
}