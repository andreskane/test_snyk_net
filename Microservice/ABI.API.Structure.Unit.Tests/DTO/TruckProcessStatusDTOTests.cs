using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class TruckProcessStatusDTOTests
    {
        [TestMethod()]
        public void TruckProcessStatusDTOTest()
        {
            var result = new TruckProcessStatusDTO();
            result.Structure = null;
            result.Username = "TEST";
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.Structure.Should().BeNull();
            result.Username.Should().Be("TEST");
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }
    }
}