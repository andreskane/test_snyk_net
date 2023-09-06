using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class TruckStatusDTOTests
    {
        [TestMethod()]
        public void TruckStatusDTOTest()
        {
            var result = new TruckStatusDTO();
            result.Code = 1;
            result.Message = "TEST";

            result.Should().NotBeNull();
            result.Code.Should().Be(1);
            result.Message.Should().Be("TEST");
        }
    }
}