using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RequestTrayDetailDTOTests
    {
        [TestMethod()]
        public void RequestTrayDetailDTOTest()
        {
            var result = new RequestTrayDetailDTO();
            result.Made = DateTime.MinValue;
            result.ChangesByNode = null;

            result.Should().NotBeNull();
            result.Made.Should().Be(DateTime.MinValue);
            result.ChangesByNode.Should().BeNull();
        }
    }
}