using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureClientDTOTests
    {
        [TestMethod()]
        public void StructureClientDTOTest()
        {
            var result = new StructureClientDTO
            {
                Id = 1,
                NodeId = 1,
                ClientId = "TEST",
                Name = "TEST",
                State = "TEST",
                ValidityFrom = DateTimeOffset.MinValue,
                ValidityTo = DateTimeOffset.MinValue
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.NodeId.Should().Be(1);
            result.ClientId.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.State.Should().Be("TEST");
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
            result.ValidityTo.Should().Be(DateTimeOffset.MinValue);
        }
    }
}