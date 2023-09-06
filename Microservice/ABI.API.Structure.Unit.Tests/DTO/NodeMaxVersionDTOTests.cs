using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class NodeMaxVersionDTOTests
    {
        [TestMethod()]
        public void NodeMaxVersionDTOTest()
        {
            var result = new NodeMaxVersionDTO();
            result.NodeId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }
    }
}