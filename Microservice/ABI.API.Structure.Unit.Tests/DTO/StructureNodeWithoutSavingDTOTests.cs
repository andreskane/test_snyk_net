using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureNodeWithoutSavingDTOTests
    {
        [TestMethod()]
        public void StructureNodeWithoutSavingDTOTest()
        {
            var result = new StructureNodeWithoutSavingDTO();
            result.ChangesWithoutSaving = true;
            result.CalendarDate = DateTimeOffset.MinValue;
            result.Nodes = null;
            result.Structure = null;

            result.Should().NotBeNull();
            result.ChangesWithoutSaving.Should().BeTrue();
            result.CalendarDate.Should().Be(DateTimeOffset.MinValue);
            result.Nodes.Should().BeNull();
            result.Structure.Should().BeNull();
        }
    }
}